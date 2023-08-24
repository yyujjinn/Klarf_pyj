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
        FileModel fileModel;

        #endregion

        #region [속성]
        public ICommand ShowFileListCommand { get; }
        public ICommand OpenFileCommand { get; }

        public ObservableCollection<FileInfo> FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        #endregion

        #region [생성자]
        public FileListViewModel()
        {
            fileName = new ObservableCollection<FileInfo>();
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
            FileName.Clear();
            fileModel.LoadFileList();

            List<FileInfo> fileNames = fileModel.LoadFileList();

            foreach (FileInfo fileName in fileNames)
            {
                FileName.Add(fileName);
            }
        }

        public void OpenFile()
        {

        }



        #endregion
    }
}
