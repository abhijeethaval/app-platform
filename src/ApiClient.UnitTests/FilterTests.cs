using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ApiClient.UnitTests
{
    [TestClass]
    public class FilterTests
    {
        [TestMethod]
        public void Creates_filter_with_number_equal_condition()
        {
            var filterString = new Filter<Employee>()
                .EqualTo(x => x.Salary, 40000)
                .AsApiFilter();
            Assert.AreEqual("Salary $eq 40000", filterString);
        }

        [TestMethod]
        public void Creates_filter_string_equal_condition()
        {
            var filterString = new Filter<Employee>()
                .EqualTo(x => x.Name, "Abhijeet")
               .AsApiFilter();
            Assert.AreEqual("Name $eq 'Abhijeet'", filterString);
        }

        [TestMethod]
        public void Creates_filter_number_greater_than_condition()
        {
            var filterString = new Filter<Employee>()
                .GreaterThan(x => x.Salary, 40000)
                .AsApiFilter();
            Assert.AreEqual("Salary $gt 40000", filterString);
        }

        [TestMethod]
        public void Creates_filter_number_less_than_condition()
        {
            var filterString = new Filter<Employee>()
                .LessThan(x => x.Salary, 40000)
                .AsApiFilter();
            Assert.AreEqual("Salary $lt 40000", filterString);
        }

        [TestMethod]
        public void Creates_filter_with_group()
        {
            var filterString = new Filter<Employee>()
                .LessThan(x => x.Salary, 40000)
                .And()
                .Group(inner => inner.EqualTo(x => x.Name, "Abhijeet").Or().EqualTo(x => x.Department, "Engineering"))
                .AsApiFilter();
            Assert.AreEqual("Salary $lt 40000 $and (Name $eq 'Abhijeet' $or Department $eq 'Engineering')", filterString);
        }
    }

    public class Employee
    {
        public string Name { get; set; }

        public string Department { get; set; }

        public double Salary { get; set; }

        public DateTime JoingingDate { get; set; }
    }
}
