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
            saveDefect = new DefectModel();
            saveWafer = new WaferModel();
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

            saveWafer.xIndex = GetPartsDieIndex(0);
            saveWafer.yIndex = GetPartsDieIndex(1);

            saveWafer.xIndices = new List<int>();
            saveWafer.yIndices = new List<int>();

            foreach (string xIndexString in saveWafer.xIndex)
            {
                if (int.TryParse(xIndexString, out int xIndex))
                {

                    saveWafer.xIndices.Add(xIndex);
                }
            }

            foreach (string yIndexString in saveWafer.yIndex)
            {
                if (int.TryParse(yIndexString, out int yIndex))
                {
                    saveWafer.yIndices.Add(yIndex);
                }
            }

            //int xMin = saveValue.xIndices.Min();
            //int xMax = saveValue.xIndices.Max();
            //int yMax = saveValue.yIndices.Max();

            //saveValue.dieIndex = new List<Point>();

            //for (int i = 0; i < saveValue.xIndex.Count; i++)
            //{
            //    if (i < saveValue.xIndices.Count && i < saveValue.yIndices.Count) // 범위 확인
            //    {
            //        int x = saveValue.xIndices[i] - xMin;
            //        int y = Math.Abs(saveValue.yIndices[i] - yMax);

            //        saveValue.dieIndex.Add(new Point(x, y));
            //    }
            //}

            //int[] xCounts = new int[50];
            //int[] yCounts = new int[50];

            //foreach (int yIndex in saveValue.yIndices)
            //{
            //    if (yIndex >= 0 && yIndex < 50)
            //    {
            //        xCounts[yIndex]++;
            //    }
            //}

            //foreach (int xIndex in saveValue.xIndices)
            //{
            //    if (xIndex >= 0 && xIndex < 50)
            //    {
            //        yCounts[xIndex]++;
            //    }
            //}

            //int xCount = xCounts.Max();
            //int yCount = yCounts.Max();

            //saveValue.width = 400 / xCount;
            //saveValue.height = 400 / yCount;

            Instance.Wafer = saveWafer;
        }

        public void GiveDieSize()
        {
            //WaferModel saveValue = new WaferModel();

            //int[] xCounts = new int[50];
            //int[] yCounts = new int[50];

            //foreach (int yIndex in saveValue.yIndices)
            //{
            //    if (yIndex >= 0 && yIndex < 50)
            //    {
            //        xCounts[yIndex]++;
            //    }
            //}

            //foreach (int xIndex in saveValue.xIndices)
            //{
            //    if (xIndex >= 0 && xIndex < 50)
            //    {
            //        yCounts[xIndex]++;
            //    }
            //}

            //int xCount = xCounts.Max();
            //int yCount = yCounts.Max();

            //saveValue.width = 400 / xCount;
            //saveValue.height = 400 / yCount;

            //Instance.Wafer = saveValue;
        }

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

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("FileTimestamp"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        saveDefect.fileTimestamp = line;
                    }
                    else if (line.StartsWith("WaferID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        saveDefect.waferID = line;
                    }
                    else if (line.StartsWith("LotID"))
                    {
                        line = line.TrimEnd(';');
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        saveDefect.lotID = line;
                    }
                    else if (line.StartsWith("DeviceID"))
                    {
                        string[] parts = line.Split(' ');
                        line = string.Join(" ", parts);
                        line = line.TrimEnd(';');
                        saveDefect.deviceID = line;
                    }
                    else
                    {
                        continue;
                    }
                }
                GiveDieIndex();
                Instance.Defect = saveDefect;
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

        public void GiveDefectList()
        {
            saveDefect.defectID = GetPartsDefectList(0);
            saveDefect.xRel = GetPartsDefectList(1);
            saveDefect.yRel = GetPartsDefectList(2);
            saveDefect.xIndex = GetPartsDefectList(3);
            saveDefect.yIndex = GetPartsDefectList(4);
            saveDefect.xSize = GetPartsDefectList(5);
            saveDefect.ySize = GetPartsDefectList(6);
            saveDefect.defectArea = GetPartsDefectList(7);
            saveDefect.dSize = GetPartsDefectList(8);
            saveDefect.classNumber = GetPartsDefectList(9);
            saveDefect.test = GetPartsDefectList(10);
            saveDefect.clusterNumber = GetPartsDefectList(11);
            saveDefect.roughBinNumber = GetPartsDefectList(12);
            saveDefect.fineBinNumber = GetPartsDefectList(13);
            saveDefect.reviewSample = GetPartsDefectList(14);
            saveDefect.imageCount = GetPartsDefectList(15);

            Instance.Defect = saveDefect;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
