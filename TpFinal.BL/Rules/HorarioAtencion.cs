namespace TpFinal.BL.Rules;

public static class HorarioAtencion
{
    public const int HoraApertura = 8;
    public const int HoraCierre = 20;
    public const int IntervaloMinutos = 30;
    public const int DiasAgenda = 30;
    public const string FormatoFecha = "yyyy-MM-dd";

    public static bool EsDiaHabil(DateTime fecha)
    {
        return fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday;
    }

    // Días hábiles desde hoy hasta el final de la agenda
    public static IEnumerable<DateTime> ObtenerDiasHabiles()
    {
        var hasta = DateTime.Today.AddDays(DiasAgenda);

        for (var fecha = DateTime.Today; fecha <= hasta; fecha = fecha.AddDays(1))
        {
            if (EsDiaHabil(fecha))
                yield return fecha;
        }
    }

    // Horarios de atención: 08:00, 08:30 ... 19:30
    public static IEnumerable<string> ObtenerHorarios()
    {
        var hora = new TimeSpan(HoraApertura, 0, 0);
        var cierre = new TimeSpan(HoraCierre, 0, 0);

        while (hora < cierre)
        {
            yield return hora.ToString(@"hh\:mm");
            hora = hora.Add(TimeSpan.FromMinutes(IntervaloMinutos));
        }
    }

    public static bool EsHorarioValido(string? hora)
    {
        return hora != null && ObtenerHorarios().Contains(hora);
    }

    public static DateTime ProximoDiaHabil()
    {
        var fecha = DateTime.Today.AddDays(1);

        while (!EsDiaHabil(fecha))
            fecha = fecha.AddDays(1);

        return fecha;
    }
}
