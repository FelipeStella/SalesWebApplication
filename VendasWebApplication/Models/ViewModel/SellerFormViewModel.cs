using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApplication.Models.ViewModel
{
  public class SellerFormViewModel
  {
    public Seller Seller { get; set; }
    public List<Department> Departments { get; set; }
  }
}
