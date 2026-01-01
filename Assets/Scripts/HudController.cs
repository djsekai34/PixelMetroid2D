using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoPuntuacion;

    public void SetVidasTexto(int vidas)
    {
        textoVidas.text = "Vidas: " + vidas;
    }

    public void SetTiempoTexto(int tiempo)
    {
        int minutos = tiempo / 60;
        int segundos = tiempo % 60;
        textoTiempo.text = "Tiempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    public void SetPuntuacionTexto(int puntuacion)
    {
        textoPuntuacion.text = "Puntuación: " + puntuacion;
    }
}
