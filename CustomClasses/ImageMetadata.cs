using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort.CustomClasses
{
    internal class ImageMetadata
    {
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Software { get; set; }
        public int ISO { get; set; }
        public string Aperture { get; set; }
        public string Exposure { get; set; }
        public DateTime Creation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime FileModified { get; set; }

    }
}
