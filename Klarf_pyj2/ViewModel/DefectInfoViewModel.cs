using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace Klarf
{
    class DefectInfoViewModel : INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [필드]
        ObservableCollection<DefectItem> defectList;
        ObservableCollection<string> defectID;
        ObservableCollection<string> xRel;
        ObservableCollection<string> yRel;
        ObservableCollection<string> xIndex;
        ObservableCollection<string> yIndex;
        ObservableCollection<string> xSize;
        ObservableCollection<string> ySize;
        ObservableCollection<string> defectArea;
        ObservableCollection<string> dSize;
        ObservableCollection<string> classNumber;
        ObservableCollection<string> test;
        ObservableCollection<string> clusterNumber;
        ObservableCollection<string> roughBinNumber;
        ObservableCollection<string> fineBinNumber;
        ObservableCollection<string> reviewSample;
        ObservableCollection<string> imageCount;

        private string displayFileInfo;
        MainModel mainModel;

        #endregion

        #region [속성]
        public MainModel MainModel
        {
            get { return mainModel; }

            set
            {
                if (mainModel != value)
                {
                    if (mainModel != null)
                    {
                        mainModel.PropertyChanged -= MainModel_PropertyChanged;
                    }

                    mainModel = value;

                    if (mainModel != null)
                    {
                        mainModel.PropertyChanged += MainModel_PropertyChanged;
                    }
                    OnPropertyChanged("MainModel");
                }
            }
        }

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

        public ObservableCollection<DefectItem> DefectList
        {
            get { return defectList; }
            set
            {
                defectList = value;
                OnPropertyChanged("DefectList");
            }
        }

        public ObservableCollection<string> DefectID
        {
            get { return defectID; }
            set
            {
                defectID = value;
                OnPropertyChanged("DefectID");
            }
        }

        public ObservableCollection<string> XRel
        {
            get { return xRel; }
            set
            {
                xRel = value;
                OnPropertyChanged("XRel");
            }
        }

        public ObservableCollection<string> YRel
        {
            get { return yRel; }
            set
            {
                yRel = value;
                OnPropertyChanged("YRel");
            }
        }

        public ObservableCollection<string> XIndex
        {
            get { return xIndex; }
            set
            {
                xIndex = value;
                OnPropertyChanged("XIndex");
            }
        }

        public ObservableCollection<string> YIndex
        {
            get { return yIndex; }
            set
            {
                yIndex = value;
                OnPropertyChanged("YIndex");
            }
        }

        public ObservableCollection<string> XSize
        {
            get { return xSize; }
            set
            {
                xSize = value;
                OnPropertyChanged("XSize");
            }
        }

        public ObservableCollection<string> YSize
        {
            get { return ySize; }
            set
            {
                ySize = value;
                OnPropertyChanged("YSize");
            }
        }

        public ObservableCollection<string> DefectArea
        {
            get { return defectArea; }
            set
            {
                defectArea = value;
                OnPropertyChanged("DefectArea");
            }
        }

        public ObservableCollection<string> DSize
        {
            get { return dSize; }
            set
            {
                dSize = value;
                OnPropertyChanged("DSize");
            }
        }

        public ObservableCollection<string> ClassNumber
        {
            get { return classNumber; }
            set
            {
                classNumber = value;
                OnPropertyChanged("ClassNumber");
            }
        }

        public ObservableCollection<string> Test
        {
            get { return test; }
            set
            {
                test = value;
                OnPropertyChanged("Test");
            }
        }

        public ObservableCollection<string> ClusterNumber
        {
            get { return clusterNumber; }
            set
            {
                clusterNumber = value;
                OnPropertyChanged("ClusterNumber");
            }
        }

        public ObservableCollection<string> RoughBinNumber
        {
            get { return roughBinNumber; }
            set
            {
                roughBinNumber = value;
                OnPropertyChanged("RoughBinNumber");
            }
        }

        public ObservableCollection<string> FineBinNumber
        {
            get { return fineBinNumber; }
            set
            {
                fineBinNumber = value;
                OnPropertyChanged("FineBinNumber");
            }
        }

        public ObservableCollection<string> ReviewSample
        {
            get { return reviewSample; }
            set
            {
                reviewSample = value;
                OnPropertyChanged("ReviewSample");
            }
        }

        public ObservableCollection<string> ImageCount
        {
            get { return imageCount; }
            set
            {
                imageCount = value;
                OnPropertyChanged("ImageCount");
            }
        }

        #endregion

        #region [생성자]
        public DefectInfoViewModel()
        {
            MainModel.Instance.PropertyChanged += MainModel_PropertyChanged;
            MainModel = MainModel.Instance;
            defectList = new ObservableCollection<DefectItem>();
            defectID = new ObservableCollection<string>();
            xRel = new ObservableCollection<string>();
            yRel = new ObservableCollection<string>();
            xIndex = new ObservableCollection<string>() ;
            yIndex = new ObservableCollection<string>() ;
            xSize = new ObservableCollection<string>() ;
            ySize = new ObservableCollection<string>() ;
            defectArea = new ObservableCollection<string>() ;
            dSize = new ObservableCollection<string>() ;
            classNumber = new ObservableCollection<string>() ;
            test = new ObservableCollection<string>() ;
            clusterNumber = new ObservableCollection<string>() ;
            roughBinNumber = new ObservableCollection<string>() ;
            fineBinNumber = new ObservableCollection<string>() ;
            reviewSample = new ObservableCollection<string>() ;
            imageCount = new ObservableCollection<string>() ;

        }

        #endregion

        #region [메서드]
        private void MainModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Defect")
            {
                ShowFileInfo();
                ShowDefecList();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowFileInfo()
        {
            StringBuilder fileInfo = new StringBuilder();

            fileInfo.AppendLine("\n" + mainModel.Defect.fileTimestamp + "\n");
            fileInfo.AppendLine(mainModel.Defect.waferID + "\n");
            fileInfo.AppendLine(mainModel.Defect.lotID + "\n");
            fileInfo.AppendLine(mainModel.Defect.deviceID);
            //string combinedInfo = string.Join("\n", mainModel.Wafer);
            DisplayFileInfo = fileInfo.ToString();
        }

        private void ShowDefecList()
        {
            List<string> defectIDs = mainModel.Defect.defectID;
            List<string> xRels = mainModel.Defect.xRel;
            List< string > yRels = mainModel.Defect.yRel;
            List<string> xIndexs = mainModel.Defect.xIndex;
            List< string > yIndexs = mainModel.Defect.yIndex;
            List<string> xSizes = mainModel.Defect.xSize;
            List< string > ySizes = mainModel.Defect.ySize;
            List<string> defectAreas = mainModel.Defect.defectArea;
            List< string > dSizes = mainModel.Defect.dSize;
            List<string> classNumbers = mainModel.Defect.classNumber;
            List< string > tests = mainModel.Defect.test;
            List<string> clusterNumbers = mainModel.Defect.clusterNumber;
            List< string > roughBinNumbers = mainModel.Defect.roughBinNumber;
            List<string> fineBinNumbers = mainModel.Defect.fineBinNumber;
            List< string > reviewSamples = mainModel.Defect.reviewSample;
            List<string> imageCounts = mainModel.Defect.imageCount;

            for (int i = 0; i < defectIDs.Count; i++)
            {
                DefectList.Add(new DefectItem
                {
                    DefectID = defectIDs[i],
                    XRel = xRels[i],
                    YRel = yRels[i],
                    XIndex = xIndexs[i],
                    YIndex = yIndexs[i],
                    XSize = xSizes[i],
                    YSize = ySizes[i],
                    DefectArea = defectAreas[i],
                    DSize = dSizes[i],
                    ClassNumber = classNumbers[i],
                    Test = tests[i],
                    ClusterNumber = clusterNumbers[i],
                    RoughBinNumber = roughBinNumbers[i],
                    FineBinNumber = fineBinNumbers[i],
                    ReviewSample = reviewSamples[i],
                    ImageCount = imageCounts[i]
                });
            }
        }

        #endregion

        #region [종속 클래스]
        public class DefectItem
        {
            public string DefectID { get; set; }
            public string XRel { get; set; }
            public string YRel { get; set; }
            public string XIndex { get; set; }
            public string YIndex { get; set; }
            public string XSize { get; set; }
            public string YSize { get; set; }
            public string DefectArea { get; set; }
            public string DSize { get; set; }
            public string ClassNumber { get; set; }
            public string Test { get; set; }
            public string ClusterNumber { get; set; }
            public string RoughBinNumber { get; set; }
            public string FineBinNumber { get; set; }
            public string ReviewSample { get; set; }
            public string ImageCount { get; set; }
        }

        #endregion
    }
}



        //public List<string> GetDefectList(string filePath)
        //{
        //    List<string> defectID = GetPartsDefectList(filePath, 0);
        //    List<string> XRel = GetPartsDefectList(filePath, 1);
        //    List<string> YRel = GetPartsDefectList(filePath, 2);
        //    List<string> XIndex = GetPartsDefectList(filePath, 3);
        //    List<string> YIndex = GetPartsDefectList(filePath, 4);
        //    List<string> XSize = GetPartsDefectList(filePath, 5);
        //    List<string> YSize = GetPartsDefectList(filePath, 6);
        //    List<string> DefectArea = GetPartsDefectList(filePath, 7);
        //    List<string> DSize = GetPartsDefectList(filePath, 8);
        //    List<string> ClassNumber = GetPartsDefectList(filePath, 9);
        //    List<string> Test = GetPartsDefectList(filePath, 10);
        //    List<string> ClusterNumber = GetPartsDefectList(filePath, 11);
        //    List<string> RoughBinNumber = GetPartsDefectList(filePath, 12);
        //    List<string> FineBinNumber = GetPartsDefectList(filePath, 13);
        //    List<string> ReviewSample = GetPartsDefectList(filePath, 14);
        //    List<string> ImageCount = GetPartsDefectList(filePath, 15);

        //    List<string> defectList = new List<string>();

        //    for (int i = 0; i < ImageCount.Count; i++)
        //    {

        //    }
        //}