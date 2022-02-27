using UnityEngine;

public class SeguirObjetivo : MonoBehaviour
{
    #region Atributos
    [SerializeField] Transform objetivo;

    // Distancia entre el jugador y la cámara
    [SerializeField] Vector3 desfase;

    // Modifica la robustez con la que la cámara sigue al objeto
    [SerializeField] float suavidad = .125f;

    [SerializeField] bool mirarAObjetivo = true;

    public bool SiguiendoObjetivo = true;

    Vector3 velocidadObjetivo;
    #endregion

    #region Eventos
    private void Start()
    {
        desfase           = transform.position - objetivo.transform.position;
        velocidadObjetivo = objetivo.GetComponent<Rigidbody>().velocity;
    }

    private void FixedUpdate()
    {
        if (SiguiendoObjetivo)
        {
            Seguir();
        }
    }
    #endregion

    #region Métodos
    void Seguir()
    {
        Vector3 nuevaPosicion = objetivo.position + desfase;

        transform.position = Vector3.SmoothDamp
        (
            transform.position,
            nuevaPosicion,
            ref velocidadObjetivo,
            suavidad
        );

        if (mirarAObjetivo)
        {
            transform.LookAt(objetivo);
        }
    }
    #endregion
}
