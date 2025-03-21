using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort.CustomClasses
{
    public static class MetadataExtensions
    {
        public static int GetInt32Safe(this Directory dir, int tag)
        {
            return dir.ContainsTag(tag) ? dir.GetInt32(tag) : 0;
        }
    }
}
