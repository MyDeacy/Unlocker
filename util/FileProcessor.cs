using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Unlocker.util
{   
    class FileProcessor
    {
        private const string TARGET_SHEET = "sheetProtection";

        private const string TARGET_WORKBOOK = "workbookProtection ";

        private string temp = Path.GetTempPath() + "\\ExcelUnlocker\\";

        public string[] ExtractBook(string path)
        {
            if(System.IO.Directory.Exists(temp))
                System.IO.Directory.Delete(temp, true);
            System.IO.Compression.ZipFile.ExtractToDirectory(
                @path, @temp, Encoding.GetEncoding("shift_jis"));
            return System.IO.Directory.GetFiles(@temp, "*.xml", System.IO.SearchOption.AllDirectories);
        }

        public int UnLockProtect(string[] files)
        {
            int count = 0;
            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };
            foreach (string file in files)
            {
                xml.Load(@file);
                foreach (XmlElement element in xml.DocumentElement.ChildNodes)
                {
                    if(element.Name == TARGET_SHEET || element.Name == TARGET_WORKBOOK)
                    {
                        element.RemoveAll();
                        count++;
                    }
                }                
                xml.Save(@file);
            }
            return count;
        }

        public void MakeExcelFile(string path)
        {
            System.IO.Compression.ZipFile.CreateFromDirectory(
                @temp,
                @path +"\\unlocked.xlsx",
                System.IO.Compression.CompressionLevel.Optimal,
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
        }
    }
}
