using UnityEngine;

public class MusicaFinNivel : MonoBehaviour
{
    [Header("Música")]
    public AudioClip musicaVictoria;
    public AudioClip musicaDerrota;

    private AudioSource audioSource;

    void Start()
    {
        // 1. Buscamos el MusicManager y lo silenciamos para que no haya mezclas
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.GetComponent<AudioSource>().Stop();
        }

        audioSource = GetComponent<AudioSource>();

        DatosJuegoControl datosJuego = null;
        GameObject goDatos = GameObject.Find("DatosJuego");

        if (goDatos != null)
            datosJuego = goDatos.GetComponent<DatosJuegoControl>();

        // 2. Elegimos qué música toca
        if (datosJuego != null && datosJuego.HaGanado)
        {
            audioSource.clip = musicaVictoria;
        }
        else
        {
            audioSource.clip = musicaDerrota;
        }

        // 3. Suena solo esta
        audioSource.Play();
    }
}