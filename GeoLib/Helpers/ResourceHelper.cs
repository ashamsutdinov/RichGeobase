using System.IO;
using System.Linq;
using System.Net;

namespace GeoLib.Helpers
{
    public static class ResourceHelper
    {
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Stream ReadFileContent(string path, bool isString = false)
        {
            Stream stream;
            if (isString)
            {
                stream = GenerateStreamFromString(path);
            }
            else
            {
                if (path.StartsWith("http"))
                {
                    var req = WebRequest.Create(path);
                    var resp = req.GetResponse();
                    stream = resp.GetResponseStream();
                }
                else
                {
                    stream = File.OpenRead(path);
                }
                if (path.EndsWith("zip"))
                {
                    var unzipped = ZipHelper.Unzip(stream);
                    var firstUnzipped = unzipped.FirstOrDefault();
                    stream = firstUnzipped.Value;
                }
            }
            return stream;
        }
    }
}