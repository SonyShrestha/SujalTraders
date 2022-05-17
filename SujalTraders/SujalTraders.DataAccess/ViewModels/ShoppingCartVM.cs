using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> cartList { get; set; }
        public double cartTotal { get; set; }
    }
}
