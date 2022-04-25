using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebApplication.Models
{
  public class Seller
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
    public double BaseSalary { get; set; }
    public Department Department { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

    public Seller()
    {
    }

    public Seller(int id, string name, string email, DateTime date, double baseSalary, Department department)
    {
      Id = id;
      Name = name;
      Email = email;
      Date = date;
      BaseSalary = baseSalary;
      Department = department;
    }

    public void AddSales(SalesRecord sr)
    {
      Sales.Add(sr);
    }

    public void RemoveSales(SalesRecord sr)
    {
      Sales.Remove(sr);
    }

    public double GetValueSales(DateTime initialDate, DateTime finalDate) 
    {
      return Sales.Where(sr => sr.Date >= initialDate && sr.Date <= finalDate).Sum(sr => sr.Amount);
    }
  }
}
