using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Order Details]")]
    public class OrderDetail
    {
        [PrimaryKey]
        [Column("OrderID")]
        public int OrderId { get; set; }

        [PrimaryKey]
        [Column("ProductID")]
        public int ProductId { get; set; }

        [NotNull]
        [Column]
        public decimal UnitPrice { get; set; }

        [NotNull]
        [Column]
        public short Quantity { get; set; }

        [NotNull]
        [Column]
        public double Discount { get; set; }

        [Association(ThisKey =nameof(ProductId), OtherKey = nameof(Models.Product.Id))]
        public Product Product { get; set; }

        [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Models.Order.Id))]
        public Order Order { get; set; }





    }
}
