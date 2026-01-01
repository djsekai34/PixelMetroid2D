using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesControl : MonoBehaviour
{
    public void OnBotonJugar()
    {
        Debug.Log("Botón Jugar pulsado");
        SceneManager.LoadScene("Juego");
    }

    public void OnBotonMenu()
    {
        //escena que se carga al pulsar Menú ( en Créditos)
        SceneManager.LoadScene("Menu Principal");
    }
    public void OnBotonCreditos()
    {
        //escena que se carga al pulsar Créditos en la escena MenuPrincipal
    SceneManager.LoadScene("Creditos");
    }
    public void OnBotonSalir()
    {
        //ejecución para el botón SALIR
        Application.Quit();
    }
}
