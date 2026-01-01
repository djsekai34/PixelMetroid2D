using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    private Rigidbody2D fisicasJugador;
    private SpriteRenderer spriteJugador;
    private Animator animacion;

    private float inputHorizontal;
    private DatosJuegoControl datosJuego;

    private AudioSource audioSource;
    public AudioClip saltoSfx;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        fisicasJugador = GetComponent<Rigidbody2D>();
        spriteJugador = GetComponentInChildren<SpriteRenderer>();
        animacion = GetComponentInChildren<Animator>();

        // Intentamos encontrar DatosJuego por nombre
        GameObject goDatos = GameObject.Find("DatosJuego");
        if (goDatos != null)
        {
            datosJuego = goDatos.GetComponent<DatosJuegoControl>();
        }
        else
        {
            // Fallback: intentamos buscar cualquier componente DatosJuegoControl
            datosJuego = FindObjectOfType<DatosJuegoControl>();
            if (datosJuego == null)
            {
                Debug.LogWarning("DatosJuego no encontrado. Crea un GameObject llamado 'DatosJuego' en Nivel1 con el script DatosJuegoControl.");
            }
        }
    }


    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");

        // Movimiento horizontal
        fisicasJugador.linearVelocity = new Vector2(inputHorizontal * velocidad, fisicasJugador.linearVelocity.y);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {
            fisicasJugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        // Girar sprite
        if (fisicasJugador.linearVelocity.x < 0) spriteJugador.flipX = true;
        else if (fisicasJugador.linearVelocity.x > 0) spriteJugador.flipX = false;

        // Animaciones
        AnimarJugador();
    }

    private bool TocandoSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        return hit.collider != null;
    }

    private void AnimarJugador()
    {
        if (fisicasJugador.linearVelocity.y == 0 && fisicasJugador.linearVelocity.x == 0)
        {
            animacion.Play("Jugador_parado");
        }
        else if (!TocandoSuelo() || Input.GetKeyDown(KeyCode.Space))
        {
            animacion.Play("Jugador_saltando");
            audioSource.PlayOneShot(saltoSfx);
        }
        else if ((fisicasJugador.linearVelocity.x < -1 || fisicasJugador.linearVelocity.x > 1) && fisicasJugador.linearVelocity.y == 0)
        {
            animacion.Play("Jugador_corriendo");
        }
    }
}
