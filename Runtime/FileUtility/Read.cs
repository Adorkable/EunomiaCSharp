using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Eunomia
{
    public static partial class FileUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">If file to read is not found</exception>
        public static async Task<string> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Unable to find file", fileName);
            }

            using (var stream = File.Open(fileName, FileMode.Open))
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, (int)stream.Length);
                stream.Close();

                return Encoding.Default.GetString(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fallbackFileName"></param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">If file to read and fallback are not found</exception>
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
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fallbackFileName"></param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">If file to read and fallback are not found</exception>
        public static async Task<(string, string)> ReadFileWithFallbackAndFileName(string fileName, string fallbackFileName)
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