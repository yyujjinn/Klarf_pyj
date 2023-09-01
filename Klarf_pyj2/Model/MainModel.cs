﻿using System;
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

        public DefectModel DefectDie
        {
            get { return saveDefect; }
            set
            {
                if (saveDefect != value)
                {
                    saveDefect = value;
                    OnPropertyChanged("DefectDie");
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

        private int[] CountIndices(List<int> indices)
        {
            int[] counts = new int[50];
            foreach (int index in indices)
            {
                if (index >= 0 && index < 50)
                {
                    counts[index]++;
                }
            }
            return counts;
        }

        private int GetMaxCount(int[] counts)
        {
            return counts.Max();
        }

        private void SetWaferDimensions(WaferModel wafer, int xCount, int yCount)
        {
            wafer.width = 400 / xCount;
            wafer.height = 400 / yCount;
        }

        public void GiveDieIndex()
        {
            WaferModel newWafer = new WaferModel();

            newWafer.xIndex = GetPartsDieIndex(0);
            newWafer.yIndex = GetPartsDieIndex(1);

            newWafer.xIndices = ExtractIndices(newWafer.xIndex);
            newWafer.yIndices = ExtractIndices(newWafer.yIndex);

            int[] xCounts = CountIndices(newWafer.yIndices);
            int[] yCounts = CountIndices(newWafer.xIndices);

            SetWaferDimensions(newWafer, GetMaxCount(xCounts), GetMaxCount(yCounts));

            Instance.Wafer = newWafer;
        }

        private List<int> ExtractIndices(List<string> indexStrings)
        {
            List<int> indices = new List<int>();
            foreach (string indexString in indexStrings)
            {
                if (int.TryParse(indexString, out int index))
                {
                    indices.Add(index);
                }
            }
            return indices;
        }

        public void GiveDefectIndex()
        {
            DefectModel newDefect = new DefectModel();

            newDefect.xIndex = GetPartsDefectList(3);
            newDefect.yIndex = GetPartsDefectList(4);

            newDefect.xIndices = ExtractIndices(newDefect.xIndex);
            newDefect.yIndices = ExtractIndices(newDefect.yIndex);

            Instance.DefectDie = newDefect;
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

                GetDefectList(newDefect);
                Instance.Defect = newDefect;
            }
        }

        private void GetDefectList(DefectModel defect)
        {
            defect.defectID = GetPartsDefectList(0);
            defect.xRel = GetPartsDefectList(1);
            defect.yRel = GetPartsDefectList(2);
            defect.xIndex = GetPartsDefectList(3);
            defect.yIndex = GetPartsDefectList(4);
            defect.xSize = GetPartsDefectList(5);
            defect.ySize = GetPartsDefectList(6);
            defect.defectArea = GetPartsDefectList(7);
            defect.dSize = GetPartsDefectList(8);
            defect.classNumber = GetPartsDefectList(9);
            defect.test = GetPartsDefectList(10);
            defect.clusterNumber = GetPartsDefectList(11);
            defect.roughBinNumber = GetPartsDefectList(12);
            defect.fineBinNumber = GetPartsDefectList(13);
            defect.reviewSample = GetPartsDefectList(14);
            defect.imageCount = GetPartsDefectList(15);
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
