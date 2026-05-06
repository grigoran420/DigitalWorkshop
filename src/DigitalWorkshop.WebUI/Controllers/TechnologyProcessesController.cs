using DigitalWorkshop.Application.Services;
using DigitalWorkshop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigitalWorkshop.WebUI.Controllers
{
    public class TechnologyProcessesController : Controller
    {
        private readonly ITechnologyProcessService _service;

        public TechnologyProcessesController(ITechnologyProcessService service)
        {
            _service = service;
        }

        // GET: TechnologyProcesses
        public async Task<IActionResult> Index()
        {
            var processes = await _service.GetAllAsync();
            return View(processes);
        }

        // GET: TechnologyProcesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var process = await _service.GetByIdAsync(id.Value);
            if (process == null) return NotFound();
            return View(process);
        }

        // GET: TechnologyProcesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TechnologyProcesses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Version,Status")] TechnologyProcess process)
        {
            if (ModelState.IsValid)
            {
                process.CreatedAt = DateTime.UtcNow;
                await _service.CreateAsync(process);
                return RedirectToAction(nameof(Index));
            }
            return View(process);
        }

        // GET: TechnologyProcesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var process = await _service.GetByIdAsync(id.Value);
            if (process == null) return NotFound();
            return View(process);
        }

        // POST: TechnologyProcesses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Version,Status,CreatedAt")] TechnologyProcess process)
        {
            if (id != process.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(process);
                return RedirectToAction(nameof(Index));
            }
            return View(process);
        }

        // GET: TechnologyProcesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var process = await _service.GetByIdAsync(id.Value);
            if (process == null) return NotFound();
            return View(process);
        }

        // POST: TechnologyProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForApproval(int id)
        {
            // В реальном проекте: User.Identity.Name или claims
            await _service.SubmitForApprovalAsync(id, "CurrentUser");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id, string comment)
        {
            await _service.ApproveAsync(id, "LeadTechnologist", comment);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string comment)
        {
            await _service.RejectAsync(id, "Engineer", comment);
            return RedirectToAction(nameof(Index));
        }
    }
}