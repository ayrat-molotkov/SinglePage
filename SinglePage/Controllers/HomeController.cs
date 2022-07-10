using Microsoft.AspNetCore.Mvc;
using SinglePage.Models;
using SinglePage.Service;
using System.Diagnostics;

namespace SinglePage.Controllers;
public class HomeController : Controller
{    
    private readonly VirtualServerService _virtualServerService;

    public HomeController(VirtualServerService virtualServerService)
    {        
        _virtualServerService = virtualServerService;
    }


    [HttpPost]
    public async Task<JsonResult> AddVirtualServer()
    {
        var newServer = await _virtualServerService.AddNewServer();

        var calculationTime = await _virtualServerService.CalculationWorkServers();

        return Json(new {newServer = newServer, calculationTime = calculationTime, nowTime = DateTime.Now, status = "success" });
    }


    [HttpPost]
    public async Task<JsonResult> DeleteServers(List<int> ids)
    {
        var deleteResult = await _virtualServerService.DeleteServers(ids);

        if(deleteResult)
            return Json(new { status = "success" });

        return Json(new { status = "failed" });
    }


    [HttpPost]
    public async Task<JsonResult> CalculationWorkServers()
    {
        var calculationTime = await _virtualServerService.CalculationWorkServers();

        return Json(new { calculationTime = calculationTime, nowTime = DateTime.Now });
    }


    public async Task<IActionResult> Index()
    {
        var servers = await _virtualServerService.GetAllServers();

        return View(new VirtualServerViewModel(servers));
    }    
}
