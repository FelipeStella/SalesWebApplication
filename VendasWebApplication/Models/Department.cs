using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApplication.Models
{
  public class Department
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    public Department()
    {
    }

    public Department(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public void AddSeller(Seller seller)
    {
      Sellers.Add(seller);
    }

    public double GetValueSales(DateTime initialDate, DateTime finalDate)
    {
      return Sellers.Sum(seller => seller.GetValueSales(initialDate, finalDate));
    }
  }
}
