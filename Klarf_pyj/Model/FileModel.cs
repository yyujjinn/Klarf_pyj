using System.IO;
using System.Collections.Generic;

namespace Klarf_pyj.Model
{
    class FileModel
    {
        #region [상수]
        string folderPath;

        #endregion

        #region [필드]
        List<FileInfo> fileNames = new List<FileInfo>();
        List<string> fileDates = new List<string>();

        #endregion

        #region [속성]


        #endregion

        #region [생성자]



        #endregion


        public List<FileInfo> LoadFileList()
        {
            folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";

            DirectoryInfo di = new DirectoryInfo(folderPath);

            string[] extensions = new string[] { "*.001", "*.tif", "*.jpg" };
            foreach (string extension in extensions)
            {
                FileInfo[] files = di.GetFiles(extension);
                fileNames.AddRange(files);
            }
            return fileNames;
        }

        public List<string> LoadFileDateList()
        {
            folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";



            string[] files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var info = new FileInfo(file);
                fileDates.Add(info.CreationTime.ToString());
            }
            return fileDates;
        }
    }
}
