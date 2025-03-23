using Microsoft.WindowsAPICodePack.Dialogs;
using PhotoSort.CustomClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoSort
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<PhotoSortFile> filesInDirectory = new List<PhotoSortFile>();
        Dictionary<string, int> fileExtensions = new Dictionary<string, int>();
        Dictionary<int, int> fileCreationYear = new Dictionary<int, int>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeFields();
        }

        private void InitializeFields()
        {
            input_originalFilepath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString();
            input_destinationFilepath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString();
        }

        private void button_Click_OriginalFilePath(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Wähle einen Ordner",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), // Optional
                EnsurePathExists = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string selectedPath = dialog.FileName;
                input_originalFilepath.Text = selectedPath;
            }
        }

        private void button_Click_DestinationFilePath(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Wähle einen Ordner",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), // Optional
                EnsurePathExists = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string selectedPath = dialog.FileName;
                input_destinationFilepath.Text = selectedPath;
            }
        }

        private void action_button_analyse_Click(object sender, RoutedEventArgs e)
        {
            output_dataGrid_fileList.ItemsSource = null;
            filesInDirectory.Clear();
            fileExtensions.Clear();
            fileCreationYear.Clear();
            string[] filesInDir = Directory.GetFiles(input_originalFilepath.Text, "*", SearchOption.AllDirectories);

            if (filesInDir == null || filesInDir.Length == 0)
                return;

            foreach (string files in filesInDir)
            {
                FileInfo info = new FileInfo(files);

                if (info.Extension.ToLower() == ".jpg" || info.Extension.ToLower() == ".jpeg" || info.Extension.ToLower() == ".tiff")
                {
                    filesInDirectory.Add(new PhotoSortFile(info.FullName, info.Name, info.Extension, info.CreationTime, info.LastWriteTime, info.Length, (bool)input_checkBox_useEXIF.IsChecked));

                    string extension = string.IsNullOrEmpty(info.Extension) ? "(ohne Endung)" : info.Extension.ToLower();

                    if (fileExtensions.ContainsKey(extension))
                    {
                        fileExtensions[extension]++;
                    } else
                    {
                        fileExtensions[extension] = 1;
                    }

                    int creationYear = info.CreationTime.Year;

                    if (fileCreationYear.ContainsKey(creationYear))
                    {
                        fileCreationYear[creationYear]++;
                    } else
                    {
                        fileCreationYear[creationYear] = 1;
                    }
                }                

                /*
                string name = info.Name;                   // Dateiname mit Endung
                string fullPath = info.FullName;           // Kompletter Pfad
                string extension = info.Extension;         // Dateiendung (.txt, .jpg, etc.)
                long size = info.Length;                   // Dateigröße in Bytes
                DateTime created = info.CreationTime;      // Erstellungsdatum
                DateTime modified = info.LastWriteTime;    // Zuletzt geändert
                DateTime accessed = info.LastAccessTime;   // Zuletzt geöffnet 
                */
            }
            // DataSet dataSet = new DataSet();
            output_dataGrid_fileList.ItemsSource = filesInDirectory;
            output_dataGrid_fileType.ItemsSource = fileExtensions;
            output_dataGrid_fileYear.ItemsSource = fileCreationYear;
        }

        private void output_dataGrid_fileList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            PhotoSortFile selectedFilePath = output_dataGrid_fileList.SelectedItem as PhotoSortFile;
        }
    }
}
