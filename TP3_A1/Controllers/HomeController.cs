using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP3_A1.Models;

namespace TP3_A1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) // Verifica se o usuário está logado
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return View();
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

        private readonly ApplicationDbContext _context;

        // Injetando o ApplicationDbContext
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Home page para logados.";

            var userId = User.Identity.GetUserId();

            // Busque os veículos e serviços do usuário logado, iniciando as listas com listas vazias se necessário
            var veiculos = _context.Veiculoes.Where(v => v.UserId == userId).ToList() ?? new List<Veiculo>();
            var servicos = _context.Servicos.Where(s => s.Veiculo.UserId == userId).ToList() ?? new List<Servico>();
            var despesas = _context.Despesas.Where(s => s.Veiculo.UserId == userId).ToList() ?? new List<Despesa>();


            var viewModel = new DashboardViewModel
            {
                Veiculos = veiculos,
                Servicos = servicos,
                Despesas = despesas
            };

            return View(viewModel);
        }

    }
}