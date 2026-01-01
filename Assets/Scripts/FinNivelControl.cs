using UnityEngine;
using TMPro;

public class FinNivelControl : MonoBehaviour
{
    private DatosJuegoControl datosJuego;
    public TextMeshProUGUI mensajeFinalTxt;

    private GameObject rodolfo;   // 👈 imagen de Rodolfo

    void Start()
    {
        datosJuego = GameObject.Find("DatosJuego").GetComponent<DatosJuegoControl>();

        // Buscamos a Rodolfo por TAG
        rodolfo = GameObject.FindGameObjectWithTag("Rodolfo");

        // Mensaje final
        string mensajeFinal = (datosJuego.HaGanado
            ? "¡Qué buena, has ganado! Rodolfo te felicita :)"
            : "Lo siento, has perdido. Vuelve a intentarlo :(");

        // Si ha ganado, mostramos puntuación y a Rodolfo
        if (datosJuego.HaGanado)
        {
            mensajeFinal += " Tu puntuación es: " + datosJuego.Puntuacion + " puntos";
            if (rodolfo != null) rodolfo.SetActive(true);
        }
        else
        {
            // Si pierde, Rodolfo no sale
            if (rodolfo != null) rodolfo.SetActive(false);
        }

        // Asignar texto
        mensajeFinalTxt.text = mensajeFinal;
    }
}
