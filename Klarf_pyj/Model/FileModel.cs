using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Klarf
{
    class FileModel
    {
        #region [상수]
        string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";

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
            DirectoryInfo di = new DirectoryInfo(folderPath);

            string[] extensions = new string[] { "*.001"};
            foreach (string extension in extensions)
            {
                FileInfo[] files = di.GetFiles(extension);
                fileNames.AddRange(files);
            }
            return fileNames;
        }

        public List<string> LoadFileDateList()
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var info = new FileInfo(file);
                fileDates.Add(info.CreationTime.ToString());
            }
            return fileDates;
        }

        public static string LoadFilePath(string folderPath, string targetExtension)
        {
            targetExtension = ".001";

            string[] files = Directory.GetFiles(folderPath);
            var filteredFiles = files.Where(file => Path.GetExtension(file).Equals(targetExtension, StringComparison.OrdinalIgnoreCase));

            string selectedfile = filteredFiles.First();
            return selectedfile;
        }

        public static string LoadFile(string filePath, string targetExtension)
        {
            //string selectedFile = LoadFilePath(folderPath, targetExtension);
            string fileContent = File.ReadAllText(filePath);

            return fileContent;
        }

        public static List<string> GetFileInfo(string fileContent, string targetExtension)
        {
            //string fileContent = LoadFile(folderPath, targetExtension);

            string pattern = @"FileTimestamp (\d{2}-\d{2}-\d{4} \d{2}:\d{2}:\d{2}); WaferID """"; LotID ""(.*?)""; DeviceID """"; ";
            Match match = Regex.Match(fileContent, pattern);

            string fileTimestamp = match.Groups[1].Value;
            string waferID = match.Groups[2].Value;
            string lotID = match.Groups[3].Value;
            string deviceID = match.Groups[3].Value;

            List<string> FileInfo = new List<string>();
            FileInfo.Add(fileTimestamp);
            FileInfo.Add(waferID);
            FileInfo.Add(lotID);
            FileInfo.Add(deviceID);

            return FileInfo;
        }
    }
}
