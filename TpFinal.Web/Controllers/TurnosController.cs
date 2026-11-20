using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TpFinal.BL.DTOs;
using TpFinal.BL.Exceptions;
using TpFinal.BL.Interfaces;
using TpFinal.BL.Rules;

namespace TpFinal.Web.Controllers;

public class TurnosController : Controller
{
    private static readonly CultureInfo CulturaAr = new("es-AR");

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
        await CargarDatosFormularioAsync();

        var dto = new SolicitudTurnoDTO
        {
            Fecha = HorarioAtencion.ProximoDiaHabil().ToString(HorarioAtencion.FormatoFecha)
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

        await CargarDatosFormularioAsync();
        return View(dto);
    }

    // GET: Turnos/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var turno = await _turnoService.GetByIdAsync(id);

        if (turno == null)
            return NotFound();

        await CargarDatosFormularioAsync(turno.Fecha);
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

        await CargarDatosFormularioAsync(dto.Fecha);
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

    // Carga los desplegables de doctores, días hábiles y horarios
    private async Task CargarDatosFormularioAsync(string? fechaActual = null)
    {
        ViewBag.Doctores = new SelectList(await _doctorService.GetAllAsync(), "Id", "NombreCompleto");
        ViewBag.Horarios = new SelectList(HorarioAtencion.ObtenerHorarios());

        var dias = HorarioAtencion.ObtenerDiasHabiles()
            .Select(d => new SelectListItem(TextoDia(d), d.ToString(HorarioAtencion.FormatoFecha)))
            .ToList();

        // Al editar, si el turno tiene un día fuera de la agenda (por ejemplo, uno pasado), se agrega para no perderlo
        if (fechaActual != null
            && dias.All(d => d.Value != fechaActual)
            && DateTime.TryParseExact(fechaActual, HorarioAtencion.FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
        {
            dias.Insert(0, new SelectListItem(TextoDia(fecha), fechaActual));
        }

        ViewBag.Dias = dias;
    }

    // Ejemplo: "Lunes 23/11/2026"
    private static string TextoDia(DateTime fecha)
    {
        return CulturaAr.TextInfo.ToTitleCase(fecha.ToString("dddd dd/MM/yyyy", CulturaAr));
    }
}
