using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServicesTest
{
    class Payment
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public Decimal amount { get; set; }
        public int status { get; set; }
        public string payment_date { get; set; }
        public string status_culqui { get; set; }
        public string response_code_culqui { get; set; }
        public string merchant_message_culqui { get; set; }
    }

}
