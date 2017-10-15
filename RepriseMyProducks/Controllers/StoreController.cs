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
using Producks.Repository;

namespace RepriseMyProducks.Controllers
{
    public class StoreController : Controller
    {
        private IProductRepository productRepo;
        private IBrandRepository brandRepo;
        private ICategoryRepository categoryRepo;

        private ServiceProxy proxy;

        public StoreController()
        {
            this.proxy = new ServiceProxy();

            this.productRepo = new ProductRepository(new StoreDb());
            this.brandRepo = new BrandRepository(new StoreDb());
            this.categoryRepo = new CategoryRepository(new StoreDb());
        }
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

            var products = this.productRepo.GetAllProducts();
            var categories = this.categoryRepo.GetAllCategories();
            var brands = this.brandRepo.GetAllBrands();

            List<Dtos.Product> StoreProducts = new List<Dtos.Product>();
            List<Dtos.Category> StoreCategories = new List<Dtos.Category>();
            List<Dtos.Brand> StoreBrands = new List<Dtos.Brand>();

            foreach (var p in products)
                StoreProducts.Add(new Dtos.Product() {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockLevel = p.StockLevel > 1 ? "In Stock" : "Out of Stock",
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId
                });

            foreach (var b in brands)
                StoreBrands.Add(new Dtos.Brand()
                {
                    Id = b.Id,
                    Name = b.Name
                });

            foreach (var c in categories)
                StoreCategories.Add(new Dtos.Category()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                });
           
            StoreVM storeVM = new StoreVM(CategoryFilter, BrandFilter, Products, StoreProducts, StoreCategories, StoreBrands);

            return View(storeVM);
        }

        //public ActionResult ProductsByCategory (int Id)
        //{
        //    IEnumerable<ProductByCategory> productsByCategory =
        //        db.Products.AsEnumerable()
        //                   .Where(p => p.CategoryId == Id && p.Active == true)
        //                   .Select(p => new ViewModels.ProductByCategory(p.Name, p.Description, p.Price, p.StockLevel));

        //    return View(productsByCategory.ToList());
        //}
    }
}