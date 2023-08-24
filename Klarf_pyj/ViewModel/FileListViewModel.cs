using Klarf_pyj.Model;
using Klarf_pyj.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Klarf_pyj.ViewModel
{
    class FileListViewModel : INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [필드]
        ObservableCollection<FileInfo> fileName;
        ObservableCollection<string> fileDate;
        ObservableCollection<FileItem> fileList;
        FileModel fileModel;

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
            fileName = new ObservableCollection<FileInfo>();
            fileDate = new ObservableCollection<string>();
            fileList = new ObservableCollection<FileItem>();
            ShowFileListCommand = new RelayCommand<object>(ShowFileList);
            fileModel = new FileModel();
            //OpenFileCommand = new RelayCommand<object>(OpenFile);
        }

        #endregion

        #region [메서드]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowFileList(object parameter)
        {
            FileList.Clear();

            List<FileInfo> fileNames = fileModel.LoadFileList();
            List<string> fileDates = fileModel.LoadFileDateList();


            for (int i = 0; i < fileNames.Count; i++)
            {
                FileList.Add(new FileItem
                {
                    FileName = fileNames[i].Name,
                    FileDate = fileDates[i]
                });
            }
        }

        public void OpenFile()
        {

        }


        public class FileItem
        {
            public string FileName { get; set; }
            public string FileDate { get; set; }
        }

        #endregion
    }
}
