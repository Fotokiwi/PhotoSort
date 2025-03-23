using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort.CustomClasses
{
    internal class PhotoSortFile
    {
        /*
        Old Style Property
        private string _pathOfFile;
        public string PathOfFile
        {
            get { return _pathOfFile; }
            set { _pathOfFile = value; }
        }
        */

        /*
        New style Property
        */
        public string PathOfFile { get; set; }
        public string NameOfFile { get; set; }
        public string TypeOfFile { get; set; }
        public DateTime CreationOfFile { get; set; }

        public DateTime LastEditOfFile { get; set; }
        public double SizeOfFile { get; set; }

        public ImageMetadata Meta { get; set; }

        public PhotoSortFile(string path, string name, string type, DateTime creation, DateTime lastedited, double size, bool useEXIF)
        {
            PathOfFile = path;
            NameOfFile = name;
            TypeOfFile = type;
            CreationOfFile = creation;
            LastEditOfFile = lastedited;
            SizeOfFile = size;
            Meta = EXIFReader.ReadExif(path);

            if(useEXIF)
            {
                if(!(Meta.Creation.Year == 1))
                    CreationOfFile = Meta.Creation;
                if(!(Meta.FileModified.Year == 1))
                    LastEditOfFile = Meta.FileModified;
            }

        }

    }
}
