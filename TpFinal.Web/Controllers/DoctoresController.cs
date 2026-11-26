using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;
using TpFinal.BL.Rules;

namespace TpFinal.Web.Controllers;

public class DoctoresController : Controller
{
    private readonly IDoctorService _doctorService;

    public DoctoresController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public async Task<IActionResult> Index()
    {
        var doctores = await _doctorService.GetAllAsync();

        return View(doctores);
    }

    public async Task<IActionResult> Details(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpGet]
    public IActionResult Create()
    {
        CargarEspecialidades();

        return View(new DoctorDTO());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorDTO dto)
    {
        if (!ModelState.IsValid)
        {
            CargarEspecialidades();

            return View(dto);
        }

        try
        {
            await _doctorService.CreateAsync(dto);

            TempData["Mensaje"] = "El doctor se creó correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            CargarEspecialidades();

            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        CargarEspecialidades();

        return View(doctor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DoctorDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            CargarEspecialidades();

            return View(dto);
        }

        try
        {
            var actualizado = await _doctorService.UpdateAsync(id, dto);

            if (!actualizado)
            {
                return NotFound();
            }

            TempData["Mensaje"] = "El doctor se actualizó correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            CargarEspecialidades();

            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(DoctorDTO dto)
    {
        try
        {
            var eliminado = await _doctorService.DeleteAsync(dto.Id);

            if (!eliminado)
            {
                return NotFound();
            }

            TempData["Mensaje"] = "El doctor se eliminó correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            TempData["Error"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    private void CargarEspecialidades()
    {
        ViewBag.Especialidades = new SelectList(Especialidades.Lista);
    }
}
