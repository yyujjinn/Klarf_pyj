using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System;

namespace Klarf
{
    public class FileListViewModel : INotifyPropertyChanged
    {
        #region [상수]
        string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";
        string targetExtension = ".001";
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [필드]
        ObservableCollection<FileInfo> fileName;
        ObservableCollection<string> fileDate;
        ObservableCollection<FileItem> fileList;
        private FileItem selectedFile;
        private IFileModel fileModel;
        private IFileModel _fileModel;
        DefectInfoViewModel defectInfoViewModel;

        #endregion

        #region [속성]
        public ICommand ShowFileListCommand { get; }
        public ICommand SelectFileCommand { get; }

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

        public FileItem SelectedFile
        {
            get { return selectedFile; }
            set
            {
                if (selectedFile != value)
                {
                    selectedFile = value;
                    OnPropertyChanged("SelectedFile");
                    //FileModel.LoadFile(Path.Combine(folderPath, selectedFile.FileName), targetExtension);
                    //SelectFile();
                    defectInfoViewModel.ShowFileInfo();
                }
            }
        }

        #endregion

        #region [생성자]
        public FileListViewModel()
        {
            fileModel = new FileModel();
            fileName = new ObservableCollection<FileInfo>();
            fileDate = new ObservableCollection<string>();
            fileList = new ObservableCollection<FileItem>();
            ShowFileListCommand = new RelayCommand<object>(ShowFileList);
            defectInfoViewModel = MainViewModel.defectInfoViewModel; //new DefectInfoViewModel();
        }

        public FileListViewModel(IFileModel fileModel)
        {
            _fileModel = fileModel;
            fileName = new ObservableCollection<FileInfo>();
            fileDate = new ObservableCollection<string>();
            fileList = new ObservableCollection<FileItem>();
            ShowFileListCommand = new RelayCommand<object>(ShowFileList);
            defectInfoViewModel = MainViewModel.defectInfoViewModel; //new DefectInfoViewModel();
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

        public void SelectFile()
        {
            //FileModel fileModel = new FileModel();
            //DefectInfoViewModel defectInfoViewModel = new DefectInfoViewModel();

            //string loadedFile = _fileModel.LoadFile(Path.Combine(folderPath, selectedFile.FileName), targetExtension);
            //List<string> loadedFileContent = fileModel.GetFileInfo(Path.Combine(folderPath, selectedFile.FileName));

            //defectInfoViewModel.ShowFileInfo();
            ////defectInfoViewModel.ShowDefectList();
        }


        public class FileItem
        {
            public string FileName { get; set; }
            public string FileDate { get; set; }
        }

        #endregion
    }
}
