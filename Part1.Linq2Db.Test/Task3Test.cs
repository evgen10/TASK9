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

                #region Demonstration
                var empl = db.Employees.First(e => e.Id == employee.Id);

                Console.WriteLine($"{ empl.FirstName} {empl.LastName}");

                var territories = db.EmployeeTerritories.LoadWith(e => e.Territory).Where(ter => ter.EmployeeId == employee.Id);

                foreach (var terr in territories)
                {
                    Console.WriteLine(terr.Territory.TerritoryDescription);
                }
                #endregion
                
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
                int sourceCategoryId = 1;
                int newCAtegoryId = 2;

                #region Demonstration
                //начальное количество продуктов под категорией newCAtegoryId
                Console.WriteLine(db.Products.Where(p => p.CategoryId == newCAtegoryId).Count());
                #endregion

                db.Products.Where(p => p.CategoryId == sourceCategoryId).Set(p => p.CategoryId, newCAtegoryId).Update();

                #region Demonstration
                // количество продуктов под категорией newCAtegoryId после запроса
                Console.WriteLine(db.Products.Where(p => p.CategoryId == newCAtegoryId).Count());
                #endregion

                scope.Dispose();
            }


        }
        
        //Добавить список продуктов со своими поставщиками и категориями (массовое занесение), 
        //при этом если поставщик или категория с таким названием есть, то использовать их – иначе создать новые. 
        [TestMethod]
        public void AddProducts()
        {
            TransactionScope scope = new TransactionScope();

            using (var db = new DbNorthwind(providerName, connectionString))
            {
                List<Product> productList = GetProductList();


                foreach (var product in productList)
                {
                    Category category = db.Categories.FirstOrDefault(c => c.Name == product.Category.Name);
                    Supplier supplier = db.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);

                    if (category == null)
                    {
                        //если такой категории не существует то заносим ее в бд и получаем её id
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

                #region Demonstration
                foreach (var product in productList)
                {

                    foreach (var item in db.Products.LoadWith(p => p.Supplier).LoadWith(p => p.Category).Where(p => p.Name == product.Name))
                    {
                        Console.WriteLine($" Product name:   {item.Name}  Supplier name:   {item.Supplier.CompanyName} Category name:   {item.Category.Name}");
                    }

                }
                #endregion

                scope.Dispose();
            }
        }
        
        //Замена продукта на аналогичный: во всех еще неисполненных заказах 
        //(считать таковыми заказы, у которых ShippedDate = NULL) заменить один продукт на другой.
        [TestMethod]
        public void ReplaceProduct()
        {
            TransactionScope scope = new TransactionScope();

            using (var db = new DbNorthwind(providerName, connectionString))
            {        

                var orderDetails = db.OrderDetails.LoadWith(od => od.Order).LoadWith(od => od.Product)
                                                  .Where(od => od.Order.ShippedDate == null)
                                                  .Update(od => new OrderDetail
                                                  {
                                                      ProductId = db.Products.First(p => p.CategoryId == od.Product.CategoryId && p.Id > od.ProductId) != null
                                                                   ? db.Products.First(p => p.CategoryId == od.Product.CategoryId && p.Id > od.ProductId).Id
                                                                   : db.Products.First(p => p.CategoryId == od.Product.CategoryId).Id
                                                  });                
                scope.Dispose();


            }

        }


        private List<Product> GetProductList()
        {
            return  new List<Product>()
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

        }


    }

}
