using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producks.Model;

namespace Producks.Repository
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductByName(string name);

    }
}
