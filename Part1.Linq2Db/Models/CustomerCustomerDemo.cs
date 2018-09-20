using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[CustomerCustomerDemo]")]
    public class CustomerCustomerDemo
    {
        [PrimaryKey]      
        [Column("CustomerID")]
        public string CustomerId { get; set; }

        [PrimaryKey]
        [Column("CustomerTypeID")]
        public string CustomerTypeId { get; set; }

        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Models.Customer.Id))]
        public Customer Customer { get; set; }

        [Association(ThisKey = nameof(CustomerTypeId), OtherKey = nameof(Models.CustomerDemographic.CustomerTypeId))]
        public IEnumerable<CustomerDemographic> CustomerDemographic { get; set; }

    }
}
