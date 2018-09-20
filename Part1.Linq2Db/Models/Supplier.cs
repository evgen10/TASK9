using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Suppliers]")]
    public class Supplier
    {
        [PrimaryKey]
        [Identity]
        [Column("SupplierID")]
        public string Id { get; set; }

        [Column]
        [NotNull]
        public string CompanyName { get; set; }

        [Column]
        public string ContactName { get; set; }

        [Column]
        public string ContactTitle { get; set; }

        [Column]
        public string Address { get; set; }

        [Column]
        public string City { get; set; }

        [Column]
        public string Region { get; set; }

        [Column]
        public string PostalCode { get; set; }

        [Column]
        public string Country { get; set; }

        [Column]
        public string Phone { get; set; }

        [Column]
        public string Fax { get; set; }

        [Column]
        public string HomePage { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.Product.SupplierId) )]
        public IEnumerable<Product> Products { get; set; }
    }
}
