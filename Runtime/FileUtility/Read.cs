using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace - In folder for organization reasons
namespace Eunomia
{
    public static partial class FileUtility
    {
        /// <summary>
        ///     Read a file, if it exists, to a string
        /// </summary>
        /// <param name="fileName">File name of the file to be read</param>
        /// <returns>Contents of the file as a string</returns>
        /// <exception cref="System.IO.FileNotFoundException">File to read is not found</exception>
        public static async Task<string> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Unable to find file", fileName);
            }

            await using (var stream = File.Open(fileName, FileMode.Open))
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer.AsMemory(0, (int) stream.Length));
                stream.Close();

                return Encoding.Default.GetString(buffer);
            }
        }

        /// <summary>
        ///     Read a file, if it exists. If it doesn't read a fallback file, if it exists
        /// </summary>
        /// <param name="fileName">File name of the file to be read</param>
        /// <param name="fallbackFileName">File name of the file to be read if first file does not exist</param>
        /// <returns>Contents of first file, if it exists, or contents of second file, if it exists</returns>
        /// <exception cref="System.IO.FileNotFoundException">First file and fallback are not found</exception>
        public static async Task<string> ReadFileWithFallback(string fileName, string fallbackFileName)
        {
            try
            {
                return await ReadFile(fileName);
            }
            catch (FileNotFoundException)
            {
                return await ReadFile(fallbackFileName);
            }
        }

        /// <summary>
        ///     Read a file, if it exists. If it doesn't read a fallback file, if it exists. Report back the fileName successfully
        ///     read
        /// </summary>
        /// <param name="fileName">File name of the file to be read</param>
        /// <param name="fallbackFileName">File name of the file to be read if first file does not exist</param>
        /// <returns>
        ///     Tuple of:<br />
        ///     1. Contents of first file, if it exists, or contents of second file, if it exists<br />
        ///     and<br />
        ///     2. File name of file successfully read
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException">First file and fallback are not found</exception>
        public static async Task<(string, string)> ReadFileWithFallbackAndFileName(string fileName,
            string fallbackFileName)
        {
            try
            {
                var contents = await ReadFile(fileName);
                return (contents, fileName);
            }
            catch (FileNotFoundException)
            {
                var contents = await ReadFile(fallbackFileName);
                return (contents, fallbackFileName);
            }
        }
    }
}