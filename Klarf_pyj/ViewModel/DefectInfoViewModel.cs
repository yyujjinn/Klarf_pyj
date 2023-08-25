using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Klarf
{
    class DefectInfoViewModel : INotifyPropertyChanged
    {
        #region [상수]
        string fileInfo;
        string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";
        string targetExtension = ".001";
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [필드]
        FileModel fileModel;

        #endregion

        #region [속성]
        //public string FileInfo
        //{
        //    get { return fileInfo; }
        //    set
        //    {
        //        fileInfo = value;
        //        OnPropertyChanged("FileInfo");
        //    }
        //}
        private string displayWaferID;
        public string DisplayWaferID
        {
            get { return displayWaferID; }
            set
            {
                if (displayWaferID != value)
                {
                    displayWaferID = value;
                    OnPropertyChanged("DisplayWaferID");
                }
            }
        }
        #endregion

        #region [생성자]
        public DefectInfoViewModel()
        {
            fileModel = new FileModel();
        }

        #endregion

        #region [메서드]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowFileInfo()
        {
            List<string> fileInfo = FileModel.GetFileInfo(folderPath,targetExtension);

            DisplayWaferID = fileInfo[1];
            //foreach (string fileItem in fileInfo)
            //{
            //    FileInfo += fileItem;
            //}
        }

        #endregion
    }
}
