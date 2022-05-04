using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebApplication.Models
{
  public class Seller
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "{0} is Required")]
    public string Name { get; set; }

    [Display(Name = "E-Mail")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "{0} is Required")]
    [EmailAddress(ErrorMessage = "Enter a valid e-mail")]
    public string Email { get; set; }

    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Required(ErrorMessage = "{0} is Required")]
    [MaxLength(10, ErrorMessage = "Date must follow the pattern dd/MM/yyyy")]
    public DateTime Date { get; set; }

    [Display(Name = "Base Salary")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Required(ErrorMessage = "{0} is Required")]
    [Range(1100.00, 50000.00, ErrorMessage = "The {0} must be from {1} to {2}")]
    public double BaseSalary { get; set; }
    public int DepartmentId { get; set; }
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
