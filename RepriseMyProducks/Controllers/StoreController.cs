using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Producks.Model;
using RepriseMyProducks.ViewModels;

namespace RepriseMyProducks.Controllers
{
    public class StoreController : Controller
    {
        private StoreDb db = new StoreDb();

        // GET: Store
        public ActionResult Index()
        {
            IEnumerable<ViewModels.StoreCategory> categories =
                db.Categories.AsEnumerable()
                             .Where(c => c.Active == true)
                             .Select(c => new ViewModels.StoreCategory
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Description = c.Description
                             });

            return View(categories.ToList());
        }

        public ActionResult ProductsByCategory (int Id)
        {
            IEnumerable<ProductByCategory> productsByCategory =
                db.Products.AsEnumerable()
                           .Where(p => p.CategoryId == Id && p.Active == true)
                           .Select(p => new ViewModels.ProductByCategory
                           {
                               Name = p.Name,
                               Description = p.Description,
                               Price = p.Price,
                               StockLevel = (p.StockLevel > 1 ? "In Stock" : "Out of Stock")
                           });

            return View(productsByCategory.ToList());
        }
    }
}