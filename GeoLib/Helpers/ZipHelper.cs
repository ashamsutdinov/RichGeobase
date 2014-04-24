using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace GeoLib.Dal.Helpers
{
    public static class ZipHelper
    {
        public static Dictionary<string, MemoryStream> Unzip(Stream stream)
        {
            var data = new Dictionary<string, MemoryStream>();
            using (var zip = ZipFile.Read(stream))
            {
                foreach (var e in zip)
                {
                    var s = new MemoryStream();
                    e.Extract(s);
                    data.Add(e.FileName, s);
                }
            }
            return data;
        }
    }
}
