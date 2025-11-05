using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Controla toda la lógica de ingresos: agregar, eliminar, obtener totales, etc.
/// </summary>
public class IngresosManager : MonoBehaviour
{
    // Lista de todos los ingresos registrados
    public List<Ingreso> ingresos = new List<Ingreso>();

    // Nombre del archivo donde se guardarán los datos
    private string archivo = "ingresos.json";

    private void Awake()
    {
        // Cargar los datos al iniciar la app
        ingresos = JsonManager.CargarDatos<Ingreso>(archivo);
    }

    /// <summary>
    /// Agrega un nuevo ingreso si no existe otro con el mismo ID.
    /// </summary>
    public void AgregarIngreso(string descripcion, int monto, DateTime fecha, bool recurrente = false, string notas = "")
    {
        string idPrevisto = $"ING_{descripcion.ToUpper().Replace(" ", "_")}_{fecha:yyyy_MM}";
        bool yaExiste = ingresos.Exists(i => i.id == idPrevisto);

        if (yaExiste)
        {
            Debug.LogWarning($"⚠️ Ya existe un ingreso con ID {idPrevisto}, no se agregará duplicado.");
            return;
        }

        Ingreso nuevo = new Ingreso(descripcion, monto, fecha.ToString("yyyy-MM-dd"), recurrente, notas);
        ingresos.Add(nuevo);
        GuardarCambios();
        Debug.Log("✅ Ingreso agregado: " + nuevo.descripcion + " por $" + nuevo.monto);
    }


    /// <summary>
    /// Elimina un ingreso según su ID.
    /// </summary>
    public void EliminarIngreso(string id)
    {
        ingresos.RemoveAll(i => i.id == id);
        GuardarCambios();
        Debug.Log("🗑️ Ingreso eliminado ID: " + id);
    }

    /// <summary>
    /// Suma todos los ingresos registrados hasta ahora.
    /// </summary>
    public int CalcularTotal()
    {
        return ingresos.Sum(i => i.monto);
    }

    /// <summary>
    /// Devuelve los ingresos de un mes específico.
    /// </summary>
    public List<Ingreso> FiltrarPorMes(int año, int mes)
    {
        return ingresos.Where(i =>
        {
            DateTime fecha = DateTime.Parse(i.fecha);
            return fecha.Year == año && fecha.Month == mes;
        }).ToList();
    }

    /// <summary>
    /// Guarda los cambios en el archivo JSON.
    /// </summary>
    public void GuardarCambios()
    {
        JsonManager.GuardarDatos(ingresos, archivo);
    }
}
