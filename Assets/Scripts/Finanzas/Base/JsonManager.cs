using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Esta clase se encarga de guardar y leer datos en formato JSON
/// para que los datos queden almacenados en el celular o computador.
/// </summary>
public static class JsonManager
{
    /// <summary>
    /// Guarda una lista de datos en un archivo JSON dentro del almacenamiento persistente.
    /// </summary>
    public static void GuardarDatos<T>(List<T> lista, string nombreArchivo)
    {
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        string json = JsonUtility.ToJson(new Wrapper<T>(lista), true);
        File.WriteAllText(ruta, json);
        Debug.Log("✅ Datos guardados en: " + ruta);
    }

    /// <summary>
    /// Carga una lista de datos desde un archivo JSON.
    /// Si no existe el archivo, devuelve una lista vacía.
    /// </summary>
    public static List<T> CargarDatos<T>(string nombreArchivo)
    {
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);

        if (!File.Exists(ruta))
        {
            Debug.LogWarning("⚠️ No existe el archivo, se crea uno nuevo: " + nombreArchivo);
            return new List<T>();
        }

        string json = File.ReadAllText(ruta);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    /// <summary>
    /// Clase interna que sirve como envoltorio (requisito del sistema JSON de Unity).
    /// </summary>
    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> items;
        public Wrapper(List<T> items) { this.items = items; }
    }
}
