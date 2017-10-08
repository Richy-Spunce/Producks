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
            SelectList CategoryFilters, BrandFilters;
            SelectListItem SelectedCategory, SelectedBrand;

            SelectedCategory = new SelectListItem();
            SelectedBrand = new SelectListItem();

            var products = db.Products.Where(p => p.Active == true)
                                      .AsQueryable();

            if (CategoryFilter != null)
            {
                products = products.Where(p => p.CategoryId == CategoryFilter);
            }

            if (BrandFilter != null)
            {
                products = products.Where(p => p.BrandId == BrandFilter);
            }

            var filteredProducts = products.AsEnumerable().Select(p => new ViewModels.ProductByCategory(
                                          p.Name,
                                          p.Description,
                                          p.Price,
                                          p.StockLevel
                                      ));

            CategoryFilters = new SelectList(
                db.Categories.Where(c => c.Active == true)
                             .Select(c => new SelectListItem()
                             {
                                 Text = c.Name,
                                 Value = c.Id.ToString() 
                             })
                  , "Value", "Text");

            BrandFilters = new SelectList(
                db.Brands.Where(b => b.Active == true)
                         .Select(b => new SelectListItem()
                         {
                             Text = b.Name,
                             Value = b.Id.ToString()
                         })
                , "Value", "Text");

            if (CategoryFilter != null)
            {
                SelectedCategory = CategoryFilters.FirstOrDefault(c => Convert.ToInt32(c.Value) == CategoryFilter);
            }

            if (BrandFilter != null)
            { 
            SelectedBrand = CategoryFilters.FirstOrDefault(b => Convert.ToInt32(b.Value) == BrandFilter);
            }

            ViewBag.Categories = new SelectList(CategoryFilters, "Value", "Text", SelectedCategory.Value);
            ViewBag.Brands = new SelectList(BrandFilters, "Value", "Text", SelectedBrand.Value);

            return View(filteredProducts.ToList());
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