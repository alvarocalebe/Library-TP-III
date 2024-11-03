using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LivrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private string imagemPath = @"C:\Users\alvarocalebe\source\repos\Library\Library\Imagens\";

        // GET: Livros
        public ActionResult Index()
        {
            var livros = db.Livros.Include(l => l.Autor).Include(l => l.Categoria);
            return View(livros.ToList());
        }

        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Carregar o livro com o Autor e Categoria relacionados
            Livro livro = db.Livros
                .Include(l => l.Autor) // Carregar Autor
                .Include(l => l.Categoria) // Carregar Categoria
                .FirstOrDefault(l => l.ID == id);

            if (livro == null)
            {
                return HttpNotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.AutorID = new SelectList(db.Autores, "ID", "Nome");
            ViewBag.CategoriaID = new SelectList(db.Categorias, "ID", "Nome");
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titulo,NomeEditora,AutorID,CategoriaID,Disponivel")] Livro livro, HttpPostedFileBase ImagemUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImagemUpload != null && ImagemUpload.ContentLength > 0)
                {
                    // Gerar o nome da imagem
                    string nomeArquivo = Guid.NewGuid() + Path.GetExtension(ImagemUpload.FileName);
                    string caminhoCompleto = Path.Combine(imagemPath, nomeArquivo);

                    // Salvar a imagem no diretório
                    ImagemUpload.SaveAs(caminhoCompleto);

                    // Atribuir o nome da imagem ao livro
                    livro.NomeImagem = nomeArquivo;
                }

                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutorID = new SelectList(db.Autores, "ID", "Nome", livro.AutorID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "ID", "Nome", livro.CategoriaID);
            return View(livro);
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutorID = new SelectList(db.Autores, "ID", "Nome", livro.AutorID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "ID", "Nome", livro.CategoriaID);
            return View(livro);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titulo,NomeEditora,AutorID,CategoriaID,Disponivel,NomeImagem")] Livro livro, HttpPostedFileBase ImagemUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImagemUpload != null && ImagemUpload.ContentLength > 0)
                {
                    // Gerar novo nome de imagem
                    string novoNomeArquivo = Guid.NewGuid() + Path.GetExtension(ImagemUpload.FileName);
                    string caminhoCompleto = Path.Combine(imagemPath, novoNomeArquivo);

                    // Salvar nova imagem
                    ImagemUpload.SaveAs(caminhoCompleto);

                    // Excluir imagem antiga, se houver
                    if (!string.IsNullOrEmpty(livro.NomeImagem))
                    {
                        string caminhoAntigo = Path.Combine(imagemPath, livro.NomeImagem);
                        if (System.IO.File.Exists(caminhoAntigo))
                        {
                            System.IO.File.Delete(caminhoAntigo);
                        }
                    }

                    // Atribuir o novo nome ao livro
                    livro.NomeImagem = novoNomeArquivo;
                }
                else
                {
                    // Se nenhuma nova imagem foi carregada, manter o nome da imagem existente
                    var livroExistente = db.Livros.AsNoTracking().FirstOrDefault(l => l.ID == livro.ID);
                    if (livroExistente != null)
                    {
                        livro.NomeImagem = livroExistente.NomeImagem;
                    }
                }

                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorID = new SelectList(db.Autores, "ID", "Nome", livro.AutorID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "ID", "Nome", livro.CategoriaID);
            return View(livro);
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livros.Find(id);

            // Remover a imagem do diretório
            if (!string.IsNullOrEmpty(livro.NomeImagem))
            {
                string caminhoImagem = Path.Combine(imagemPath, livro.NomeImagem);
                if (System.IO.File.Exists(caminhoImagem))
                {
                    System.IO.File.Delete(caminhoImagem);
                }
            }

            db.Livros.Remove(livro);
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
