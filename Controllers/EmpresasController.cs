using CadastroMongoDB.Config;
using CadastroMongoDB.Models;
using CadastroMongoDB.Utils;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CadastroMongoDB.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ContextoMongodb _context;

        public EmpresasController(ContextoMongodb context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empresas.Find(_ => true).ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var empresa = await _context.Empresas.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            empresa.Id = Guid.NewGuid();
            empresa.Cnpj = Utils.Utils.SemFormatacao(empresa.Cnpj);
            await _context.Empresas.InsertOneAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var empresa = await _context.Empresas.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Empresa empresa)
        {
            var product = await _context.Empresas.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }
            empresa.Cnpj = Utils.Utils.SemFormatacao(empresa.Cnpj);
            await _context.Empresas.ReplaceOneAsync(p => p.Id == id, empresa);
            return RedirectToAction(nameof(Index));
        }

        // POST: Empresas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _context.Empresas.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            await _context.Empresas.DeleteOneAsync(p => p.Id == id);

            return RedirectToAction(nameof(Index));
        }

    }
}