using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace WCFServicesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1CrearPayment()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Payment paymentACrear = new Payment()
            { amount = 1000,
                 merchant_message_culqui ="",
                order_id = 5,
                payment_date = "2019-01-01",
                response_code_culqui ="",
                status = 0,
                status_culqui =""
            };

            string postdata = js.Serialize(paymentACrear);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
            Create("http://localhost:2153/Payments.svc/Payments");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            Payment paymentCreado = js.Deserialize<Payment>(tramaJson);
            Assert.AreEqual( 1000, paymentCreado.amount);
            Assert.AreEqual("", paymentCreado.merchant_message_culqui);
            Assert.AreEqual(5, paymentCreado.order_id);
            DateTime fecha = DateTime.Parse(paymentCreado.payment_date.ToString());
             Debug.Write(fecha.ToString("yyyy-MM-dd"));
            Assert.AreEqual("2019-01-01", fecha.ToString("yyyy-MM-dd"));
            Assert.AreEqual("", paymentCreado.response_code_culqui);
            Assert.AreEqual(0, paymentCreado.status);
            Assert.AreEqual("", paymentCreado.status_culqui);

        }
        [TestMethod]
        public void Test2CrearPaymentDuplicado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Payment paymentACrear = new Payment()
            {
                amount = 1000,
                merchant_message_culqui = "",
                order_id = 5,
                payment_date = "2019-01-01",
                response_code_culqui = "",
                status = 0,
                status_culqui = ""
            };
            string postdata = js.Serialize(paymentACrear);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
            Create("http://localhost:2153/Payments.svc/Payments");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                Payment paymentCreado = js.Deserialize<Payment>(tramaJson);
                Assert.AreEqual(1000, paymentCreado.amount);
                Assert.AreEqual("", paymentCreado.merchant_message_culqui);
                Assert.AreEqual(5, paymentCreado.order_id);
                DateTime fecha = DateTime.Parse(paymentCreado.payment_date.ToString());
                Debug.Write(fecha.ToString("yyyy-MM-dd"));
                Assert.AreEqual("2019-01-01", fecha.ToString("yyyy-MM-dd"));
                Assert.AreEqual("", paymentCreado.response_code_culqui);
                Assert.AreEqual(0, paymentCreado.status);
                Assert.AreEqual("", paymentCreado.status_culqui);
            }
            catch (WebException e)
            {
                HttpStatusCode codigo = ((HttpWebResponse)e.Response).StatusCode;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
               PaymentException error = js.Deserialize<PaymentException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("Ya existe un pago para esta orden", error.Descripcion);
            }
        }

        [TestMethod]
        public void Test3Obtener()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:2153/Payments.svc/Payments/10");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Payment paymentObtenido = js.Deserialize<Payment>(tramaJson);
             Assert.AreEqual(1000, paymentObtenido.amount);
            Assert.AreEqual("", paymentObtenido.merchant_message_culqui);
            Assert.AreEqual(5, paymentObtenido.order_id);
            DateTime fecha = DateTime.Parse(paymentObtenido.payment_date.ToString());
            Debug.Write(fecha.ToString("yyyy-MM-dd"));
            Assert.AreEqual("2019-01-01", fecha.ToString("yyyy-MM-dd"));
            Assert.AreEqual("", paymentObtenido.response_code_culqui);
            Assert.AreEqual(0, paymentObtenido.status);
            Assert.AreEqual("", paymentObtenido.status_culqui);
            Assert.AreEqual(10, paymentObtenido.id);

        }


        [TestMethod]
        public void Test4Modificar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Payment paymentAModificar = new Payment()
            {
                amount = 1500,
                merchant_message_culqui = "Pago registrado",
                order_id = 5,
                payment_date = "2019-01-01",
                response_code_culqui = "Adhvgaq837t128",
                status = 0,
                status_culqui = "Paid",
                id=10
            };
            string postdata = js.Serialize(paymentAModificar);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
            Create("http://localhost:2153/Payments.svc/Payments");
            request.Method = "PUT";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            Payment paymentActualizado = js.Deserialize<Payment>(tramaJson);
            Assert.AreEqual(1500, paymentActualizado.amount);
            Assert.AreEqual("Pago registrado", paymentActualizado.merchant_message_culqui);
            Assert.AreEqual(5, paymentActualizado.order_id);
            DateTime fecha = DateTime.Parse(paymentActualizado.payment_date.ToString());
            Debug.Write(fecha.ToString("yyyy-MM-dd"));
            Assert.AreEqual("2019-01-01", fecha.ToString("yyyy-MM-dd"));
            Assert.AreEqual("Adhvgaq837t128", paymentActualizado.response_code_culqui);
            Assert.AreEqual(0, paymentActualizado.status);
            Assert.AreEqual("Paid", paymentActualizado.status_culqui);
            Assert.AreEqual(10, paymentActualizado.id);
        }


        [TestMethod]
        public void Test5Eliminar()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:2153/Payments.svc/Payments/2");
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            HttpWebRequest request2 = (HttpWebRequest)WebRequest.
                Create("http://localhost:2153/Payments.svc/Payments/2");
            request2.Method = "GET";
            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            StreamReader reader = new StreamReader(response2.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Payment paymentObtenido = js.Deserialize<Payment>(tramaJson);
            Assert.IsNull(paymentObtenido);
        }


        [TestMethod]
        public void Test6Listar()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:2153/Payments.svc/Payments");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Payment> paymentsObtenidos = js.Deserialize<List<Payment>>(tramaJson);
            Assert.AreEqual(1, paymentsObtenidos.Count);
        }
    }
}
