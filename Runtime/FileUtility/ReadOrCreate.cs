using System.IO;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{
    public static partial class FileUtility
    {
        /// <summary>
        ///     Attempts to read a file which if not found is then created with `defaultContents`
        /// </summary>
        /// <param name="fileName">Name of file to read</param>
        /// <param name="defaultContents">String contents to use to create file if it does not exist</param>
        /// <returns>Contents of file, whether read or used to create file from `defaultContents`</returns>
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