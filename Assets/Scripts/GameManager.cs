using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Jugador")]
    private Rigidbody2D rbJugador;
    private PlayerController jugador;
    private SpriteRenderer spriteJugador; // Para invulnerabilidad
    private Vector3 posicionInicialJugador;

    [Header("Juego")]
    public int vidas = 3;
    private bool vulnerable = true;
    private bool juegoTerminado = false;
    private bool juegoGanado = false;

    [Header("Tiempo")]
    public float tiempoNivel = 60f;
    private float tiempoInicio;
    private float tiempoEmpleado;

    [Header("Puntuaci�n")]
    private int puntuacion = 0;

    [Header("PowerUps")]
    private int powerUpsRestantes;

    [Header("UI")]
    public Canvas canva;
    private HudController hud;

    private DatosJuegoControl datosJuego;

    [Header("Audio")]
    public AudioClip vidaSfx;
    private AudioSource audioJugador;



    //Generamos solo un game manager y hacemos que lo podamos usar donde deseemos
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Jugador
        GameObject goJugador = GameObject.FindGameObjectWithTag("Player");
        jugador = goJugador.GetComponent<PlayerController>();
        rbJugador = goJugador.GetComponent<Rigidbody2D>();
        spriteJugador = goJugador.GetComponentInChildren<SpriteRenderer>();
        posicionInicialJugador = goJugador.transform.position;

        // HUD
        hud = canva.GetComponentInChildren<HudController>();

        // PowerUps
        powerUpsRestantes = GameObject.FindGameObjectsWithTag("PowerUP").Length;

        // Inicio
        tiempoInicio = Time.time;
        Time.timeScale = 1f;

        ActualizarHUD();

        datosJuego = null;
        GameObject goDatos = GameObject.Find("DatosJuego");
        if (goDatos != null)
        {
            datosJuego = goDatos.GetComponent<DatosJuegoControl>();
        }

        audioJugador = goJugador.GetComponent<AudioSource>();


    }

    void Update()
    {
        if (juegoTerminado || juegoGanado || hud == null)
            return;

        // Tiempo
        tiempoEmpleado = Time.time - tiempoInicio;
        int tiempoRestante = Mathf.Max(0, (int)(tiempoNivel - tiempoEmpleado));
        hud.SetTiempoTexto(tiempoRestante);

        if (tiempoRestante <= 0)
            FinDeJuego();
    }

    // ---------- VIDAS ----------
    public void QuitarVida(int cantidad)
    {
        if (!vulnerable || juegoTerminado) return;

        vulnerable = false;
        vidas--;
        hud.SetVidasTexto(vidas);
        audioJugador.PlayOneShot(vidaSfx);

        // Color rojo al recibir da�o
        if (spriteJugador != null)
            spriteJugador.color = Color.red;

        if (vidas <= 0)
            FinDeJuego();
        else
            Invoke(nameof(HacerVulnerable), 2f);
    }

    private void HacerVulnerable()
    {
        vulnerable = true;

        // Restaurar color original
        if (spriteJugador != null)
            spriteJugador.color = Color.white;
    }

    // ---------- FIN ----------
    private void FinDeJuego()
    {
        if (datosJuego != null)
            datosJuego.HaGanado = false;

        SceneManager.LoadScene("Fin_del_nivel");
    }

    // ---------- GANAR ----------
    void Ganar()
    {
        puntuacion += vidas * 100 + (int)(tiempoNivel - tiempoEmpleado);
        if (datosJuego != null)
        {
            datosJuego.Puntuacion = puntuacion;
            datosJuego.HaGanado = true;
        }
        else
        {
            Debug.LogWarning("Ganar(): datosJuego es null, no se guardó la puntuación/HaGanado");
        }

        SceneManager.LoadScene("Fin_del_nivel");
    }


    // ---------- POWER UPS ----------
    public void PowerUpRecogido()
    {
        powerUpsRestantes--;

        if (powerUpsRestantes <= 0)
        {
            Ganar();
        }
    }

    // ---------- PUNTOS ----------
    public void SumarPuntos(int cantidad)
    {
        puntuacion += cantidad;
        hud.SetPuntuacionTexto(puntuacion);
    }


    // ---------- HUD ----------
    private void ActualizarHUD()
    {
        hud.SetVidasTexto(vidas);
        hud.SetTiempoTexto((int)tiempoNivel);
        hud.SetPuntuacionTexto(puntuacion);
    }
}
