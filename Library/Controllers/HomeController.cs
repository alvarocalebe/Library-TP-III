using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models; // Certifique-se de que o namespace do seu modelo esteja aqui
using System.Data.Entity; // Adicione esta linha para usar Include

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // Adicionando o contexto do banco de dados

        public ActionResult Index()
        {
            // Buscando apenas os livros cadastrados que estão disponíveis
            var livros = db.Livros.Include("Autor").Include("Categoria")
                                  .Where(l => l.Disponivel) // Filtra apenas os livros disponíveis
                                  .ToList(); // Converte para lista

            // Buscando todas as categorias cadastradas
            var categorias = db.Categorias.ToList(); // Incluindo as categorias

            // Passando a lista de categorias para a view através do ViewBag
            ViewBag.Categorias = categorias;

            return View(livros); // Retornando a lista de livros disponíveis para a view
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
