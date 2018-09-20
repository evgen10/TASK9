using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Orders]")]
    public class Order
    {
        [PrimaryKey]
        [Identity]
        [Column("OrderID")]
        public int Id { get; set; }

        [Column("CustomerID")]                
        public string CustomerId { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        [Column]
        public DateTime? OrderDate { get; set; }

        [Column]
        public DateTime? RequiredDate { get; set; }

        [Column]
        public DateTime?  ShippedDate { get; set; }

        [Column("ShipVia")]
        public int ShipperId { get; set; }

        [Column]
        public decimal? Freight { get; set; }

        [Column]
        public string ShipName { get; set; }

        [Column]
        public string ShipAddress { get; set; }

        [Column]
        public string ShipCity { get; set; }

        [Column]
        public string ShipRegion { get; set; }

        [Column]
        public string ShipPostalCode { get; set; }

        [Column]
        public string ShipCountry { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.OrderDetail.OrderId) )]
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        [Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(Models.Employee.Id))]
        public Employee Employee { get; set; }

        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Models.Customer.Id))]
        public Customer Customer { get; set; }

        [Association(ThisKey = nameof(ShipperId), OtherKey = nameof(Models.Shipper.Id))]
        public Shipper Shipper { get; set; }

    }
}
