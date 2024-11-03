using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Library.Models;
using Microsoft.AspNet.Identity;

namespace Library.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Livro).Include(r => r.Usuario);
            return View(reservas.ToList());
        }

        // GET: Reservas/MinhasReservas
        public ActionResult MinhasReservas()
        {
            var userId = User.Identity.GetUserId(); // Obter ID do usuário autenticado
            var minhasReservas = db.Reservas.Where(r => r.UsuarioID == userId).Include(r => r.Livro).ToList();
            return View(minhasReservas);
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Carregar a reserva, o livro, o autor e a categoria
            Reserva reserva = db.Reservas
                .Include(r => r.Livro)
                .Include(r => r.Livro.Autor)
                .Include(r => r.Livro.Categoria)
                .FirstOrDefault(r => r.ID == id);

            if (reserva == null)
            {
                return HttpNotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.LivroID = new SelectList(db.Livros.Where(l => l.Disponivel), "ID", "Titulo"); // Somente livros disponíveis
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LivroID")] Reserva reserva) // Removemos UsuarioID do Bind
        {
            if (ModelState.IsValid)
            {
                // Definindo a DataReserva como a data atual
                reserva.DataReserva = DateTime.Now;

                // Definindo a DataExpiracao como uma semana depois da DataReserva
                reserva.DataExpiracao = DateTime.Now.AddDays(7);

                // Definindo o ID do usuário logado
                reserva.UsuarioID = User.Identity.GetUserId();

                // Definir o livro como indisponível
                var livro = db.Livros.Find(reserva.LivroID);
                if (livro != null)
                {
                    livro.Disponivel = false; // Tornar o livro indisponível
                    db.Entry(livro).State = EntityState.Modified;
                }

                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("MinhasReservas");
            }

            ViewBag.LivroID = new SelectList(db.Livros.Where(l => l.Disponivel), "ID", "Titulo", reserva.LivroID);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            if (reserva != null)
            {
                var livro = db.Livros.Find(reserva.LivroID);
                if (livro != null)
                {
                    livro.Disponivel = true; // Tornar o livro disponível novamente
                    db.Entry(livro).State = EntityState.Modified;
                }

                db.Reservas.Remove(reserva);
                db.SaveChanges();
            }
            return RedirectToAction("MinhasReservas");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
