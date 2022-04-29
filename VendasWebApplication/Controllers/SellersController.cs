﻿using Microsoft.AspNetCore.Mvc;
using SalesWebApplication.Models;
using SalesWebApplication.Models.ViewModel;
using SalesWebApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApplication.Controllers
{
  public class SellersController : Controller
  {
    private readonly SellerService _sellerService;
    private readonly DepartmentService _departmentService;

    public SellersController(SellerService sellerService, DepartmentService departmentService)
    {
      _sellerService = sellerService;
      _departmentService = departmentService;
    }

    public IActionResult Index()
    {
      return View(_sellerService.FindAll());
    }

    public IActionResult Create()
    {
      var viewModel = new SellerFormViewModel { Departments = (ICollection<Department>)_departmentService.FindAll() };
      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Seller seller)
    {
      _sellerService.Insert(seller);

      return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var seller = _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }

      return View(seller);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
      _sellerService.Remove(id);
      return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var seller = _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return NotFound();
      }

      return View(seller);
    }
  }
}
