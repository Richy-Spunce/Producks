using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producks.Model;

namespace Producks.Repository
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAllBrands();
        Brand GetBrandByName(string name);
    }
}
