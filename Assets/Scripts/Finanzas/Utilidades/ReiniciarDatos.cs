using UnityEngine;
using System.IO;

/// <summary>
/// Permite borrar los archivos de datos JSON (por ejemplo, ingresos, gastos, etc.)
/// para reiniciar la aplicación a un estado limpio.
/// </summary>
public class ReiniciarDatos : MonoBehaviour
{
    [Header("⚠️ Archivos a reiniciar (solo nombres, no rutas)")]
    public string[] archivos = { "ingresos.json", "egresos.json", "ahorros.json", "prestamos.json" };

    /// <summary>
    /// Elimina los archivos de datos especificados. 
    /// Útil durante pruebas o para reiniciar el sistema financiero.
    /// </summary>
    [ContextMenu("Reiniciar Datos Financieros")]
    public void ReiniciarTodo()
    {
        foreach (var nombre in archivos)
        {
            string ruta = Path.Combine(Application.persistentDataPath, nombre);
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
                Debug.Log("🗑️ Archivo eliminado: " + nombre);
            }
            else
            {
                Debug.LogWarning("⚠️ No se encontró: " + nombre);
            }
        }

        Debug.Log("✅ Todos los datos fueron reiniciados correctamente.");
    }
}
