using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klarf
{
    public class MainModel
    {
        #region [필드]
        private WaferModel waferModel;
        private DefectModel defectModel;
        private FileModel fileModel;
        private static MainModel instance = null;

        #endregion

        #region [속성]
        public static MainModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainModel();
                }



                return instance;
            }
        }

        public WaferModel Wafer
        {
            get { return waferModel; }
            set
            {
                if (waferModel != value)
                {
                    waferModel = value;
                    OnPropertyChanged("Wafer");
                }
            }
        }
        public DefectModel Defect
        {
            get { return defectModel; }
            set
            {
                if (defectModel != value)
                {
                    defectModel = value;
                    OnPropertyChanged("Defect");
                }
            }
        }

        public FileModel FileData
        {
            get { return fileModel; }

            set
            {
                if (fileModel != value)
                {
                    fileModel = value;
                    OnPropertyChanged("FileData");
                }
            }
        }

        #endregion

        #region [생성자]
        public MainModel()
        {
            
            this.fileModel = new FileModel();
            Wafer = new WaferModel();
            Defect = new DefectModel();
        }

        #endregion

        #region [메서드]
        public void LoadFile(string filePath)
        {
            FileData.fileData = File.ReadAllText(FileData.filePath);
        }


        public void LoadFilePath()
        {
            FileData.filePath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf\Klarf Format.001";

        }

        public (string, DateTime) LoadFileList()
        {
            LoadFilePath();
            FileInfo fileInfo = new FileInfo(FileData.filePath);

            string fileName = Path.GetFileName(FileData.filePath);
            DateTime fileDate = fileInfo.CreationTime;

            return (fileName, fileDate);
        }

        public void GetPartsWaferIndex(string filePath, int partIndex)
        {
            List<string> partsWaferIndex = new List<string>();
            bool startReading = false;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("SampleTestPlan"))
                    {
                        startReading = true;
                        continue;
                    }

                    if (startReading)
                    {
                        if (line.Contains("AreaPerTest"))
                        {
                            break;
                        }
                        string[] parts = line.Split(' ');
                        partsWaferIndex.Add(parts[partIndex]);
                    }
                }
            }
        }
        //public void GiveFilePath()
        //{
        //    string filePath = FileData.filePath;

        //    for (int i = 0; i < 2; i++)
        //    {
        //        GetPartsWaferIndex(filePath, i);
        //    }

        //    for (int i = 0; i < 2; i++)
        //    {
        //        GetPartsDefectList(filePath, i);
        //    }

        //}

        public (string, DateTime) GiveFileList()
        {
            string filePath = FileData.filePath;

            (string fileName, DateTime fileDate) = LoadFileList();

            return (fileName, fileDate);
        }

        public void GiveFileInfo()
        {
            StringBuilder combineInfo = new StringBuilder();

            string filePath = FileData.filePath;
            LoadFile(filePath);
            string fileContent = FileData.fileData;

            List<string> lines = new List<string>();

            using (StringReader reader = new StringReader(fileContent))
            {
                string line;

                WaferModel saveValue = new WaferModel();
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("FileTimestamp"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        saveValue.fileTimestamp = line;
                    }
                    else if (line.StartsWith("WaferID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        saveValue.waferID = line;
                    }
                    else if (line.StartsWith("LotID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        saveValue.lotID = line;
                    }
                    else if (line.StartsWith("DeviceID"))
                    {
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        line = line.TrimEnd(';');
                        saveValue.deviceID = line;
                    }
                    else
                    {
                        continue;
                    }
                }
                Instance.Wafer = saveValue;
            }
        }

        public List<string> GetPartsDefectList(int partIndex)
        {
            string filePath = FileData.filePath;
            LoadFile(filePath);
            string fileContent = FileData.fileData;

            List<string> partsDefectList = new List<string>();
            bool startReading = false;
            int lineCounter = 0;

            using (StringReader reader = new StringReader(fileContent))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {

                    if (line.Contains("DefectList"))
                    {
                        startReading = true;
                        continue;
                    }

                    if (startReading)
                    {
                        lineCounter++;

                        if (line.Contains("SummarySpec"))
                        {
                            break;
                        }

                        if (lineCounter % 2 == 1)
                        {
                            string[] parts = line.Split(' ');
                            partsDefectList.Add(parts[partIndex]);
                        }
                    }
                }
            }
            return partsDefectList;
        }

        public void GiveDefectList()
        {
            DefectModel saveValue = new DefectModel();

            saveValue.defectID = GetPartsDefectList(0);
            saveValue.xRel = GetPartsDefectList(1);
            saveValue.yRel = GetPartsDefectList(2);
            saveValue.xIndex = GetPartsDefectList(3);
            saveValue.yIndex = GetPartsDefectList(4);
            saveValue.xSize = GetPartsDefectList(5);
            saveValue.ySize = GetPartsDefectList(6);
            saveValue.defectArea = GetPartsDefectList(7);
            saveValue.dSize = GetPartsDefectList(8);
            saveValue.classNumber = GetPartsDefectList(9);
            saveValue.test = GetPartsDefectList(10);
            saveValue.clusterNumber = GetPartsDefectList(11);
            saveValue.roughBinNumber = GetPartsDefectList(12);
            saveValue.fineBinNumber = GetPartsDefectList(13);
            saveValue.reviewSample = GetPartsDefectList(14);
            saveValue.imageCount = GetPartsDefectList(15);

            Instance.Defect = saveValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
