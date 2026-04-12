using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AM_Supplement.Dashboard.Models;
using AM_Supplement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AM_Supplement.Dashboard.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AMSublementDbContext _db;

    public HomeController(ILogger<HomeController> logger, AMSublementDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var totalProducts = await _db.Products.CountAsync();
        var totalOrders = await _db.Orders.CountAsync();
        var totalRevenue = await _db.Orders.SumAsync(o => o.TotalAmount);

        // sales per week (last 4)
        var start = DateTime.UtcNow.Date.AddDays(-28);
        var recentOrders = await _db.Orders
            .Where(o => o.OrderDate >= start)
            .Select(o => new { o.OrderDate, o.TotalAmount })
            .ToListAsync();

        var series = new decimal[4];
        var labels = new string[4];
        var todayWeek = System.Globalization.ISOWeek.GetWeekOfYear(DateTime.UtcNow);
        var year = DateTime.UtcNow.Year;
        for (int i = 0; i < 4; i++)
        {
            var w = todayWeek - (3 - i);
            labels[i] = $"W{w}";
            var match = recentOrders
                .Where(r => System.Globalization.ISOWeek.GetWeekOfYear(r.OrderDate) == w && r.OrderDate.Year == year)
                .Sum(r => r.TotalAmount);
            series[i] = match;
        }

        ViewBag.TotalProducts = totalProducts;
        ViewBag.TotalOrders = totalOrders;
        ViewBag.TotalRevenue = totalRevenue;
        ViewBag.SalesLabels = JsonSerializer.Serialize(labels);
        ViewBag.SalesSeries = JsonSerializer.Serialize(series);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
