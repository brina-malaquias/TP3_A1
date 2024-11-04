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
    public class TipoDespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoDespesas
        public ActionResult Index()
        {
            return View(db.TipoDespesas.ToList());
        }

        // GET: TipoDespesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            if (tipoDespesa == null)
            {
                return HttpNotFound();
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDespesas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoDespesaId,Nome")] TipoDespesa tipoDespesa)
        {
            if (ModelState.IsValid)
            {
                db.TipoDespesas.Add(tipoDespesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            if (tipoDespesa == null)
            {
                return HttpNotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoDespesaId,Nome")] TipoDespesa tipoDespesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDespesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            if (tipoDespesa == null)
            {
                return HttpNotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            db.TipoDespesas.Remove(tipoDespesa);
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
