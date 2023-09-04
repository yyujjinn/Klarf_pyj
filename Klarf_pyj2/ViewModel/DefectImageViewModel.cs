using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.IO;

namespace Klarf
{
    class DefectImageViewModel : INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [필드]
        MainModel mainModel;
        private TiffBitmapDecoder tiffDecoder;
        private BitmapFrame loadedImage;

        #endregion

        #region [속성]
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

        #endregion

        #region [생성자]
        public DefectImageViewModel()
        {
            MainModel.Instance.PropertyChanged += MainModel_PropertyChanged;
            mainModel = MainModel.Instance;
        }

        #endregion

        #region [메서드]
        private void MainModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DefectImage")
            {
                ShowTifImage();
            }
            //else if (e.PropertyName == "NextDefectImage")
            //{
            //    ShowTifImage();
            //}
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

        #endregion

    }
}
