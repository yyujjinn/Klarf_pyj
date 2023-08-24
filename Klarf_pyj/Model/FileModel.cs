using System.IO;
using System.Collections.Generic;

namespace Klarf_pyj.Model
{
    class FileModel
    {
        public List<FileInfo> LoadFileList()
        {
            string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";

            DirectoryInfo di = new DirectoryInfo(folderPath);
            List<FileInfo> fileNames = new List<FileInfo>();

            string[] extensions = new string[] { "*.001", "*.tif", "*.jpg" };
            foreach (string extension in extensions)
            {
                FileInfo[] files = di.GetFiles(extension);
                fileNames.AddRange(files);
            }
            return fileNames;
        }
    }
}
