using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SqlToExcelTool.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SqlToExcelTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //ConfigureMappings();
        }


        /// <summary>
        /// 配置datatable和实体之间的映射
        /// </summary>
        //private void ConfigureMappings()
        //{
        //    var config = TypeAdapterConfig.GlobalSettings;
        //    config.NewConfig<DataTable, DatabaseEntity>()
        //        .Map(dest => dest.SCHEMA_NAME, src => src.getba);
        //}
    }
}
