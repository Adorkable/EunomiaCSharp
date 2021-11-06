using System;
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
            await using (var stream = File.Open(fileName, FileMode.Create))
            {
                var buffer = Encoding.UTF8.GetBytes(contents);
                await stream.WriteAsync(buffer.AsMemory(0, buffer.Length));
                stream.Close();
            }
        }
    }
}