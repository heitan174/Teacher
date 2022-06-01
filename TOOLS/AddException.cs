using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOOLS
{
    public class AddException:Exception
    {
        
        public AddException()
            : base("默认测试")
        {

        }
        public AddException(string message)
            :base(message)
        {

        }
    }
}
