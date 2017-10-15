using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Producks.UnderCutters.Dtos
{
    public class UnderCutterBrand
    {
        public virtual string Name { get; set; }
        public virtual int AvailableProductCount { get; set; }
    }
}