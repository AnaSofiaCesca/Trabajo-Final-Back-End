namespace TpFinal.BL.Rules;

public static class Especialidades
{
    public static readonly IReadOnlyList<string> Lista = new[]
    {
        "Cardiología",
        "Clínica Médica",
        "Dermatología",
        "Ginecología",
        "Pediatría",
        "Traumatología"
    };

    public static bool EsValida(string especialidad)
    {
        return Lista.Contains(especialidad);
    }
}
