using Microsoft.AspNetCore.Mvc;
using TpFinal.BL.DTOs;
using TpFinal.BL.Interfaces;

namespace TpFinal.Web.Controllers;

public class PacientesController : Controller
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    // GET: Pacientes
    public async Task<IActionResult> Index()
    {
        var pacientes = await _pacienteService.GetAllAsync();
        return View(pacientes);
    }

    // GET: Pacientes/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
            return NotFound();

        return View(paciente);
    }

    // GET: Pacientes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Pacientes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PacienteDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _pacienteService.CreateAsync(dto);

            TempData["Mensaje"] = "Paciente registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("Dni", ex.Message);
            return View(dto);
        }
    }

    // GET: Pacientes/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
            return NotFound();

        return View(paciente);
    }

    // POST: Pacientes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PacienteDTO dto)
    {
        if (id != dto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            var actualizado = await _pacienteService.UpdateAsync(id, dto);

            if (!actualizado)
                return NotFound();

            TempData["Mensaje"] = "Paciente actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("Dni", ex.Message);
            return View(dto);
        }
    }

    // GET: Pacientes/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
            return NotFound();

        return View(paciente);
    }

    // POST: Pacientes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var eliminado = await _pacienteService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            TempData["Mensaje"] = "Paciente eliminado correctamente.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
