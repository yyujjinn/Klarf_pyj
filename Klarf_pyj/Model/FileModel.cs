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


        #region [메서드]
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

        public List<string> GetFileInfo(string filePath)
        {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("FileTimestamp"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        lines.Add(line);
                    }
                    else if (line.StartsWith("WaferID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        lines.Add(line);
                    }
                    else if (line.StartsWith("LotID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        lines.Add(line);
                    }
                    else if (line.StartsWith("DeviceID"))
                    {
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        line = line.TrimEnd(';');
                        lines.Add(line);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return lines;
        }
        #endregion
    }
}
