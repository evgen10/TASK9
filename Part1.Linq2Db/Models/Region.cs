using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Region]")]
    public class Region
    {
        [PrimaryKey]
        [Identity]
        [Column("RegionID")]
        public int Id { get; set; }

        [NotNull]
        [Column]
        public string RegionDescription { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey =nameof(Models.Territory.RegionId))]
        public IEnumerable<Territory> Territories { get; set; }
    }
}
