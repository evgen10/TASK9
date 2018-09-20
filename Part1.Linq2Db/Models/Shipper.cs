using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Shippers]")]
    public class Shipper
    {
        [PrimaryKey]
        [Identity]
        [Column("ShipperID")]
        public int Id { get; set; }

        [NotNull]
        [Column]
        public string CompanyName{ get; set; }

        [Column]
        public string Phone { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.Order.ShipperId))]
        public IEnumerable<Order> Orders { get; set; }


    }
}
