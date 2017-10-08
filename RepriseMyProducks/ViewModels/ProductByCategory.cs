using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepriseMyProducks.ViewModels
{
    public class ProductByCategory
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual double Price { get; set; }

        [Display(Name = "Stock Level")]
        public virtual string StockLevel { get; set; }

        public ProductByCategory() { }

        public ProductByCategory(string Name, string Description, double Price, int StockLevel)
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;

            CheckStockLevel(StockLevel);
        }

        public void CheckStockLevel(int StockLevel)
        {
            if (StockLevel > 1)
            {
                this.StockLevel = "In Stock";
            } else
            {
                this.StockLevel = "Out of Stock";
            }
        }
    }
}