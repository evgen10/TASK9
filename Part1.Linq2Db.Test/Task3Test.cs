using System;
using System.Text;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using System.Collections.Generic;
using Part1.Linq2Db.Models;


namespace Part1.Linq2Db.Test
{
    /// <summary>
    /// Summary description for Task3Test
    /// </summary>
    [TestClass]
    public class Task3Test
    {
        private string connectionString = @"Data Source = (localdb)\ProjectsV13; Initial Catalog = Northwind; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultipleActiveResultSets = True; MultiSubnetFailover = False";
        private string providerName = "SqlServer";


        //Добавить нового сотрудника, и указать ему список территорий, за которые он несет ответственность. 
        [TestMethod]
        public void AddNewEmployee()
        {
            Employee employee = new Employee()
            {
                FirstName = "Evgeniy",
                LastName = "Chernyshkov",
                BirthDate = new DateTime(1996, 3, 10)

            };


            TransactionScope scope = new TransactionScope();


            using (var db = new DbNorthwind(providerName, connectionString))
            {

                employee.Id = db.InsertWithInt32Identity(employee);
                db.Territories.Take(3).Insert(db.EmployeeTerritories, et => new EmployeeTerritory() { EmployeeId = employee.Id, TerritoryId = et.Id });


                //var query = from empl in db.Employees.Where(e => e.Id == employee.Id)
                //            join empTer in db.EmployeeTerritories on empl.Id equals empTer.TerritoryId
                //            join ter in db.Territories on empTer.TerritoryId equals ter.Id
                //            select new { Employee = new { empl.FirstName, empl.LastName }, Territories = ter };


                //foreach (var item in query)
                //{
                //    Console.WriteLine($"{item.Employee.FirstName} {item.Employee.LastName}");

                //    foreach (var i in item.Territories)
                //    {

                //    }
                //}



                foreach (var item in db.Employees)
                {

                }


                foreach (var empl in db.Employees)
                {
                    Console.WriteLine($"{ empl.FirstName} {empl.LastName}");


                }

                scope.Dispose();

            }


        }


        //Перенести продукты из одной категории в другую
        [TestMethod]
        public void MoveProducts()
        {
            TransactionScope scope = new TransactionScope();


            using (var db = new DbNorthwind(providerName, connectionString))
            {
                Console.WriteLine(db.Products.Where(p => p.CategoryId == 2).Count());

                db.Products.Where(p => p.CategoryId == 1).Set(p => p.CategoryId, 2).Update();

                Console.WriteLine(db.Products.Where(p => p.CategoryId == 2).Count());

                scope.Dispose();
            }


        }

        [TestMethod]
        public void AddProducts()
        {
            TransactionScope scope = new TransactionScope();

            using (var db = new DbNorthwind(providerName, connectionString))
            {
                List<Product> productList = new List<Product>()
                {
                    new Product
                    {
                        Name = "NewProduct1",
                        Category = new Category { Name = "NewCategory1" },
                        Supplier = new Supplier { CompanyName = "NewSupplier1"  }

                    },

                    new Product
                    {
                        Name = "NewProduct2",
                        Category = new Category { Name = "NewCategory2" },
                        Supplier = new Supplier { CompanyName = "NewSupplier2"  }

                    },

                    new Product
                    {
                        Name = "NewProduct3",
                        Category = new Category { Name = "Beverages" },
                        Supplier = new Supplier { CompanyName = "Exotic Liquids"  }

                    }
                };


                foreach (var product in productList)
                {
                    Category category = db.Categories.FirstOrDefault(c => c.Name == product.Category.Name);
                    Supplier supplier = db.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);

                    if (category == null)
                    {
                        product.CategoryId = db.InsertWithInt32Identity(new Category { Name = product.Category.Name });
                    }
                    else
                    {
                        product.CategoryId = category.Id;
                    }

                    if (supplier == null)
                    {
                        product.SupplierId = db.InsertWithIdentity(new Supplier { CompanyName = product.Supplier.CompanyName }).ToString();
                    }
                    else
                    {
                        product.SupplierId = supplier.Id;
                    }

                }

                db.BulkCopy(productList);


                foreach (var product in productList)
                {

                    foreach (var item in db.Products.LoadWith(p => p.Supplier).LoadWith(p => p.Category).Where(p => p.Name == product.Name))
                    {
                        Console.WriteLine($" Product name:   {item.Name}  Supplier name:   {item.Supplier.CompanyName} Category name:   {item.Category.Name}");
                    }

                }


                scope.Dispose();
            }
        }





    }

}
