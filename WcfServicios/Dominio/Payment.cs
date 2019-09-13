using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServicios.Dominio
{
    [DataContract]
    public class Payment
    {

        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int order_id { get; set; }
        [DataMember]
        public Decimal amount { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public string payment_date { get; set; }
        [DataMember]
        public string status_culqui { get; set; }
        [DataMember]
        public string response_code_culqui { get; set; }
        [DataMember]
        public string merchant_message_culqui { get; set; }
 
    }
}