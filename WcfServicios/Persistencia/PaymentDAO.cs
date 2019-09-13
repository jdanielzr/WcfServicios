using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WcfServicios.Dominio;

namespace WcfServicios.Persistencia
{
    public class PaymentDAO
    {
        private string CadenaConexion = "Data Source=DESKTOP-EJM18E6;Initial Catalog=Restaurant;User ID=sa;Password=12345";
        public Payment Crear(Payment PaymentACrear)
        {
            Int32 newId;
            Payment PaymentCreado = null;
            string sql = "INSERT INTO  payment (order_id ,amount,status,payment_date,status_culqui,response_code_culqui,merchant_message_culqui) OUTPUT INSERTED.ID  VALUES (@order_id,@amount,@status,@payment_date ,@status_culqui,@response_code_culqui,@merchant_message_culqui)";
            using (SqlConnection conex = new SqlConnection(CadenaConexion))
            {
                conex.Open();
                using (SqlCommand comando = new SqlCommand(sql, conex))
                {
                    comando.Parameters.Add(new SqlParameter("@order_id", PaymentACrear.order_id));
                    comando.Parameters.Add(new SqlParameter("@amount", PaymentACrear.amount));
                    comando.Parameters.Add(new SqlParameter("@status", PaymentACrear.status));
                    comando.Parameters.Add(new SqlParameter("@payment_date", PaymentACrear.payment_date));
                    comando.Parameters.Add(new SqlParameter("@status_culqui", PaymentACrear.status_culqui));
                    comando.Parameters.Add(new SqlParameter("@response_code_culqui", PaymentACrear.response_code_culqui));
                    comando.Parameters.Add(new SqlParameter("@merchant_message_culqui", PaymentACrear.merchant_message_culqui));
                    //comando.ExecuteNonQuery();
                    newId = (Int32)comando.ExecuteScalar();

                }
            }


            PaymentCreado = Obtener(newId);
            return PaymentCreado;
        }
        public Payment Obtener(int id)
        {
            Payment PaymentEncontrado = null;
            string sql = "SELECT * FROM payment WHERE id = @id";
            using (SqlConnection conex = new SqlConnection(CadenaConexion))
            {
                conex.Open();
                using (SqlCommand comando = new SqlCommand(sql, conex))
                {
                    comando.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            PaymentEncontrado = new Payment()
                            {
                                id = (int)resultado["id"],
                                order_id = (int)resultado["order_id"],
                                amount = (Decimal)resultado["amount"],
                                status = (int)resultado["status"],
                                payment_date = (string)resultado["payment_date"].ToString (),
                                status_culqui = (string)resultado["status_culqui"],
                                response_code_culqui = (string)resultado["response_code_culqui"],
                                merchant_message_culqui = (string)resultado["merchant_message_culqui"]

                        };
                        }
                    }
                }
                return PaymentEncontrado;
            }
        }
        public Payment ObtenerxOrden(int orden)
        {
            Payment PaymentEncontrado = null;
            string sql = "SELECT * FROM payment WHERE order_id = @id";
            using (SqlConnection conex = new SqlConnection(CadenaConexion))
            {
                conex.Open();
                using (SqlCommand comando = new SqlCommand(sql, conex))
                {
                    comando.Parameters.Add(new SqlParameter("@id", orden));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            PaymentEncontrado = new Payment()
                            {
                                id = (int)resultado["id"],
                                order_id = (int)resultado["order_id"],
                                amount = (Decimal)resultado["amount"],
                                status = (int)resultado["status"],
                                payment_date = (string)resultado["payment_date"].ToString(),
                                status_culqui = (string)resultado["status_culqui"],
                                response_code_culqui = (string)resultado["response_code_culqui"],
                                merchant_message_culqui = (string)resultado["merchant_message_culqui"]

                            };
                        }
                    }
                }
                return PaymentEncontrado;
            }
        }
        public Payment Modificar(Payment PaymentAModificar)
        {
            Payment PaymentModificado = null;
            string sql = "UPDATE  payment SET order_id=@order_id,amount=@amount,status=@status,payment_date=@payment_date,status_culqui=@status_culqui,response_code_culqui=@response_code_culqui,merchant_message_culqui=@merchant_message_culqui WHERE  id=@id";
            using (SqlConnection conex = new SqlConnection(CadenaConexion))
            {
                conex.Open();
                using (SqlCommand comando = new SqlCommand(sql, conex))
                {
                    comando.Parameters.Add(new SqlParameter("@order_id", PaymentAModificar.order_id));
                    comando.Parameters.Add(new SqlParameter("@amount", PaymentAModificar.amount));
                    comando.Parameters.Add(new SqlParameter("@status", PaymentAModificar.status));
                    comando.Parameters.Add(new SqlParameter("@payment_date", PaymentAModificar.payment_date));
                    comando.Parameters.Add(new SqlParameter("@status_culqui", PaymentAModificar.status_culqui));
                    comando.Parameters.Add(new SqlParameter("@response_code_culqui", PaymentAModificar.response_code_culqui));
                    comando.Parameters.Add(new SqlParameter("@merchant_message_culqui", PaymentAModificar.merchant_message_culqui));
                    comando.Parameters.Add(new SqlParameter("@id", PaymentAModificar.id));

                    comando.ExecuteNonQuery();
                }
            }
            PaymentModificado = Obtener(PaymentAModificar.id);
            return PaymentModificado;
        }
        public void Eliminar(int id)
        {
            string sql = "DELETE FROM payment WHERE id = @id";
            using (SqlConnection conex = new SqlConnection(CadenaConexion))
            {
                conex.Open();
                using (SqlCommand comando = new SqlCommand(sql, conex))
                {
                    comando.Parameters.Add(new SqlParameter("@id", id));
                    comando.ExecuteNonQuery();
                }
            }
        }
        public List<Payment> Listar()
        {
            List<Payment> PaymentsEncontrados = new List<Payment>();
            Payment PaymentEncontrado = null;
            string sql = "SELECT * FROM payment";
            using (SqlConnection conex = new SqlConnection(CadenaConexion))
            {
                conex.Open();
                using (SqlCommand comando = new SqlCommand(sql, conex))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            PaymentEncontrado = new Payment()
                            {
                                id = (int)resultado["id"],
                                order_id = (int)resultado["order_id"],
                                amount = (Decimal)resultado["amount"],
                                status = (int)resultado["status"],
                                payment_date = (string)resultado["payment_date"].ToString(),
                                status_culqui = (string)resultado["status_culqui"],
                                response_code_culqui = (string)resultado["response_code_culqui"],
                                merchant_message_culqui = (string)resultado["merchant_message_culqui"]
                            };
                            PaymentsEncontrados.Add(PaymentEncontrado);
                        }
                    }
                }
            }
            return PaymentsEncontrados;
        }
    }
}