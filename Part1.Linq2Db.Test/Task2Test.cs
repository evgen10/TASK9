using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LinqToDB;
using LinqToDB.Common;

namespace Part1.Linq2Db.Test
{
    [TestClass]
    public class Task2Test
    {
        private string connectionString = @"Data Source = (localdb)\ProjectsV13; Initial Catalog = Northwind; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultipleActiveResultSets = True; MultiSubnetFailover = False";
        private string providerName = "SqlServer";


        //Список продуктов с категорией и поставщиком
        [TestMethod]
        public void GetProductListWithCategoryAndSupllier()
        {

            using (var db = new DbNorthwind(providerName, connectionString))
            {
                var products = db.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier);

                #region Demonstration
                foreach (var product in products)
                {
                    Console.WriteLine($" Id: {product.Id} ProductName: {product.Name}  Category: {product.Category?.Name}  SupplierName: {product.Supplier?.ContactName}");
                }
                #endregion
                
            }
        }

        //Cписок сотрудников с указанием региона, за который они отвечают
        [TestMethod]
        public void GetEmployeesWithRegion()
        {

            using (var db = new DbNorthwind(providerName, connectionString))
            {

                var employees = from employee in db.Employees
                                join employeeTerritory in db.EmployeeTerritories on employee.Id equals employeeTerritory.EmployeeId
                                join territory in db.Territories on employeeTerritory.TerritoryId equals territory.Id
                                join region in db.Regions on territory.RegionId equals region.Id
                                select new { employee.Id, employee.FirstName, employee.LastName, region.RegionDescription };

                #region Demonstration

                foreach (var employee in employees.Distinct())
                {
                    Console.WriteLine($"Id: {employee.Id}   Name: {employee.FirstName} {employee.LastName}  Region: {employee.RegionDescription}");
                }

                #endregion



            }

        }

        //Статистики по регионам: количества сотрудников по регионам
        [TestMethod]
        public void GetEmployeeCountByRegion()
        {
            using (var db = new DbNorthwind(providerName, connectionString))
            {
                var employees = from employee in db.Employees
                                join employeeTerritory in db.EmployeeTerritories on employee.Id equals employeeTerritory.EmployeeId
                                join territory in db.Territories on employeeTerritory.TerritoryId equals territory.Id
                                join region in db.Regions on territory.RegionId equals region.Id
                                select new { RegionName = region.RegionDescription, EmployeesId = employee.Id };

                var regionStatistic = from regStat in employees.Distinct()
                                      group regStat.EmployeesId by regStat.RegionName into groupResult
                                      select new { Region = groupResult.Key, EmployeesCount = groupResult.Count() };


                #region Demonstration

                foreach (var employee in regionStatistic)
                {
                    Console.WriteLine($"Region: { employee.Region}  EmployeesCount { employee.EmployeesCount} ");
                }
                #endregion



            }



        }

        //Списка «сотрудник – с какими грузоперевозчиками работал» (на основе заказов)
        [TestMethod]
        public void GetShipperListByEmployee()
        {
            using (var db = new DbNorthwind(providerName, connectionString))
            {

                var orders = from order in db.Orders.LoadWith(o => o.Employee).LoadWith(o => o.Shipper)
                             group order by new { order.Employee.FirstName, order.Employee.LastName };


                #region Demonstration

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Key.FirstName} {order.Key.LastName}");

                    foreach (var item in order)
                    {
                        Console.WriteLine($"Company name: {item.Shipper.CompanyName}  ShipCountry {item.ShipCountry}  ShipCity {item.ShipCity} ShipAddress {item.ShipAddress}");
                    }
                }

                #endregion


            }
            
        }
    }
}
