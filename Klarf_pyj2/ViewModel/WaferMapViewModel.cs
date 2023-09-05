using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

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
        private ObservableCollection<DefectIndexItem> defectIndex;
        private List<Point> defectPoint;
        private int width;
        private int height;
        private Thickness margin;

        #endregion

        #region [속성]
        //public ICommand ShowCoordinatesCommand { get; }
        //public ICommand HideCoordinatesCommand { get; }

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

        public ObservableCollection<DefectIndexItem> DefectIndex
        {
            get { return defectIndex; }
            set
            {
                if (defectIndex != value)
                {
                    defectIndex = value;
                    OnPropertyChanged(nameof(DefectIndex));
                }
            }
        }

        public List<Point> DefectPoint
        {
            get { return defectPoint; }
            set
            {
                if (defectPoint != value)
                {
                    defectPoint = value;
                    OnPropertyChanged(nameof(DefectPoint));
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
            DefectIndex = new ObservableCollection<DefectIndexItem>();
            //ShowCoordinatesCommand = new RelayCommand<object>(ShowCoordinates);
            //HideCoordinatesCommand = new RelayCommand<object>(HideCoordinates);
        }

        #endregion

        #region [메서드]
        private void MainModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Wafer")
            {
                ShowDie();
            }
            else if (e.PropertyName == "DefectDie")
            {
                ShowDefectDie();
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

                var dieIndexItem = new DieIndexItem
                {
                    DiePoint = new List<Point> { new Point { X = x, Y = y } },
                    Height = mainModel.Wafer.height,
                    Width = mainModel.Wafer.width,
                    Margin = new Thickness(x, y, 0, 0),
                };

                DieIndex.Add(dieIndexItem);
            }
        }

        private void ShowDefectDie()
        {
            int xMin = mainModel.Wafer.xIndices.Min();
            int yMax = mainModel.Wafer.yIndices.Max();

            for (int i = 0; i < mainModel.DefectDie.xIndex.Count; i++)
            {
                int x = (mainModel.DefectDie.xIndices[i] - xMin) * mainModel.Wafer.width;
                int y = Math.Abs(mainModel.DefectDie.yIndices[i] - yMax) * mainModel.Wafer.height;

                int xText = mainModel.Wafer.xIndices[i];
                int yText = mainModel.Wafer.yIndices[i];

                var defectIndexItem = new DefectIndexItem
                {
                    DefectPoint = new List<Point> { new Point { X = x, Y = y } },
                    Height = mainModel.Wafer.height,
                    Width = mainModel.Wafer.width,
                    Margin = new Thickness(x, y, 0, 0),

                    Text = string.Format("( {0}, {1} )", xText, yText)
                };

                DefectIndex.Add(defectIndexItem);
            }
        }

        #endregion

        #region [종속 클래스]
        public class DieIndexItem
        {
            public List<Point> DiePoint { get; set; }
            public List<Point> DefectPoint { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public Thickness Margin { get; set; }
        }

        public class DefectIndexItem
        {
            public List<Point> DefectPoint { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public Thickness Margin { get; set; }
            public string Text { get; set; }
        }

        #endregion
    }
}
