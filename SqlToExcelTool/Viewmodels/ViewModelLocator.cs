using Microsoft.Extensions.DependencyInjection;
using SqlToExcelTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SqlToExcelTool.Viewmodels
{
    public class ViewModelLocator
    {
        private IServiceProvider _serviceProvider;

        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() =>Application.Current.TryFindResource("Locator") as ViewModelLocator)?.Value;

        public ViewModelLocator() {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }


        // 注册你的服务
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<DataBaseService>();
            services.AddSingleton<ExportTableService>();
            services.AddSingleton<ExportTableViewModel>();
        }


        /// <summary>
        /// 登录页面ViewModel
        /// </summary>
        public LoginViewModel LoginViewModel => _serviceProvider.GetRequiredService<LoginViewModel>();

        /// <summary>
        /// 主页面ViewModel
        /// </summary>
        public MainWindowViewModel MainWindowViewModel => _serviceProvider.GetRequiredService<MainWindowViewModel>();

        /// <summary>
        /// 导出表格页面ViewModel
        /// </summary>
        public ExportTableViewModel ExportTableViewModel => _serviceProvider.GetRequiredService<ExportTableViewModel>();
    }
}
