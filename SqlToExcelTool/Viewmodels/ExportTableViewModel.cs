using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using SqlToExcelTool.DataModels;
using SqlToExcelTool.DataModels.ViewObjects;
using SqlToExcelTool.Services;
using SqlToExcelTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SqlToExcelTool.Viewmodels
{
    public class ExportTableViewModel : ObservableObject
    {
        private DataBaseService _dataBaseService;
        private ObservableCollection<DatabaseEntity> _dbList;
        private ObservableCollection<TableSelectorVO> _tables;
        private DatabaseEntity _selectedDatabase;

        /// <summary>
        /// 数据库列表
        /// </summary>
        public ObservableCollection<DatabaseEntity> DbList
        {
            get => _dbList;
            set => SetProperty(ref _dbList, value);
        }

        /// <summary>
        /// 选中的数据库
        /// </summary>
        public DatabaseEntity SelectedDatabase {
            get => _selectedDatabase;
            set => SetProperty(ref _selectedDatabase, value);
        }

        /// <summary>
        /// 表列表
        /// </summary>
        public ObservableCollection<TableSelectorVO> Tables
        {
            get => _tables;
            set => SetProperty(ref _tables, value);
        }


        /// <summary>
        /// 按钮：跳转登录页面
        /// </summary>
        /// <returns></returns>
        public RelayCommand SwitchToLoginViewCmd => new RelayCommand(() =>
        {
            try
            {
                _dataBaseService.DisconnectDataBase();
                ViewModelLocator.Instance.MainWindowViewModel.CurrentDisplayContent = new LoginView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });


        /// <summary>
        /// 全选/全不选表格
        /// </summary>
        public RelayCommand SelectAllTablesCmd => new RelayCommand(() =>
        {
            try
            {
                // 标记全选或者不选
                bool isCheckOrUnchecked = false;

                // 根据是否有没选中的选项，判断本次执行全选，还是全不选操作
                if (Tables.Any(x => x.IsChecked == false))
                {
                    isCheckOrUnchecked = true;
                }
                else
                {
                    isCheckOrUnchecked = false;
                }

                foreach (var table in Tables)
                {
                    table.IsChecked = isCheckOrUnchecked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });


        /// <summary>
        /// 导出表结构
        /// </summary>
        public RelayCommand ExportTableSchemaCmd => new RelayCommand(() =>
        {
            try
            {
                if (Tables == null || !Tables.Any(x => x.IsChecked == true))
                {
                    MessageBox.Show("请选择要导出表结构的表格", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = $"{SelectedDatabase.SCHEMA_NAME}表结构"; // Default file name
                saveFileDialog.DefaultExt = ".xlsx"; // Default file extension
                saveFileDialog.Filter = "Excel 2007文档 (.xlsx)|*.xlsx"; // Filter files by extension
                bool? result = saveFileDialog.ShowDialog();

                if (result != true)
                {
                    return;
                }

                string filename = saveFileDialog.FileName;
                List<TableSelectorVO> selectedTables = Tables.Where(x=>x.IsChecked == true).ToList();

                _dataBaseService.ExportTableToExcel(selectedTables,SelectedDatabase.SCHEMA_NAME,filename);

                MessageBox.Show("导出成功", "消息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });



        /// <summary>
        /// 加载数据库列表
        /// </summary>
        public void ReloadDataBaseList()
        {
            DbList = new ObservableCollection<DatabaseEntity>(_dataBaseService.GetDatabases());
        }


        /// <summary>
        /// 加载数据库列表
        /// </summary>
        public void ReloadTables(DatabaseEntity databaseEntity)
        {
            Tables = TableSelectorVO.GetVOsFromEntity(_dataBaseService.GetTables(databaseEntity));
        }


        public ExportTableViewModel(DataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
    }
}
