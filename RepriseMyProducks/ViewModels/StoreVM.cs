using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepriseMyProducks.Dtos;
using System.Web.Mvc;
using Producks.Model;
using Producks.Repository;

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
      
        public StoreVM(int? CategoryId, int? BrandId, List<Dtos.Product> httpProducts, 
            List<Dtos.Product> Products, List<Dtos.Category> Categories, List<Dtos.Brand> Brands)
        {
            if (Products != null)
                this.Products = Products;

            if (Categories != null)
                this.Categories = Categories;

            if (Brands != null)
                this.Brands = Brands;

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
            CategoryList = new List<SelectListItem>();
            BrandList = new List<SelectListItem>();

            if (this.SelectedCategoryId != null)
                Products = Products.Where(p => p.CategoryId == this.SelectedCategoryId).ToList();

            if (this.SelectedBrandId != null)
                Products = Products.Where(p => p.BrandId == this.SelectedBrandId).ToList();

            if (this.HttpProducts != null)
                Products = Products.Concat(HttpProducts).ToList();

            foreach (Dtos.Category category in this.Categories)
                CategoryList.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });

            foreach (Dtos.Brand brand in this.Brands)
                BrandList.Add(new SelectListItem() { Text = brand.Name, Value = brand.Id.ToString() });
        }
    }
}