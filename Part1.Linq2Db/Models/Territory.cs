using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Territories]")]
    public class Territory
    {
        [PrimaryKey]
        [Identity]
        [Column("TerritoryID")]
        public int Id { get; set; }
            
        [NotNull]
        [Column]
        public string TerritoryDescription { get; set; }

        [Column("RegionID")]
        public int RegionId { get; set; }

        [Association(ThisKey = nameof(RegionId),OtherKey =nameof(Models.Region.Id))]
        public Region Region { get; set; }

        [Association(ThisKey =nameof(Id),OtherKey =nameof(Models.EmployeeTerritory.TerritoryId))]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }


    }
}
