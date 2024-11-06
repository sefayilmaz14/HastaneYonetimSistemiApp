using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Types
{
    public class ServiceMassage
    {
        public bool IsSucced { get; set; }
        public string Massage { get; set; }
    }

    public class ServiceMassage<T>
    {
        public bool IsSucced { get; set; }
        public string Massage { get; set; }
        public T? Data { get; set; }
    }
}
