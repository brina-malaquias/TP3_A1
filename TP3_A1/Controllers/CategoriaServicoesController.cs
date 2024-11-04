using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP3_A1.Models;

namespace TP3_A1.Controllers
{
    [Authorize]
    public class CategoriaServicoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoriaServicoes
        public ActionResult Index()
        {
            return View(db.CategoriaServicos.ToList());
        }

        // GET: CategoriaServicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaServico categoriaServico = db.CategoriaServicos.Find(id);
            if (categoriaServico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaServico);
        }

        // GET: CategoriaServicoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaServicoes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoriaServicoId,Nome")] CategoriaServico categoriaServico)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaServicos.Add(categoriaServico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaServico);
        }

        // GET: CategoriaServicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaServico categoriaServico = db.CategoriaServicos.Find(id);
            if (categoriaServico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaServico);
        }

        // POST: CategoriaServicoes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaServicoId,Nome")] CategoriaServico categoriaServico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaServico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaServico);
        }

        // GET: CategoriaServicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaServico categoriaServico = db.CategoriaServicos.Find(id);
            if (categoriaServico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaServico);
        }

        // POST: CategoriaServicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaServico categoriaServico = db.CategoriaServicos.Find(id);
            db.CategoriaServicos.Remove(categoriaServico);
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
