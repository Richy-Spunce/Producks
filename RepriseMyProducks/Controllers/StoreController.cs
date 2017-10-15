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
using Producks.UnderCutters;
using Producks.UnderCutters.Dtos;

namespace RepriseMyProducks.Controllers
{
    public class StoreController : Controller
    {
        private StoreDb db = new StoreDb();
        private ServiceProxy proxy = new ServiceProxy();

        // GET: Store
        public ActionResult Index(int? CategoryFilter, int? BrandFilter)
        {
            IEnumerable<UnderCutterProduct> httpProducts = proxy.GetAllProducts();
            List<Dtos.Product> Products = new List<Dtos.Product>();

            foreach (UnderCutterProduct p in httpProducts)
                Products.Add(new Dtos.Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockLevel = (p.InStock ? "In Stock" : "Out of Stock")
                });

            StoreVM storeVM = new StoreVM(CategoryFilter, BrandFilter, Products);

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