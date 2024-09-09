using SqlToExcelTool.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SqlToExcelTool.DataModels;
using Mapster;
using NPOI.SS.Formula.Functions;
using NPOI.POIFS.Storage;
using SqlToExcelTool.DataModels.ViewObjects;

namespace SqlToExcelTool.Services
{
    public class DataBaseService
    {
        private ExportTableService _exportTableService;
        public MySqlConnection MySqlConnection { get; set; }
        public string ConnectionString { get; set; }

        public DataBaseService(ExportTableService exportTableService)
        {
            _exportTableService = exportTableService;
        }


        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MySqlConnection CreateDataBaseConnection(string hostName, string port, string user, string password)
        {
            ConnectionString = $"Server={hostName};Uid={user};Pwd={password};";
            MySqlConnection = new MySqlConnection(ConnectionString);
            MySqlConnection.Open();
            return MySqlConnection;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void DisconnectDataBase()
        {
            if (MySqlConnection != null)
            {
                MySqlConnection.Dispose();
            }
        }

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        public List<DatabaseEntity> GetDatabases()
        {
            string query = "SELECT schema_name FROM information_schema.schemata";
            var adapter = new MySqlDataAdapter(query, MySqlConnection);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable.ConvertDataTableToList<DatabaseEntity>();

        }

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        public List<TableEntity> GetTables(DatabaseEntity databaseEntity)
        {
            string query = $"SHOW TABLE STATUS FROM `{databaseEntity.SCHEMA_NAME}`;";
            var adapter = new MySqlDataAdapter(query, MySqlConnection);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable.ConvertDataTableToList<TableEntity>();
        }


        /// <summary>
        /// 导出表格结构到Excel表
        /// </summary>
        /// <param name="tables">选中的表</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="fileName">文件名</param>
        public void ExportTableToExcel(List<TableSelectorVO> tables, string databaseName, string fileName)
        {
            if (tables == null)
            {
                throw new Exception("选中的表格不能为空");
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception("数据库名称不能为空");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("文件名称不能为空");
            }

            _exportTableService.ExportTableToExcel(MySqlConnection, tables, databaseName, fileName);
        }
    }
}
