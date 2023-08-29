using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Klarf
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 원하는 ViewModel 인스턴스 생성
            IFileModel fileModel = new FileModel();
            var viewModel = new MainViewModel(fileModel);

            DefectInfoViewModel defectInfoViewModel = new DefectInfoViewModel();
            FileListViewModel fileListViewModel = new FileListViewModel();
        }
    }
}
