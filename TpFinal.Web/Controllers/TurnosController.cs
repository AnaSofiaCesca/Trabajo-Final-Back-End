using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;

namespace TpFinal.Web.Controllers;

public class TurnosController : Controller
{
    private readonly ITurnoService _turnoService;
    private readonly IDoctorService _doctorService;

    public TurnosController(ITurnoService turnoService, IDoctorService doctorService)
    {
        _turnoService = turnoService;
        _doctorService = doctorService;
    }

    // GET: Turnos
    public async Task<IActionResult> Index()
    {
        var turnos = await _turnoService.GetAllAsync();
        return View(turnos);
    }

    // GET: Turnos/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var turno = await _turnoService.GetByIdAsync(id);

        if (turno == null)
            return NotFound();

        return View(turno);
    }

    // GET: Turnos/Create  (Solicitar turno)
    public async Task<IActionResult> Create()
    {
        await CargarDoctoresAsync();

        var dto = new SolicitudTurnoDTO
        {
            FechaHora = DateTime.Today.AddDays(1).AddHours(9)
        };

        return View(dto);
    }

    // POST: Turnos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SolicitudTurnoDTO dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _turnoService.SolicitarAsync(dto);
                TempData["Mensaje"] = "Turno solicitado correctamente. Quedó en estado Pendiente.";

                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        await CargarDoctoresAsync();
        return View(dto);
    }

    // GET: Turnos/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var turno = await _turnoService.GetByIdAsync(id);

        if (turno == null)
            return NotFound();

        await CargarDoctoresAsync();
        return View(turno);
    }

    // POST: Turnos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TurnoDTO dto)
    {
        if (id != dto.Id)
            return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                var actualizado = await _turnoService.UpdateAsync(id, dto);

                if (!actualizado)
                    return NotFound();

                TempData["Mensaje"] = "Turno actualizado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        await CargarDoctoresAsync();
        return View(dto);
    }

    // GET: Turnos/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var turno = await _turnoService.GetByIdAsync(id);

        if (turno == null)
            return NotFound();

        return View(turno);
    }

    // POST: Turnos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var eliminado = await _turnoService.DeleteAsync(id);

        if (!eliminado)
            return NotFound();

        TempData["Mensaje"] = "Turno eliminado correctamente.";

        return RedirectToAction(nameof(Index));
    }

    // Carga el desplegable de doctores
    private async Task CargarDoctoresAsync()
    {
        ViewBag.Doctores = new SelectList(await _doctorService.GetAllAsync(), "Id", "NombreCompleto");
    }
}
