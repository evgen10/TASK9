using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[CustomerDemographics]")]
    public class CustomerDemographic
    {
        [PrimaryKey]
        [Identity]
        [Column("CustomerTypeID")]
        public int CustomerTypeId { get; set; }

        [Column]
        public string CustomerDesc { get; set; }

        [Association(ThisKey = nameof(CustomerTypeId), OtherKey = nameof(Models.CustomerCustomerDemo.CustomerTypeId))]
        public CustomerCustomerDemo CustomerCustomerDemo { get; set; }

    }
}
