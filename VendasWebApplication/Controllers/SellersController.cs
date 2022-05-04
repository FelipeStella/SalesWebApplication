using Microsoft.AspNetCore.Mvc;
using SalesWebApplication.Models;
using SalesWebApplication.Models.ViewModel;
using SalesWebApplication.Services;
using SalesWebApplication.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;
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

    public async Task<IActionResult> Index()
    {
      List<Seller> seller = await _sellerService.FindAllAsync();
      return View(seller);
    }

    public async Task<IActionResult> Create()
    {
      List<Department> departments = await _departmentService.FindAllAsync();
      var viewModel = new SellerFormViewModel { Departments = departments };
      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
      if (!ModelState.IsValid)
      {
        var departments = await _departmentService.FindAllAsync();
        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        return View(viewModel);
      }

      await _sellerService.InsertAsync(seller);

      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new {Message = "Id not provided" });
      }

      var seller = await _sellerService.FindByIdAsync(id.Value);

      if (seller == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not found" });
      }

      return View(seller);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _sellerService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
      }
      catch (IntegrityException ie)
      {
        return RedirectToAction(nameof(Error), new { Message = ie.Message });
      }

    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
      }

      var seller = await _sellerService.FindByIdAsync(id.Value);

      if (seller == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not found" });
      }

      return View(seller);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
      }

      var seller = await _sellerService.FindByIdAsync(id.Value);

      if (seller == null)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not found" });
      }

      List<Department> departments = await _departmentService.FindAllAsync();
      SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

      return View(sellerFormViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id,Seller seller)
    {
      if (!ModelState.IsValid)
      {
        var departments = await _departmentService.FindAllAsync();
        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        return View(viewModel);
      }

      if (Id != seller.Id)
      {
        return RedirectToAction(nameof(Error), new { Message = "Id not mismatch" });
      }

      try
      {
        await _sellerService.UpdateAsync(seller);
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

