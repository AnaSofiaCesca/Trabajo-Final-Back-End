using Microsoft.AspNetCore.Mvc;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;

namespace TpFinal.Web.Controllers;

public class PacientesController : Controller
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    public async Task<IActionResult> Index()
    {
        var pacientes = await _pacienteService.GetAllAsync();

        return View(pacientes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
        {
            return NotFound();
        }

        return View(paciente);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new PacienteDTO());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PacienteDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _pacienteService.CreateAsync(dto);

            TempData["Mensaje"] = "El paciente se creó correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
        {
            return NotFound();
        }

        return View(paciente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PacienteDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            var actualizado = await _pacienteService.UpdateAsync(id, dto);

            if (!actualizado)
            {
                return NotFound();
            }

            TempData["Mensaje"] = "El paciente se actualizó correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);

        if (paciente == null)
        {
            return NotFound();
        }

        return View(paciente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(PacienteDTO dto)
    {
        try
        {
            var eliminado = await _pacienteService.DeleteAsync(dto.Id);

            if (!eliminado)
            {
                return NotFound();
            }

            TempData["Mensaje"] = "El paciente se eliminó correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            TempData["Error"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }
}