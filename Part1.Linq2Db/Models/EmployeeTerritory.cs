using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[EmployeeTerritories]")]
    public class EmployeeTerritory
    {
        [PrimaryKey]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }

        [PrimaryKey]
        [Column("TerritoryID")]
        public int TerritoryId { get; set; }

        [Association(ThisKey = nameof(TerritoryId),OtherKey = nameof(Models.Territory.Id))]
        public Territory Territory { get; set; }

        [Association(ThisKey = nameof(EmployeeId),OtherKey =nameof(Models.Employee.Id))]
        public Employee Employee { get; set; }

    }
}
