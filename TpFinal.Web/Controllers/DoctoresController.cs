using Microsoft.AspNetCore.Mvc;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;

namespace TpFinal.Web.Controllers;

public class DoctoresController : Controller
{
    private readonly IDoctorService _doctorService;

    public DoctoresController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    // GET: Doctores
    public async Task<IActionResult> Index()
    {
        var doctores = await _doctorService.GetAllAsync();
        return View(doctores);
    }

    // GET: Doctores/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor == null)
            return NotFound();

        return View(doctor);
    }

    // GET: Doctores/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Doctores/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            await _doctorService.CreateAsync(dto);
            TempData["Mensaje"] = "Doctor registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError("Matricula", ex.Message);
            return View(dto);
        }
    }

    // GET: Doctores/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor == null)
            return NotFound();

        return View(doctor);
    }

    // POST: Doctores/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DoctorDTO dto)
    {
        if (id != dto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(dto);

        try
        {
            var actualizado = await _doctorService.UpdateAsync(id, dto);

            if (!actualizado)
                return NotFound();

            TempData["Mensaje"] = "Doctor actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError("Matricula", ex.Message);
            return View(dto);
        }
    }

    // GET: Doctores/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor == null)
            return NotFound();

        return View(doctor);
    }

    // POST: Doctores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var eliminado = await _doctorService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            TempData["Mensaje"] = "Doctor eliminado correctamente.";
        }
        catch (BusinessException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
