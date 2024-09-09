using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SqlToExcelTool.Services;
using SqlToExcelTool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToExcelTool.Viewmodels
{
    public class MainWindowViewModel: ObservableObject
    {
        private object _currentDisplayContent;


        /// <summary>
        /// 当前展示的值
        /// </summary>
        public object CurrentDisplayContent {
            get => _currentDisplayContent;
            set => SetProperty(ref _currentDisplayContent, value);
        }



        public MainWindowViewModel() {
            CurrentDisplayContent = new LoginView();
        }
    }
}
