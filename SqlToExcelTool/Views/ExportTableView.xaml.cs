using SqlToExcelTool.DataModels;
using SqlToExcelTool.Viewmodels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqlToExcelTool.Views
{
    /// <summary>
    /// ExportTableView.xaml 的交互逻辑
    /// </summary>
    public partial class ExportTableView : UserControl
    {
        private readonly ExportTableViewModel _exportTableViewModel;
        public ExportTableView()
        {
            InitializeComponent();
            _exportTableViewModel = (ExportTableViewModel)this.DataContext;

            ReloadDataBaseList();
        }

        /// <summary>
        /// 加载数据库列表
        /// </summary>
        private void ReloadDataBaseList() {
            _exportTableViewModel.ReloadDataBaseList();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a =(ListBox)sender;
            var selectedDatabase = (DatabaseEntity)a.SelectedItem;
            _exportTableViewModel.SelectedDatabase = selectedDatabase;
            _exportTableViewModel.ReloadTables(selectedDatabase);
        }
    }
}
