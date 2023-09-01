using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Klarf
{
    public class MainModel
    {
        #region [필드]
        private WaferModel saveWafer;
        private DefectModel saveDefect;
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
            get { return saveWafer; }
            set
            {
                if (saveWafer != value)
                {
                    saveWafer = value;
                    OnPropertyChanged("Wafer");
                }
            }
        }
        public DefectModel Defect
        {
            get { return saveDefect; }
            set
            {
                if (saveDefect != value)
                {
                    saveDefect = value;
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
            Defect = new DefectModel();
            Wafer = new WaferModel();
            //xIndices = new List<int>();
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

        public List<string> GetPartsDieIndex(int partIndex)
        {
            string filePath = FileData.filePath;
            LoadFile(filePath);
            string fileContent = FileData.fileData;

            List<string> partsDieIndex = new List<string>();
            bool startReading = false;

            using (StringReader reader = new StringReader(fileContent))
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
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        partsDieIndex.Add(parts[partIndex]);
                    }
                }
            }
            return partsDieIndex;
        }

        public void GiveDieIndex ()
        {
            WaferModel newWafer = new WaferModel();

            newWafer.xIndex = GetPartsDieIndex(0);
            newWafer.yIndex = GetPartsDieIndex(1);

            newWafer.xIndices = new List<int>();
            newWafer.yIndices = new List<int>();

            foreach (string xIndexString in newWafer.xIndex)
            {
                if (int.TryParse(xIndexString, out int xIndex))
                {

                    newWafer.xIndices.Add(xIndex);
                }
            }

            foreach (string yIndexString in newWafer.yIndex)
            {
                if (int.TryParse(yIndexString, out int yIndex))
                {
                    newWafer.yIndices.Add(yIndex);
                }
            }

            int[] xCounts = new int[50];
            int[] yCounts = new int[50];

            foreach (int yIndex in newWafer.yIndices)
            {
                if (yIndex >= 0 && yIndex < 50)
                {
                    xCounts[yIndex]++;
                }
            }

            foreach (int xIndex in newWafer.xIndices)
            {
                if (xIndex >= 0 && xIndex < 50)
                {
                    yCounts[xIndex]++;
                }
            }

            int xCount = xCounts.Max();
            int yCount = yCounts.Max();

            newWafer.width = 400 / xCount;
            newWafer.height = 400 / yCount;

            Instance.Wafer = newWafer;
        }

        public (string, DateTime) GiveFileList()
        {
            string filePath = FileData.filePath;

            (string fileName, DateTime fileDate) = LoadFileList();

            return (fileName, fileDate);
        }

        public void GiveFileInfo()
        {
            DefectModel newDefect = new DefectModel();

            StringBuilder combineInfo = new StringBuilder();

            string filePath = FileData.filePath;
            LoadFile(filePath);
            string fileContent = FileData.fileData;

            List<string> lines = new List<string>();

            using (StringReader reader = new StringReader(fileContent))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("FileTimestamp"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        newDefect.fileTimestamp = line;
                    }
                    else if (line.StartsWith("WaferID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        newDefect.waferID = line;
                    }
                    else if (line.StartsWith("LotID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        newDefect.lotID = line;
                    }
                    else if (line.StartsWith("DeviceID"))
                    {
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        line = line.TrimEnd(';');
                        newDefect.deviceID = line;
                    }
                    else
                    {
                        continue;
                    }
                }

                newDefect.defectID = GetPartsDefectList(0);
                newDefect.xRel = GetPartsDefectList(1);
                newDefect.yRel = GetPartsDefectList(2);
                newDefect.xIndex = GetPartsDefectList(3);
                newDefect.yIndex = GetPartsDefectList(4);
                newDefect.xSize = GetPartsDefectList(5);
                newDefect.ySize = GetPartsDefectList(6);
                newDefect.defectArea = GetPartsDefectList(7);
                newDefect.dSize = GetPartsDefectList(8);
                newDefect.classNumber = GetPartsDefectList(9);
                newDefect.test = GetPartsDefectList(10);
                newDefect.clusterNumber = GetPartsDefectList(11);
                newDefect.roughBinNumber = GetPartsDefectList(12);
                newDefect.fineBinNumber = GetPartsDefectList(13);
                newDefect.reviewSample = GetPartsDefectList(14);
                newDefect.imageCount = GetPartsDefectList(15);

                Instance.Defect = newDefect;
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
                            line = line.TrimEnd(';');
                            string[] parts = line.Split(' ');
                            partsDefectList.Add(parts[partIndex]);
                        }
                    }
                }
            }
            return partsDefectList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
