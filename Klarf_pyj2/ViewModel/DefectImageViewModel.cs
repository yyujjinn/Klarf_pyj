using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void MainModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DefectImage")
            {
                ShowTifImage();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        private void OnRightDown(object parameter)
        {
            isRightButtonDown = true;
            startPoint = Mouse.GetPosition(null);
            isDragging = true;
        }

        private void OnMouseMove(object parameter)
        {
            if (isRightButtonDown)
            {
                endPoint = Mouse.GetPosition(null);
                RulerLength = CalculateRulerLength();
            }
        }

        private void OnRightUp(object parameter)
        {
            isRightButtonDown = false;
            isDragging = false;
        }

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
