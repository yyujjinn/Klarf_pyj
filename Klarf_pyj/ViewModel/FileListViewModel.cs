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
        private ObservableCollection<string> fileName = new ObservableCollection<string>();
        private FileModel fileModel;

        #endregion

        #region [속성]
        public ICommand OpenFileCommand { get; }

        public ObservableCollection<string> FileName
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
            fileModel = new FileModel();
            //OpenFileCommand = new RelayCommand<object>(OpenFile);
        }

        #endregion

        #region [메서드]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowFileList()
        {
            string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";

            string[] fileNames = Directory.GetFiles(folderPath);
            foreach (var fileName in fileNames)
            {
                string fileEntry = Path.GetFileName(fileName);
                FileName.Add(fileEntry);
            }
        }

        public void OpenFile()
        {

        }



        #endregion
    }
}
