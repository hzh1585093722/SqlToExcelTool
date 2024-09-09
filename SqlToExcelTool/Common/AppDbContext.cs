using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToExcelTool.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string connectionString) :base(connectionString) { 
        
        }
    }
}
