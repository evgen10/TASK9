using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.Mapping;
using Part1.Linq2Db.Models;


namespace Part1.Linq2Db
{
    public class DbNorthwind : DataConnection
    {
        public DbNorthwind(string configurationString) : base(configurationString)
        {
            
        }

        public DbNorthwind(string providerName,string connectionString) : base(providerName, connectionString)
        {
        }

        public ITable<Category> Categories => GetTable<Category>();
        public ITable<CustomerCustomerDemo> CustomerCustomerDemo => GetTable<CustomerCustomerDemo>();
        public ITable<CustomerDemographic> CustomerDemographics => GetTable<CustomerDemographic>();
        public ITable<Customer> Customers => GetTable<Customer>();
        public ITable<Employee> Employees => GetTable<Employee>();
        public ITable<EmployeeTerritory> EmployeeTerritories => GetTable<EmployeeTerritory>();
        public ITable<OrderDetail> OrderDetails => GetTable<OrderDetail>();
        public ITable<Order> Orders => GetTable<Order>();
        public ITable<Product> Products => GetTable<Product>();
        public ITable<Region> Regions => GetTable<Region>();
        public ITable<Shipper> Shippers => GetTable<Shipper>();
        public ITable<Supplier> Suppliers => GetTable<Supplier>();
        public ITable<Territory> Territories => GetTable<Territory>();



    }
}
