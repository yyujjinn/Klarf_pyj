using System;
using System.Collections.Generic;
using System.IO;

namespace Klarf
{
    public class FileModel
    {
        #region [상수]
        public string filePath, fileData;
        
        #endregion

        #region [필드]
        List<string> fileList = new List<string>();

        #endregion

        #region [생성자]
        public FileModel()
        {

        }

        #endregion

        #region [메서드]

        //public string LoadFilePath()
        //{
        //    string filePath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf\Klarf Format.001";

        //    return filePath;
        //}

        //public (string, DateTime) LoadFileList(string filePath)
        //{
        //    FileInfo fileInfo = new FileInfo(filePath);

        //    string fileName = Path.GetFileName(filePath);
        //    DateTime fileDate = fileInfo.CreationTime;

        //    return (fileName, fileDate);
        //}

        //public string LoadFile(string filePath)
        //{
        //    string loadedFile = File.ReadAllText(filePath);

        //    return loadedFile;
        //}

        #endregion
    }
}
