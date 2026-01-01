using UnityEngine;

public class EfectoParalax : MonoBehaviour
{
    public float efectoParalax;
    private Transform camaraPrincipal;
    private Vector3 ultimaPosicionCamara;
    void Start()
    {
        camaraPrincipal = Camera.main.transform;
        ultimaPosicionCamara = camaraPrincipal.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Comprobar el movimiento de la cámara
        Vector3 movimientoCamara = camaraPrincipal.position - ultimaPosicionCamara;
        //Añadir paralax al fondo
        transform.position += new Vector3(movimientoCamara.x * efectoParalax, movimientoCamara.y, 0);
        ultimaPosicionCamara = camaraPrincipal.position;
    }
}
