using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidad;
    private Vector3 posicionInicio;
    public Vector3 posicionFin;
    private Vector3 posicionAnterior; //Variable auxiliar para almacenar la posición anterior
    private bool movimientoAlDestino;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        posicionInicio = transform.position;
        movimientoAlDestino = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        MoverEnemigo();

    }

    private void MoverEnemigo()
    {
        //Controlar el sentido del movimiento
        //Vector3 posicionDestino = (movimientoAlDestino ? posicionFin : posicionInicio);
        Vector3 posicionDestino;
        if (movimientoAlDestino)
        {
            posicionDestino = posicionFin;
            sprite.flipX = true;
        }
        else
        {
            posicionDestino = posicionInicio;
            sprite.flipX = false;
        }

        //Movimiento del pesonaje
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        //¿Hemos llegado al final?
        if (transform.position == posicionFin)
        {
            movimientoAlDestino = false; //Cambiar el sentido del movimiento
        }
        else if (transform.position == posicionInicio)
        { 
            movimientoAlDestino = true;
        }
    }

    /*-------------------------
               On Collision
     -------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si hacemos contacto con el enemigo (Añado etiqueta jugador) sacamos el mensaje
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Has tenido contacto con el enemigo usando On Collision");
            //Con on collsion si tocas tu al enemigo tambien te detecta la colision y este ultimo no se atraviesa como ocurre con el istrigger
        }
    }*/


    /*-------------------------
               Is trigger
     -------------------------*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // El enemigo NO daña al jugador directamente
            // Se lo dice al GameManager
            GameManager.Instance.QuitarVida(1);
        }
    }


}


/* Para darle la vuelta
 sprite.flipX = (posicionXAnterior < transform.position.x);
       posicionXAnterior = transform.position.x;
 */

/*
 Tareas mejoras:
 1º On collison enter para detectar colisiones con el jugador
 2º On trigger enter para detectar colisiones con ataques del jugador
 3º Is trigger en el collider del enemigo para que no colisione físicamente con el jugador 
 */