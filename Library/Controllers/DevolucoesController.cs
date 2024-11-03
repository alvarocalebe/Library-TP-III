using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class DevolucoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Devolucoes
        public ActionResult Index()
        {
            var devolucoes = db.Devolucoes.Include(d => d.Reserva);
            return View(devolucoes.ToList());
        }

        // GET: Devolucoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucao devolucao = db.Devolucoes.Find(id);
            if (devolucao == null)
            {
                return HttpNotFound();
            }
            return View(devolucao);
        }

        // GET: Devolucoes/Create
        public ActionResult Create()
        {
            ViewBag.ReservaID = new SelectList(db.Reservas, "ID", "UsuarioID");
            return View();
        }

        // POST: Devolucoes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ReservaID,DataDevolucao")] Devolucao devolucao)
        {
            if (ModelState.IsValid)
            {
                db.Devolucoes.Add(devolucao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservaID = new SelectList(db.Reservas, "ID", "UsuarioID", devolucao.ReservaID);
            return View(devolucao);
        }

        // GET: Devolucoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucao devolucao = db.Devolucoes.Find(id);
            if (devolucao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservaID = new SelectList(db.Reservas, "ID", "UsuarioID", devolucao.ReservaID);
            return View(devolucao);
        }

        // POST: Devolucoes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ReservaID,DataDevolucao")] Devolucao devolucao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devolucao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservaID = new SelectList(db.Reservas, "ID", "UsuarioID", devolucao.ReservaID);
            return View(devolucao);
        }

        // GET: Devolucoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucao devolucao = db.Devolucoes.Find(id);
            if (devolucao == null)
            {
                return HttpNotFound();
            }
            return View(devolucao);
        }

        // POST: Devolucoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devolucao devolucao = db.Devolucoes.Find(id);
            db.Devolucoes.Remove(devolucao);
            db.SaveChanges();
            return RedirectToAction("Index");
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
