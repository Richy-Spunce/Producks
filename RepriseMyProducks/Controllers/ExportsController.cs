using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Producks.Model;

namespace RepriseMyProducks.Controllers
{
    public class ExportsController : ApiController
    {
        private Producks.Model.StoreDb db = new Producks.Model.StoreDb();

        // GET: api/Brands
        [HttpGet]
        [Route("api/Brands")]
        public IEnumerable<Dtos.Brand> GetBrands()
        {
            return db.Brands
                     .AsEnumerable()
                     .Where(b => b.Active == true)
                     .Select(b => new Dtos.Brand
                     {
                         Id = b.Id,
                         Name = b.Name,
                         Active = b.Active
                     });
        }

        [HttpGet]
        [Route("api/Categories")]
        public IEnumerable<Dtos.Category> GetCategories()
        {
            return db.Categories
                     .AsEnumerable()
                     .Where(c => c.Active == true)
                     .Select(c => new Dtos.Category
                     {
                         Id = c.Id,
                         Name = c.Name,
                         Description = c.Description,
                         Active = c.Active
                     });
        }

        [HttpGet]
        [Route("api/Products")]
        public IEnumerable<Product> GetProducts(double? minPrice = null,
                                                     double? maxPrice = null)
        {
            var products = db.Products.AsQueryable();
            if (minPrice != null)
            {
                products = products.Where(p => p.Active == true && p.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                products = products.Where(p => p.Active && p.Price <= maxPrice);
            }

            return products.AsEnumerable();
        }

        public IEnumerable<Product> FindProductsByCategory (int? CategoryId)
        {
            var products = db.Products.AsQueryable();

            if (CategoryId != null)
            {
                products = products.Where(p => p.Active && p.Category.Id == CategoryId);
            }

            return products.AsEnumerable();
        }

        public IEnumerable<Product> FindProductsByBrand (int? BrandId)
        {
            var products = db.Products.AsQueryable();

            if (BrandId != null)
            {
                products = products.Where(p => p.Active == true && p.BrandId == BrandId);
            }

            return products.AsEnumerable();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}