using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BLFactory
    {   
        public static IBL GetBL()
        {
            return BLImp1.Instance;
        }
    }
}
