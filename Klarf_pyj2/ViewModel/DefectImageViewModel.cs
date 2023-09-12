using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace Klarf
{
    class DefectImageViewModel : INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isRightButtonDown = false;

        #endregion

        #region [필드]
        MainModel mainModel;
        private TiffBitmapDecoder tiffDecoder;
        private BitmapFrame loadedImage;
        private Point startPoint;
        private Point endPoint;
        private Point linePoint;
        private bool isDragging;
        private string rulerLength;

        #endregion

        #region [속성]
        public ICommand MouseRightButtonDownCommand { get; }
        public ICommand MouseMoveCommand { get; }
        public ICommand MouseRightButtonUpCommand { get; }

        public BitmapFrame LoadedImage
        {
            get { return loadedImage; }
            set
            {
                if (loadedImage != value)
                {
                    loadedImage = value;
                    OnPropertyChanged("LoadedImage");
                }
            }
        }

        public string RulerLength
        {
            get { return rulerLength; }
            set
            {
                if (rulerLength != value)
                {
                    rulerLength = value;
                    OnPropertyChanged(nameof(RulerLength));
                }
            }
        }

        public Point StartPoint
        {
            get { return startPoint; }
            set
            {
                startPoint = value;
                OnPropertyChanged(nameof(StartPoint));
            }
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
                OnPropertyChanged(nameof(EndPoint));
            }
        }

        public Point LinePoint
        {
            get { return linePoint; }
            set
            {
                linePoint = value;
                OnPropertyChanged(nameof(LinePoint));
            }
        }

        public bool IsDragging
        {
            get { return isDragging; }
            set
            {
                isDragging = value;
                OnPropertyChanged(nameof(IsDragging));
            }
        }

        #endregion

        #region [생성자]
        public DefectImageViewModel()
        {
            MainModel.Instance.PropertyChanged += MainModel_PropertyChanged;
            mainModel = MainModel.Instance;
            MouseRightButtonDownCommand = new RelayCommand<object>(OnRightDown);
            MouseMoveCommand = new RelayCommand<object>(OnMouseMove);
            MouseRightButtonUpCommand = new RelayCommand<object>(OnRightUp);
        }

        #endregion

        #region [메서드]
        /**
        * @brief PropertyChanged 이벤트 핸들러
        * @param sender : 이벤트 발생 객체
        * @param e : PropertyChangedEventArgs 객체
        * @note DefectImage 속성이 변경되면 ShowTifImage() 메서드 호출
        * 2023-09-12|박유진|
        */
        private void MainModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DefectImage")
            {
                ShowTifImage();
            }
        }

        /**
        * @brief 속성 변경 이벤트 호출
        * @param propertyName : 변경된 속성의 이름
        * 2023-09-12|박유진|
        */
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /**
        * @brief TIF 이미지 로드, 현재 선택된 이미지 표시
        * 2023-09-12|박유진|
        */
        private void ShowTifImage()
        {
            string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";

            if (!Directory.Exists(folderPath))
                return;

            var tifFiles = Directory.GetFiles(folderPath, "*.tif");
            if (tifFiles.Length == 0)
                return;

            var tifFile = tifFiles[0];
            tiffDecoder = new TiffBitmapDecoder(new Uri(tifFile, UriKind.Absolute), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            if (tiffDecoder.Frames.Count > 0)
            {
                LoadedImage = tiffDecoder.Frames[mainModel.DefectImage.currentImageIndex];
            }
        }

        /**
        * @brief 마우스 오른쪽 버튼 눌렀을 때 호출되는 메서드
        * @param parameter : 이벤트 매개변수
        * 2023-09-12|박유진|
        */
        private void OnRightDown(object parameter)
        {
            isRightButtonDown = true;
            startPoint = new Point { X = Mouse.GetPosition(null).X - 400, Y = Mouse.GetPosition(null).Y};
            StartPoint = startPoint;
            IsDragging = true;
        }

        /**
        * @brief 마우스 이동 시 호출되는 메서드
        * @param parameter : 이벤트 매개변수
        * 2023-09-12|박유진|
        */
        private void OnMouseMove(object parameter)
        {
            if (isDragging && isRightButtonDown)
            {
                linePoint = new Point { X = Mouse.GetPosition(null).X - 400, Y = Mouse.GetPosition(null).Y };
                LinePoint = linePoint;
            }
        }

        /**
        * @brief 마우스 오른쪽 버튼 놓았을 때 호출되는 메서드
        * @param parameter : 이벤트 매개변수
        * 2023-09-12|박유진|
        */
        private void OnRightUp(object parameter)
        {
            endPoint = new Point { X = Mouse.GetPosition(null).X - 400, Y = Mouse.GetPosition(null).Y };
            EndPoint = endPoint;
            RulerLength = CalculateRulerLength();

            isRightButtonDown = false;
        }

        /**
        * @brief 두 점 간의 거리 계산하여 길이 반환
        * @return lengthMicrometersText : 계산된 길이 문자열
        * 2023-09-12|박유진|
        */
        private string CalculateRulerLength()
        {
            double deltaX = endPoint.X - startPoint.X;
            double deltaY = endPoint.Y - startPoint.Y;

            double length = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            double lengthMicrometers = length * 0.298;

            string lengthMicrometersText = $"{lengthMicrometers:F2} µm";

            return lengthMicrometersText;
        }

        #endregion

    }
}
