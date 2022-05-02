using Microsoft.AspNetCore.Mvc;
using SalesWebApplication.Models;
using SalesWebApplication.Models.ViewModel;
using SalesWebApplication.Services;
using SalesWebApplication.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;

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
      var viewModel = new SellerFormViewModel { Departments = _departmentService.FindAll() };
      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Seller seller)
    {
      if (!ModelState.IsValid)
      {
        var departments = _departmentService.FindAll();
        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        return View(viewModel);
      }

      _sellerService.Insert(seller);

      return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new {Message = "Id not provided" });
      }

      var seller = _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not found" });
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
        return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
      }

      var seller = _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not found" });
      }

      return View(seller);
    }

    public IActionResult Edit(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
      }

      var seller = _sellerService.FindById(id.Value);

      if (seller == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not found" });
      }

      List<Department> departments = _departmentService.FindAll();
      SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

      return View(sellerFormViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int Id,Seller seller)
    {
      if (!ModelState.IsValid)
      {
        var departments = _departmentService.FindAll();
        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        return View(viewModel);
      }

      if (Id != seller.Id)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not mismatch" });
      }

      try
      {
        _sellerService.Update(seller);
        return RedirectToAction(nameof(Error));
      }
      catch (NotFoundException nfe)
      {
        return RedirectToAction(nameof(Error), new { Message = nfe.Message });
      }
      catch (DbConcurrencyException dce)
      {
        return RedirectToAction(nameof(Error), new { Message = dce.Message });
      }
    }

    public IActionResult Error(string message)
    {
      var errorViewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
      return View(errorViewModel);
    }
  }
}

