using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepriseMyProducks.Dtos;
using System.Web.Mvc;
using Producks.Model;

namespace RepriseMyProducks.ViewModels
{
    public class StoreVM
    {
        public virtual List<Dtos.Category> Categories { get; set; }
        public virtual List<Dtos.Brand> Brands { get; set; }
        public virtual List<Dtos.Product> Products { get; set; }
        public virtual List<SelectListItem> CategoryList { get; set; }
        public virtual List<SelectListItem> BrandList { get; set; }
        public virtual List<Dtos.Product> HttpProducts { get; set; }
        public virtual int? SelectedCategoryId { get; set; }
        public virtual int? SelectedBrandId { get; set; }
        public StoreVM(int? CategoryId, int? BrandId, List<Dtos.Product> httpProducts)
        {
            if (CategoryId != null)
                this.SelectedCategoryId = CategoryId;

            if (BrandId != null)
                this.SelectedBrandId = BrandId;

            if (httpProducts != null)
                this.HttpProducts = httpProducts;

            RetrieveData();
        }

        private void RetrieveData()
        {
            StoreDb db = new StoreDb();

            Categories = new List<Dtos.Category>();
            Brands = new List<Dtos.Brand>();
            Products = new List<Dtos.Product>();
            CategoryList = new List<SelectListItem>();
            BrandList = new List<SelectListItem>();

            var products = db.Products.AsQueryable();

            if (this.SelectedCategoryId != null)
                products = products.Where(p => p.CategoryId == this.SelectedCategoryId);

            if (this.SelectedBrandId != null)
                products = products.Where(p => p.BrandId == this.SelectedBrandId);

            Products = products.Select(p => new Dtos.Product()
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockLevel = (p.StockLevel > 1 ? "In Stock" : "Out of Stock")
            }).ToList();

            if (this.HttpProducts != null)
                Products = Products.Concat(HttpProducts).ToList();

            Categories = db.Categories.Where(c => c.Active).Select(c => new Dtos.Category()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            Brands = db.Brands.Where(b => b.Active).Select(b => new Dtos.Brand()
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();

            foreach (Dtos.Category category in Categories)
                CategoryList.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });

            foreach (Dtos.Brand brand in Brands)
                BrandList.Add(new SelectListItem() { Text = brand.Name, Value = brand.Id.ToString() });
        }
    }
}