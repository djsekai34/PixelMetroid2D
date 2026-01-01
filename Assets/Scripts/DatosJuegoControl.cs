using UnityEngine;

public class DatosJuegoControl : MonoBehaviour
{
    private int puntuacion;
    private bool haGanado;
    public int Puntuacion { get => puntuacion; set => puntuacion = value; }
    public bool HaGanado { get => haGanado; set => haGanado = value; }
    private void Awake()
    {
        //¿cuántas instancias hay?
        int numeroInstancias = FindObjectsByType<DatosJuegoControl>(FindObjectsSortMode.None).Length;
        //si hay más de una, es por que se ha recargado la escena, destrimos la nueva instancia
        if (numeroInstancias != 1) Destroy(this.gameObject);
        //this representa la instancia de DatosJuegosControl que se está ejecutando
        else DontDestroyOnLoad(this.gameObject);
    }
}