using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;

namespace Klarf
{
    class FileListViewModel :INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        #region [필드]
        ObservableCollection<FileInfo> fileName;
        ObservableCollection<string> fileDate;
        ObservableCollection<FileItem> fileList;
        MainModel mainModel;
        #endregion

        #region [속성]
        public ICommand ShowFileListCommand { get; }
        public ICommand OpenFileCommand { get; }

        public ObservableCollection<FileItem> FileList
        {
            get { return fileList; }
            set
            {
                fileList = value;
                OnPropertyChanged("FileList");
            }
        }

        public ObservableCollection<FileInfo> FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        public ObservableCollection<string> FileDate
        {
            get { return fileDate; }
            set
            {
                fileDate = value;
                OnPropertyChanged("FileDate");
            }
        }

        #endregion

        #region [생성자]
        public FileListViewModel()
        {
            fileList = new ObservableCollection<FileItem>();
            mainModel = MainModel.Instance;
            OpenFileCommand = new RelayCommand<object>(OpenFile);
            ShowFileListCommand = new RelayCommand<object>(ShowFileList);

        }

        #endregion

        #region [메서드]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowFileList(object parameter)
        {
            var result = mainModel.GiveFileList();
            string name = result.Item1;
            DateTime date = result.Item2;

            FileItem fileItem = new FileItem { FileName = name, FileDate = date };
            FileList.Add(fileItem);
        }

        private void OpenFile(object parameter)
        {
            mainModel.GiveFileInfo();
            mainModel.GiveDefectList();
        }

        #endregion


        public class FileItem
        {
            public string FileName { get; set; }
            public DateTime FileDate { get; set; }
        }
    }
}
