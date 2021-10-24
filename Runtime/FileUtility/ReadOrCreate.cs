using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Eunomia
{
    public static partial class FileUtility
    {
        /// <summary>
        /// Attempts to read a file which if not found is then created with `defaultContents`
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="defaultContents"></param>
        /// <returns></returns>
        public static async Task<string> ReadFileOrCreateDefault(string fileName, string defaultContents)
        {
            try
            {
                var contents = await ReadFile(fileName);
                return contents;
            }
            catch (FileNotFoundException)
            {
                await WriteFile(fileName, defaultContents);
                return defaultContents;
            }
        }
    }
}