using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace Klarf
{
    class WaferMapViewModel : INotifyPropertyChanged
    {
        #region [상수]
        public event PropertyChangedEventHandler PropertyChanged;
        int x;
        int y;

        #endregion

        #region [필드]
        MainModel mainModel;
        private ObservableCollection<DieIndexItem> dieIndex;
        private ObservableCollection<Point> diePoint;
        private double width;
        private double height;

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

        public ObservableCollection<Point> DiePoint
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

        public double Width
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

        public double Height
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

        #endregion

        #region [생성자]
        public WaferMapViewModel()
        {
            MainModel.Instance.PropertyChanged += MainModel_PropertyChanged;
            MainModel = MainModel.Instance;
            DieIndex = new ObservableCollection<DieIndexItem>();
            DiePoint = new ObservableCollection<Point>();
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

            //List<int> xList = new List<int>();
            //List<int> yList = new List<int>();

            for (int i = 0; i < mainModel.Wafer.xIndex.Count; i++)
            {
                if (i < mainModel.Wafer.xIndices.Count && i < mainModel.Wafer.yIndices.Count) // 범위 확인
                {
                    x = mainModel.Wafer.xIndices[i] - xMin;
                    y = Math.Abs(mainModel.Wafer.yIndices[i] - yMax);

                    //xList[i] = x;
                    //yList[i] = y;

                    int[] xCounts = new int[50];
                    int[] yCounts = new int[50];

                    foreach (int yIndex in mainModel.Wafer.yIndices)
                    {
                        if (yIndex >= 0 && yIndex < 50)
                        {
                            xCounts[yIndex]++;
                        }
                    }

                    foreach (int xIndex in mainModel.Wafer.xIndices)
                    {
                        if (xIndex >= 0 && xIndex < 50)
                        {
                            yCounts[xIndex]++;
                        }
                    }

                    int xCount = xCounts.Max();
                    int yCount = yCounts.Max();

                    mainModel.Wafer.width = 400 / xCount;
                    mainModel.Wafer.height = 400 / yCount;

                    //Point savePoint = new Point { X = x, Y = y };


                }
            }

            DieIndex.Add(new DieIndexItem
            {
                DiePoint = new List<Point> { new Point { X = x, Y = y } },
                Width = mainModel.Wafer.width,
                Height = mainModel.Wafer.height
            });
        }

        #endregion

        #region [종속 클래스]
        public class DieIndexItem
        {
            public List<Point> DiePoint { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }

        #endregion
    }
}
