using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        private int currentImageIndex;

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

        public DefectModel DefectImage
        {
            get { return saveDefect; }
            set
            {
                if (saveDefect != value)
                {
                    saveDefect = value;
                    OnPropertyChanged(nameof(DefectImage));
                    CurrentImageIndex = saveDefect.currentImageIndex;
                }
            }
        }

        public DefectModel NextDefectImage
        {
            get { return saveDefect; }
            set
            {
                if (saveDefect != value)
                {
                    saveDefect = value;
                    OnPropertyChanged("NextDefectImage");
                }
            }
        }

        public int CurrentImageIndex
        {
            get { return currentImageIndex; }
            set
            {
                if (currentImageIndex != value)
                {
                    currentImageIndex = value;
                    OnPropertyChanged(nameof(CurrentImageIndex));
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
            fileModel = new FileModel();
            Defect = new DefectModel();
            Wafer = new WaferModel();
        }

        #endregion

        #region [메서드]
        /**
        * @brief 파일 로드  
        * @param filePath : 로드할 파일 경로
        * 2023-09-12|박유진|
        */
        public void LoadFile(string filePath)
        {
            FileData.fileData = File.ReadAllText(FileData.filePath);
        }

        /**
        * @brief 파일 경로 설정
        * 2023-09-12|박유진|
        */
        public void LoadFilePath()
        {
            FileData.filePath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf\Klarf Format.001";
        }

        /**
        * @brief 파일 목록 로드 
        * @return (파일 이름, 파일 생성 일자)
        * 2023-09-12|박유진|
        */
        public (string, DateTime) LoadFileList()
        {
            LoadFilePath();
            FileInfo fileInfo = new FileInfo(FileData.filePath);

            string fileName = Path.GetFileName(FileData.filePath);
            DateTime fileDate = fileInfo.CreationTime;

            return (fileName, fileDate);
        }

        /**
        * @brief Die 인덱스 추출, 리스트 반환 
        * @param partIndex : 추출할 인덱스의 열 번호
        * @return partsDieIndex : Die 인덱스 목록
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief 주어진 인덱스 목록의 개수를 세어 배열로 반환
        * @param indices : 개수를 세고자하는 인덱스 목록
        * @return counts : 인덱스 개수 배열
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief 배열에서 가장 큰 값 반환  
        * @param counts : 배열
        * @return 배열에서 가장 큰 값
        * 2023-09-12|박유진|
        */
        private int GetMaxCount(int[] counts)
        {
            return counts.Max();
        }

        /**
        * @brief WaferModel의 너비와 높이 설정
        * @param wafer : WaferModel 인스턴스
        * @param xCount : X 축 개수
        * @param yCount : Y 축 개수
        * 2023-09-12|박유진|
        */
        private void SetWaferDimensions(WaferModel wafer, int xCount, int yCount)
        {
            wafer.width = 400 / xCount;
            wafer.height = 400 / yCount;
        }

        /**
        * @brief Die 인덱스 추출, WaferModel에 설정
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief 주어진 문자열 목록에서 정수 인덱스를 추출하여 리스트로 반환
        * @param indexStrings : 정수 인덱스가 포함된 문자열 목록
        * @return indices : 추출된 정수 인덱스 목록
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief Defect 인덱스를 추출, DefectModel에 설정
        * 2023-09-12|박유진|
        */
        public void GiveDefectIndex()
        {
            DefectModel newDefect = new DefectModel();

            newDefect.xIndex = GetPartsDefectList(3);
            newDefect.yIndex = GetPartsDefectList(4);

            newDefect.xIndices = ExtractIndices(newDefect.xIndex);
            newDefect.yIndices = ExtractIndices(newDefect.yIndex);

            Instance.DefectDie = newDefect;
        }

        /**
        * @brief 파일 목록 로드, 파일 이름과 생성 일자 반환
        * @return (파일 이름, 파일 생성 일자)
        * 2023-09-12|박유진|
        */
        public (string, DateTime) GiveFileList()
        {
            string filePath = FileData.filePath;

            (string fileName, DateTime fileDate) = LoadFileList();

            return (fileName, fileDate);
        }


        /**
        * @brief 파일 정보 로드, DefectModel에 설정
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief Defect 정보 추출, DefectModel에 설정
        * @param defect : DefectModel 인스턴스
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief Defect 정보 추출, 문자열 목록으로 반환
        * @param partIndex : 추출할 정보의 열 번호
        * @return partsDefectList : 문자열 목록으로 된 Defect 정보
        * 2023-09-12|박유진|
        */
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

        /**
        * @brief Tiff 파일 로드 DefectModel에 현재 이미지 인덱스 설정
        * 2023-09-12|박유진|
        */
        public void LoadTiffFile()
        {
            DefectModel newDefect = new DefectModel();

            newDefect.currentImageIndex = 0;

            Instance.DefectImage = newDefect;
        }

        /**
        * @brief 선택한 인덱스를 기반으로 Tiff 파일을 업데이트, 현재 이미지 인덱스 설정
        * @param selectedIndex : 업데이트할 이미지의 인덱스
        * 2023-09-12|박유진|
        */
        public void UpdateTiffFile(int selectedIndex)
        {
            DefectModel newDefect = new DefectModel();

            newDefect.currentImageIndex = selectedIndex;

            Instance.DefectImage = newDefect;
            CurrentImageIndex = selectedIndex;
        }

        /**
        * @brief 선택한 좌표를 기반으로 이미지 인덱스 변환, Tiff 파일 업데이트
        * @param selectedCoordinate 선택한 좌표
        * 2023-09-12|박유진|
        */
        public void ConvertImageIndex(Point selectedCoordinate)
        {
            DefectModel newDefect = new DefectModel();

            newDefect.xIndex = GetPartsDefectList(3);
            newDefect.yIndex = GetPartsDefectList(4);
            newDefect.xIndices = ExtractIndices(newDefect.xIndex);
            newDefect.yIndices = ExtractIndices(newDefect.yIndex);
            newDefect.defectID = GetPartsDefectList(0);

            WaferModel newWafer = new WaferModel();

            newWafer.width = 400 / 20;
            newWafer.height = 400 / 50;

            newWafer.xIndex = GetPartsDieIndex(0);
            newWafer.yIndex = GetPartsDieIndex(1);
            newWafer.xIndices = ExtractIndices(newWafer.xIndex);
            newWafer.yIndices = ExtractIndices(newWafer.yIndex);

            int xMin = newWafer.xIndices.Min();
            int yMax = newWafer.yIndices.Max();

            List<int> indexList = new List<int>();

            for (int i = 0; i < newDefect.xIndex.Count; i++)
            {
                int newX = (newDefect.xIndices[i] - xMin) * newWafer.width;
                int newY = Math.Abs(newDefect.yIndices[i] - yMax) * newWafer.height;

                if (selectedCoordinate.X == newX)
                {
                    if (selectedCoordinate.Y == newY)
                    {
                        int index = int.Parse(newDefect.defectID[i]) -1;
                        indexList.Add(index);
                        UpdateTiffFile(indexList[0]);
                    }
                }
            }
        }

        /**
        * @brief 속성 변경 이벤트 핸들러 호출
        * @param propertyName : 변경된 속성의 이름
        * 2023-09-12|박유진|
        */
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
