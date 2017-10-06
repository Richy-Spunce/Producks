using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepriseMyProducks.ViewModels
{
    public class StoreMainPage
    {
        public virtual IEnumerable<StoreBrand> StoreBrands { get; set; }

        public virtual IEnumerable<StoreCategory> StoreCategories { get; set; }

        public virtual IEnumerable<StoreProduct> StoreProducts { get; set; }
    }
}