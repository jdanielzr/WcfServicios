using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServicios.Dominio;
using WcfServicios.Errores;
using WcfServicios.Persistencia;

namespace WcfServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Payments" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Payments.svc o Payments.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Payments : IPayments
    {
        private PaymentDAO paymentDAO = new PaymentDAO();
        public Payment CrearPayment(Payment paymentACrear)
        {
            Payment paymentExistente = paymentDAO.ObtenerxOrden(paymentACrear.order_id );
            if (paymentExistente != null)
            {
                throw new WebFaultException<PaymentException>(new PaymentException()
                {
                    Codigo = 102,
                    Descripcion = "Ya existe un pago para esta orden",
                }, HttpStatusCode.Conflict);
            }
            return paymentDAO.Crear(paymentACrear);
        }
        public void EliminarPayment(string id)
        {
            paymentDAO.Eliminar(int.Parse(id));
        }
        public List<Payment> ListarPayment()
        {
            return paymentDAO.Listar();
        }
        public Payment ModificarPayment(Payment paymentAModificar)
        {
            return paymentDAO.Modificar(paymentAModificar);
        }
        public Payment ObtenerPayment(string id)
        {
            return paymentDAO.Obtener(int.Parse(id));
        }

        public Payment ObtenerOrdenPayment(string id)
        {
            return paymentDAO.ObtenerxOrden(int.Parse(id));
        }
    }
}
