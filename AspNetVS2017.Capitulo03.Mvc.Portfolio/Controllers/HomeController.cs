using AspNetVS2017.Capitulo03.Mvc.Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetVS2017.Capitulo03.Mvc.Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        //public ActionResult Contact(FormCollection formulario)
        //public ActionResult Contact(string nome, string email, string mensagem)
        public ActionResult Contact(ContatoViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var portfolioConnectionString = ConfigurationManager.ConnectionStrings["porftolioConnectionString"].ConnectionString;

            using (var conexao = new SqlConnection(portfolioConnectionString))
            {
                conexao.Open();
                const string instrucao = @"              
                                        INSERT INTO [dbo].[Contato]
                                        ([Nome]
                                        ,[Email]
                                        ,[Messagem])
                                        VALUES
                                        (@Nome
                                        ,@Email 
                                        ,@Messagem) ";

                using (var comando = new SqlCommand(instrucao, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", viewModel.Nome);
                    comando.Parameters.AddWithValue("Email", viewModel.Email);
                    comando.Parameters.AddWithValue("@Messagem", viewModel.Mensagem);

                    comando.ExecuteNonQuery();
                }
            }

            ModelState.Clear();
            return View();
        }
    }
}