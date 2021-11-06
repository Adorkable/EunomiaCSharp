using System.IO;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{
    public static partial class FileUtility
    {
        public static async Task WriteFile(string fileName, string contents)
        {
            // ReSharper disable once UseAwaitUsing - Not supported by Unity 2019
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                var buffer = Encoding.UTF8.GetBytes(contents);
#pragma warning disable CA1835 // Not supported by Unity 2019
                await stream.WriteAsync(buffer, 0, buffer.Length);
#pragma warning restore CA1835
                stream.Close();
            }
        }
    }
}