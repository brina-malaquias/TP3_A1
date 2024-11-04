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
    public class VeiculoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Veiculoes
        public ActionResult Index()
        {
            var veiculoes = db.Veiculoes.Include(v => v.User);
            return View(veiculoes.ToList());
        }

        // GET: Veiculoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculoes.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // GET: Veiculoes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Veiculoes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VeiculoId,Marca,Modelo,Ano,Quilometragem,Placa,Observacoes,UserId")] Veiculo veiculo)
        {
            if (!veiculo.IsUniquePlate(veiculo.Placa, db))
            {
                ModelState.AddModelError("Placa", "A placa já está registrada para este usuário.");
            }

            if (ModelState.IsValid)
            {
                db.Veiculoes.Add(veiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", veiculo.UserId);
            return View(veiculo);


        }


        // GET: Veiculoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculoes.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", veiculo.UserId);
            return View(veiculo);
        }

        // POST: Veiculoes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VeiculoId,Marca,Modelo,Ano,Quilometragem,Placa,Observacoes,UserId")] Veiculo veiculo)
        {

            var existingVeiculo = db.Veiculoes.Find(veiculo.VeiculoId);

            if (existingVeiculo == null)
            {
                return HttpNotFound();
            }

            // Verifica se a quilometragem é crescente
            if (!existingVeiculo.IsKilometragemIncreasing(veiculo.Quilometragem, existingVeiculo.Quilometragem))
            {
                ModelState.AddModelError("Quilometragem", "A quilometragem deve ser um valor crescente.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(existingVeiculo).CurrentValues.SetValues(veiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", veiculo.UserId);
            return View(veiculo);
        }

        // GET: Veiculoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculoes.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Veiculo veiculo = db.Veiculoes.Find(id);
            db.Veiculoes.Remove(veiculo);
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
