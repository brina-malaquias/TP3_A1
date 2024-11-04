using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TP3_A1.Models;



namespace TP3_A1.Controllers
{
    [Authorize]
    public class ServicoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ApplicationDbContext _context;
        public ServicoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicoes
        public ActionResult Index()
        {
            // Carrega os serviços, incluindo as categorias através da tabela intermediária
            var servicos = db.Servicos
                .Include(s => s.Oficina)
                .Include(s => s.Veiculo)
                .Include(s => s.ServicoCategorias.Select(sc => sc.CategoriaServico)); // Inclui as categorias associadas

            return View(servicos.ToList()); // Passa os dados para a visualização
        }

        // GET: Servicoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            // Incluindo as categorias de serviço e outras propriedades necessárias
            var servico = await _context.Servicos
                .Include(s => s.Oficina) // Inclui a oficina associada
                .Include(s => s.Veiculo) // Inclui o veículo associado
                .Include(s => s.ServicoCategorias.Select(sc => sc.CategoriaServico)) // Inclui as categorias de serviço
                .FirstOrDefaultAsync(m => m.ServicoId == id);

            if (servico == null)
            {
                return HttpNotFound();
            }

            return View(servico);
        }


        // GET: Servicoes/Create
        public ActionResult Create()
        {
            ViewBag.OficinaId = new SelectList(db.Oficinas, "OficinaId", "Nome");
            ViewBag.VeiculoId = new SelectList(db.Veiculoes, "VeiculoId", "Modelo");

            // Adiciona as categorias de serviço ao ViewBag
            ViewBag.CategoriaServicoId = new SelectList(db.CategoriaServicos, "CategoriaServicoId", "Nome");

            return View();
        }

        // POST: Servicoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicoId,Data,Status,Observacoes,VeiculoId,OficinaId")] Servico servico, int[] selectedCategorias)
        {
            if (ModelState.IsValid)
            {
                db.Servicos.Add(servico);
                db.SaveChanges();

                if (selectedCategorias != null)
                {
                    foreach (var categoriaId in selectedCategorias)
                    {
                        db.ServicoCategorias.Add(new ServicoCategoria
                        {
                            ServicoId = servico.ServicoId,
                            CategoriaServicoId = categoriaId
                        });
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.OficinaId = new SelectList(db.Oficinas, "OficinaId", "Nome", servico.OficinaId);
            ViewBag.VeiculoId = new SelectList(db.Veiculoes, "VeiculoId", "Modelo", servico.VeiculoId);
            ViewBag.CategoriaServicoId = new SelectList(db.CategoriaServicos, "CategoriaServicoId", "Nome", selectedCategorias);
            return View(servico);
        }


        // GET: Servicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servicos.Include(s => s.ServicoCategorias).SingleOrDefault(s => s.ServicoId == id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            ViewBag.OficinaId = new SelectList(db.Oficinas, "OficinaId", "Nome", servico.OficinaId);
            ViewBag.VeiculoId = new SelectList(db.Veiculoes, "VeiculoId", "Modelo", servico.VeiculoId);
            ViewBag.CategoriaServicoId = new SelectList(db.CategoriaServicos, "CategoriaServicoId", "Nome");
            return View(servico);
        }

        // POST: Servicoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicoId,Data,Status,Observacoes,VeiculoId,OficinaId")] Servico servico, int[] selectedCategorias)
        {
            if (ModelState.IsValid)
            {
                // Atualiza o serviço
                db.Entry(servico).State = EntityState.Modified;

                // Atualiza as associações de categorias
                var existingCategories = db.ServicoCategorias.Where(sc => sc.ServicoId == servico.ServicoId).ToList();

                // Remove associações que não estão mais selecionadas
                foreach (var existingCategory in existingCategories)
                {
                    if (!selectedCategorias.Contains(existingCategory.CategoriaServicoId))
                    {
                        db.ServicoCategorias.Remove(existingCategory);
                    }
                }

                // Adiciona novas associações selecionadas
                foreach (var categoriaId in selectedCategorias)
                {
                    if (!existingCategories.Any(ec => ec.CategoriaServicoId == categoriaId))
                    {
                        db.ServicoCategorias.Add(new ServicoCategoria
                        {
                            ServicoId = servico.ServicoId,
                            CategoriaServicoId = categoriaId
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recarrega as listas de dropdowns em caso de erro
            ViewBag.OficinaId = new SelectList(db.Oficinas, "OficinaId", "Nome", servico.OficinaId);
            ViewBag.VeiculoId = new SelectList(db.Veiculoes, "VeiculoId", "Modelo", servico.VeiculoId);
            ViewBag.CategoriaServicoId = new SelectList(db.CategoriaServicos, "CategoriaServicoId", "Nome", selectedCategorias);

            return View(servico);
        }

        // GET: Servicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Servico servico = db.Servicos.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }

            return View(servico);
        }

        // POST: Servicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servico servico = db.Servicos.Find(id);
            if (servico != null) // Verifica se o serviço foi encontrado
            {
                db.Servicos.Remove(servico);
                db.SaveChanges();
            }

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