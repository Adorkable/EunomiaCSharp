using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Eunomia
{
    public static class FileUtility_Read
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
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

    public static class FileUtility_Write
    {
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

    public static class FileUtility_ReadOrCreate
    {
        public static async Task<string> ReadFileOrCreateDefault(string fileName, string defaultContents)
        {
            try
            {
                var contents = await FileUtility_Read.ReadFile(fileName);
                return contents;
            }
            catch (FileNotFoundException)
            {
                await FileUtility_Write.WriteFile(fileName, defaultContents);
                return defaultContents;
            }
        }
    }
};