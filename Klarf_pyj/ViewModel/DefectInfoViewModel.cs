using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Klarf
{
    //public partial class Test : UserControl
    //{
    //    DefectInfoViewModel defectInfoViewModel = new DefectInfoViewModel();

    //    public Test()
    //    {
    //        InitializeComponent();
    //        this.DataContext = defectInfoViewModel;
    //    }

    //    public void ShowFileInfo()
    //    {
    //        defectInfoViewModel.ShowFileInfo();
    //    }
    //}

    public class DefectInfoViewModel : INotifyPropertyChanged
    {
        #region [상수]
        string fileInfo;
        string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";
        string targetExtension = ".001";
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region [필드]
        private IFileModel fileModel;
        //private DefectModel defectModel;
        private string displayFileInfo;
        //private ObservableCollection<DefectItem> defectList;
        private ObservableCollection<string> defectID;

        #endregion

        #region [속성]
        //public FileModel SelectedFile
        //{
        //    get { return selectedFile; }
        //    set
        //    {
        //        selectedFile = value;
        //        //ShowFileInfo();
        //        this.OnPropertyChanged("SelectedFile");
        //    }
        //}

        public string DisplayFileInfo
        {
            get { return displayFileInfo; }
            set
            {
                if (displayFileInfo != value)
                {
                    displayFileInfo = value;
                    OnPropertyChanged(nameof(DisplayFileInfo));
                }
            }
        }
        //public ObservableCollection<DefectItem> DefectList
        //{
        //    get { return defectList; }
        //    set
        //    {
        //        defectList = value;
        //        OnPropertyChanged("DefectList");
        //    }
        //}

        //public ObservableCollection<string> DefectID
        //{
        //    get { return defectID; }
        //    set
        //    {
        //        defectID = value;
        //        OnPropertyChanged("DefectID");
        //    }
        //}

        #endregion

        #region [생성자]
        public DefectInfoViewModel()
        {
            fileModel = new FileModel();
            //defectModel = new DefectModel();
        }

        #endregion

        #region [메서드]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;

        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        public void ShowFileInfo()
        {
            List<string> fileInfo = fileModel.GetFileInfo(@"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf\Klarf Format.001");

            string combinedInfo = string.Join("\n", fileInfo);
            DisplayFileInfo = combinedInfo;
        }

        //public void ShowDefectList()
        //{
        //    //DefectList.Clear();

        //    List<string> defectID = defectModel.GetDefectID(@"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf\Klarf Format.001");

        //    for (int i = 0; i < defectID.Count; i++)
        //    {
        //        DefectList.Add(new DefectItem
        //        {
        //            DefectID = defectID[i]
        //        });
        //    }
        //}

        #endregion

        #region [종속 클래스]
        //public class DefectItem
        //{
        //    public string DefectID { get; set; }
        //    public string XRel { get; set; }
        //    public string YRel { get; set; }
        //    public string XIndex { get; set; }
        //    public string YIndex { get; set; }
        //    public string XSize { get; set; }
        //    public string YSize { get; set; }
        //    public string DefectArea { get; set; }
        //    public string DSize { get; set; }
        //    public string ClassNumber { get; set; }
        //    public string Test { get; set; }
        //    public string ClusterNumber { get; set; }
        //    public string RoughBinNumber { get; set; }
        //    public string FineBinNumber { get; set; }
        //    public string ImageCount { get; set; }
        //}

        #endregion
    }
}
