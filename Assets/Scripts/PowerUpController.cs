using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public int puntos = 100;
    public float aumentoVelocidad = 2f;
    
    [Header("Audio")]
    public AudioClip powerUPSfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AudioSource>().PlayOneShot(powerUPSfx);
            GameManager.Instance.SumarPuntos(puntos);
            GameManager.Instance.PowerUpRecogido();

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.velocidad += aumentoVelocidad;
            }

            Destroy(gameObject);
        }
    }
}
