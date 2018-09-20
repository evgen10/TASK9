using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;


namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Categories]")]
    public class Category
    {
        [PrimaryKey]
        [Identity]
        [Column("CategoryID")]
        public int Id { get; set; }

        [Column("CategoryName")]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public byte[] Picture { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.Product.CategoryId))]
        public IEnumerable<Product> Products { get; set; }
    }
}
