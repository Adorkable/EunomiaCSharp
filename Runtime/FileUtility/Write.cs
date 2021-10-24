using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Eunomia
{
    public static partial class FileUtility {
        public static async Task WriteFile(string fileName, string contents)
        {
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(contents);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                stream.Close();
            }
        }
    }
}