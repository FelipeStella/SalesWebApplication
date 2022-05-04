using Microsoft.EntityFrameworkCore;
using SalesWebApplication.Data;
using SalesWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApplication.Services
{
  public class DepartmentService
  {
    private readonly SalesWebApplicationContext _contect;
    public DepartmentService(SalesWebApplicationContext contect)
    {
      _contect = contect;
    }
    public async Task<List<Department>> FindAllAsync()
    {
      return await _contect.Department.OrderBy(d => d.Name).ToListAsync();
    }
  }
}
