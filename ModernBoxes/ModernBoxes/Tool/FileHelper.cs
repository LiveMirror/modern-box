using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Tool
{
    public class FileHelper
    {

        public static async Task<bool> WriteFile(String path,String Content)
        {
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            try
            {
                await streamWriter.WriteAsync(Content);
               
            }catch (Exception ex)
            {
                return false;
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
            }
            return true;
        }



        public static async Task<String> ReadFile(String path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            String content = streamReader.ReadToEnd();
            streamReader.Close();
            fileStream.Close();
            return content;
        }
    }
}
