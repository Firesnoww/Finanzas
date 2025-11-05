using System;

/// <summary>
/// Representa un ingreso registrado por el usuario.
/// Los IDs son personalizados para mayor legibilidad (ej: "ING_SALARIO_2025_11").
/// </summary>
[Serializable]
public class Ingreso
{
    public string id;            // Ejemplo: "ING_SALARIO_2025_11"
    public string descripcion;   // Nombre o motivo (ej. "Salario", "Venta")
    public int monto;            // Valor en pesos
    public string fecha;         // Fecha (texto)
    public bool recurrente;      // Si se repite cada mes
    public string notas;         // Comentario adicional

    /// <summary>
    /// Constructor que crea un ingreso con ID legible.
    /// </summary>
    public Ingreso(string descripcion, int monto, string fecha, bool recurrente = false, string notas = "")
    {
        this.descripcion = descripcion;
        this.monto = monto;
        this.fecha = fecha;
        this.recurrente = recurrente;
        this.notas = notas;

        // Crear ID amigable: ING_NOMBRE_AÑO_MES
        string nombreLimpio = descripcion.ToUpper().Replace(" ", "_");
        DateTime fechaParsed = DateTime.Parse(fecha);
        this.id = $"ING_{nombreLimpio}_{fechaParsed:yyyy_MM}";
    }
}
