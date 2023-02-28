using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decor.DB
{
    public partial class Product
    {
        public string Color => ActualDiscount > 15 ? "#7fff00" : "Transparent";
    }
}
