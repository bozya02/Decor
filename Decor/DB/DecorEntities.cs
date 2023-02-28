using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decor.DB
{
    public partial class DecorEntities
    {
        private static DecorEntities _context;

        public static DecorEntities GetContext()
        {
            if (_context == null)
                _context = new DecorEntities();

            return _context;
        }
    }
}
