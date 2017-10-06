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
            StoreMainPage model = new StoreMainPage();

            List<StoreCategory> ModelledCategories = new List<StoreCategory>();
            List<StoreBrand> ModelledBrands = new List<StoreBrand>();
            List<StoreProduct> ModelledProducts = new List<StoreProduct>();

            var categories = db.Categories.Where(c => c.Active == true).ToList();
            var brands = db.Brands.Where(b => b.Active == true).ToList();
            var products = db.Products.Where(p => p.Active).ToList();

            foreach (Category cat in categories)
            {
                ModelledCategories.Add(new StoreCategory() {
                    Id = cat.Id,
                    Name = cat.Name,
                    Description = cat.Description
                });
            }

            foreach (Brand br in brands)
            {
                ModelledBrands.Add(new StoreBrand()
                {
                    Id = br.Id,
                    Name = br.Name
                });
            }

            foreach (Product prod in products)
            {
                ModelledProducts.Add(new StoreProduct()
                {
                    Name = prod.Name,
                    Description = prod.Name,
                    Price = prod.Price,
                    StockLevel = (prod.StockLevel > 0 ? "In Stock" : "Out of Stock")
                });
            }

            model.StoreBrands = ModelledBrands;
            model.StoreCategories = ModelledCategories;
            model.StoreProducts = ModelledProducts;

            return View(model);
        }

        public ActionResult ProductsByCategory(int? id)
        {
            List<ProductByCategory> ModelledProducts = new List<ProductByCategory>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var products = db.Products.Where(p => p.CategoryId == id && p.Active == true);

            if (products == null)
            {
                return HttpNotFound();
            }

            foreach (Product prod in products)
            {
                ModelledProducts.Add(new ProductByCategory()
                {
                    Name = prod.Name,
                    Description = prod.Name,
                    Price = prod.Price,
                    StockLevel = (prod.StockLevel > 0 ? "In Stock" : "Out of Stock")
                });
            }

            return View(ModelledProducts);
        }
    }
}