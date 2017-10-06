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
        public IEnumerable<Dtos.Product> GetProducts([FromUri] Int32? CategoryId = null, [FromUri] Int32? BrandId = null, [FromUri] Double? StartPrice = null, [FromUri] Double? EndPrice = null)
        {

            return db.Products
                     .AsEnumerable()
                     .Where(p => (p.Category.Active == true && p.Brand.Active == true && p.Active == true) && (
                                 (p.CategoryId == CategoryId) || 
                                 (p.BrandId == BrandId) || 
                                 (p.Price >= StartPrice && p.Price <= EndPrice))
                     )
                     .Select(p => new Dtos.Product
                     {
                         Id = p.Id,
                         Name = p.Name,
                         Description = p.Description,
                         Price = p.Price,
                         StockLevel = p.StockLevel
                     });

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