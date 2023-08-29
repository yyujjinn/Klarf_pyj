using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klarf
{
    public class MainViewModel
    {
        private IFileModel _fileModel;
        public  FileListViewModel fileListViewModel { get; set; }
        public static DefectInfoViewModel defectInfoViewModel;
        public DefectImageViewModel defectImageViewModel { get; set; }
        public WaferMapViewModel waferMapViewModel { get; set; }

        public MainViewModel(IFileModel fileModel)
        {
            _fileModel = fileModel;
            fileListViewModel = new FileListViewModel();
            defectInfoViewModel = new DefectInfoViewModel();
            defectImageViewModel = new DefectImageViewModel();
            waferMapViewModel = new WaferMapViewModel();
        }

    }
}
