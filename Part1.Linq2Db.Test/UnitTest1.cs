using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LinqToDB;
using LinqToDB.Common;

namespace Part1.Linq2Db.Test
{
    [TestClass]
    public class UnitTest1
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
                                                            
                foreach (var product in products)
                {
                    Console.WriteLine($" Id: {product.Id} ProductName: {product.Name}  Category: {product.Category?.Name}  SupplierName: {product.Supplier?.ContactName}");
                }
                
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

                foreach (var employee in employees.Distinct())
                {
                    Console.WriteLine($"Id: {employee.Id}   Name: {employee.FirstName} {employee.LastName}  Region: {employee.RegionDescription}");
                }

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
                                select new { RegionId = region.Id, EmployeesId = employee.Id };

                var regionStatistic = from regStat in employees.Distinct()
                                      group regStat.EmployeesId by regStat.RegionId into groupResult
                                      select new { Region = groupResult.Key, EmployeesCount = groupResult.Count() };
                               



                foreach (var employee in regionStatistic)
                {
                    Console.WriteLine($"Region: { employee.Region}  EmployeesCount { employee.EmployeesCount} ");

                }

            }



        }


       


    }
}
