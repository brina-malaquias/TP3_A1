﻿using System;
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
    public class OficinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Oficinas
        public ActionResult Index()
        {
            return View(db.Oficinas.ToList());
        }

        // GET: Oficinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return HttpNotFound();
            }
            return View(oficina);
        }

        // GET: Oficinas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oficinas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OficinaId,Nome,Responsavel,Telefone")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                db.Oficinas.Add(oficina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oficina);
        }

        // GET: Oficinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return HttpNotFound();
            }
            return View(oficina);
        }

        // POST: Oficinas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OficinaId,Nome,Responsavel,Telefone")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oficina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oficina);
        }

        // GET: Oficinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return HttpNotFound();
            }
            return View(oficina);
        }

        // POST: Oficinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oficina oficina = db.Oficinas.Find(id);
            db.Oficinas.Remove(oficina);
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
