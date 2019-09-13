using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServicios.Dominio;

namespace WcfServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPayments" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPayments
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Payments", ResponseFormat = WebMessageFormat.Json)]
        Payment CrearPayment(Payment paymentACrear);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Payments/{id}", ResponseFormat = WebMessageFormat.Json)]
        Payment ObtenerPayment(string id);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Payments/Order/{id}", ResponseFormat = WebMessageFormat.Json)]
        Payment ObtenerOrdenPayment(string id);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Payments", ResponseFormat = WebMessageFormat.Json)]
        Payment ModificarPayment(Payment paymentAModificar);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Payments/{id}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarPayment(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Payments", ResponseFormat = WebMessageFormat.Json)]
        List<Payment> ListarPayment();
    }
}
