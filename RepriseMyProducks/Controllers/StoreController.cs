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
        public ActionResult Index(int? CategoryFilter, int? BrandFilter)
        {
            StoreVM storeVM = new StoreVM(CategoryFilter, BrandFilter);

            return View(storeVM);
        }

        public ActionResult ProductsByCategory (int Id)
        {
            IEnumerable<ProductByCategory> productsByCategory =
                db.Products.AsEnumerable()
                           .Where(p => p.CategoryId == Id && p.Active == true)
                           .Select(p => new ViewModels.ProductByCategory(p.Name, p.Description, p.Price, p.StockLevel));

            return View(productsByCategory.ToList());
        }
    }
}