using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.Linq2Db.Models
{
    [Table("[dbo].[Employees]")]
    public class Employee
    {
        [PrimaryKey]
        [Identity]
        [Column("EmployeeID")]
        public int Id { get; set; }

        [Column]
        [NotNull]
        public string LastName { get; set; }

        [Column]
        [NotNull]
        public string FirstName { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string TitleOfCourtesy { get; set; }

        [Column]
        public DateTime? BirthDate { get; set; }

        [Column]
        public DateTime? HireDate { get; set; }

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
        public string HomePhone { get; set; }

        [Column]
        public string Extension { get; set; }

        [Column]
        public byte[] Photo { get; set; }

        [Column]
        public string Notes { get; set; }

        [Column]
        public string PhotoPath { get; set; }

        [Column("ReportsTo")]
        public int? BossId { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Models.EmployeeTerritory.EmployeeId))]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Order.EmployeeId))]
        public IEnumerable<Order> Orders { get; set; }

        [Association(ThisKey = nameof(BossId), OtherKey = nameof(Id))]
        public Employee Boss { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(BossId))]
        public IEnumerable<Employee> People { get; set; }

    }
}
