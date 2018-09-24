using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Products]")]
    public class Product
    {
        [PrimaryKey]
        [Identity]
        [Column("ProductID")]
        public int Id { get; set; }

        [Column("ProductName")]
        public string Name { get; set; }

        [Column("SupplierID")]
        public string SupplierId { get; set; }

        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        [Column]
        public string QuantityPerUnit { get; set; }

        [Column]
        public decimal? UnitPrice { get; set; }

        [Column]
        public short? UnitsInStock { get; set; }

        [Column]
        public short? UnitsOnOrder { get; set; }

        [Column]
        public short? ReorderLevel { get; set; }

        [Column]
        [NotNull]
        public bool Discontinued { get; set; }

        [Association(ThisKey = nameof(CategoryId), OtherKey = nameof(Models.Category.Id),CanBeNull =true)]
        public Category Category { get; set; }

        [Association(ThisKey = nameof(SupplierId), OtherKey =nameof(Models.Supplier.Id))]
        public Supplier Supplier { get; set; }
        


    }
}
