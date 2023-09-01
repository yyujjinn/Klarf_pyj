using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Klarf
{
    class WaferMapViewModel : INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [필드]
        MainModel mainModel;
        private ObservableCollection<DieIndexItem> dieIndex;
        private List<Point> diePoint;
        private int width;
        private int height;
        private Thickness margin;

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

        public ObservableCollection<DieIndexItem> DieIndex
        {
            get { return dieIndex; }
            set
            {
                if (dieIndex != value)
                {
                    dieIndex = value;
                    OnPropertyChanged(nameof(DieIndex));
                }
            }
        }

        public List<Point> DiePoint
        {
            get { return diePoint; }
            set
            {
                if (diePoint != value)
                {
                    diePoint = value;
                    OnPropertyChanged(nameof(DiePoint));
                }
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public Thickness Margin
        {
            get { return margin; }
            set
            {
                if (margin != value)
                {
                    margin = value;
                    OnPropertyChanged(nameof(Margin));
                }
            }
        }

        #endregion

        #region [생성자]
        public WaferMapViewModel()
        {
            MainModel.Instance.PropertyChanged += MainModel_PropertyChanged;
            MainModel = MainModel.Instance;
            DieIndex = new ObservableCollection<DieIndexItem>();
            //DiePoint = new List<Point>();
        }

        #endregion

        #region [메서드]
        private void MainModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Wafer")
            {
                ShowDie();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowDie()
        {
            int xMin = mainModel.Wafer.xIndices.Min();
            int yMax = mainModel.Wafer.yIndices.Max();

            for (int i = 0; i < mainModel.Wafer.xIndex.Count; i++)
            {
                int x = (mainModel.Wafer.xIndices[i] - xMin)*mainModel.Wafer.width;
                int y = Math.Abs(mainModel.Wafer.yIndices[i] - yMax)*mainModel.Wafer.height;


                //DieIndex.Add(new DieIndexItem
                //{
                //    DiePoint = new List<Point> { new Point { X = x, Y = y } },
                //    Width = mainModel.Wafer.width,
                //    Height = mainModel.Wafer.height
                //});

                var dieIndexItem = new DieIndexItem
                {
                    DiePoint = new List<Point> { new Point { X = x, Y = y } },
                    Height = mainModel.Wafer.height,
                    Width = mainModel.Wafer.width,
                    Margin = new Thickness(x, y, 0, 0)
                };

                DieIndex.Add(dieIndexItem);
            }
        }

        #endregion

        #region [종속 클래스]
        public class DieIndexItem
        {
            public List<Point> DiePoint { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public Thickness Margin { get; set; }
        }

        #endregion
    }
}
