using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Jpeg;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PhotoSort.CustomClasses
{
    internal class EXIFReader
    {

        public static ImageMetadata ReadExif(string filePath)
        {
            var meta = new ImageMetadata();

            try
            {
                var directories = ImageMetadataReader.ReadMetadata(filePath);

                var temp_subIfd = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                if (temp_subIfd != null && temp_subIfd.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out DateTime exifDate))
                {
                    // File info
                    FileInfo fi = new FileInfo(filePath);
                    meta.FileName = fi.Name;
                    meta.FileSize = fi.Length;
                    meta.FileModified = fi.LastWriteTime;

                    // IFD0 - general info
                    var ifd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                    if (ifd0 != null)
                    {
                        meta.Maker = ifd0.GetDescription(ExifDirectoryBase.TagMake);
                        meta.Model = ifd0.GetDescription(ExifDirectoryBase.TagModel);
                        meta.Software = ifd0.GetDescription(ExifDirectoryBase.TagSoftware);
                    }

                    // SubIFD - photo parameters
                    var subIfd = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                    if (subIfd != null)
                    {
                        meta.ISO = subIfd.GetInt32Safe(ExifDirectoryBase.TagIsoEquivalent);
                        meta.Aperture = subIfd.GetDescription(ExifDirectoryBase.TagFNumber);
                        meta.Exposure = subIfd.GetDescription(ExifDirectoryBase.TagExposureTime);

                        var dateStr = subIfd.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                        if (DateTime.TryParseExact(dateStr, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime creation))
                        {
                            meta.Creation = creation;
                        }
                    }

                    // GPS
                    var gps = directories.OfType<GpsDirectory>().FirstOrDefault();
                    var location = gps?.GetGeoLocation();
                    if (location != null)
                    {
                        meta.Latitude = location.Latitude;
                        meta.Longitude = location.Longitude;
                    }
                }
            } catch
            {
              
            }

            

            return meta;
        }

    }
}
