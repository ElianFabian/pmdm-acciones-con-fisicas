using UnityEngine;

class LogicaBolaBolos : MonoBehaviour
{
    #region Atributos
    [HideInInspector]
    public Rigidbody Rbody;

    Camera Camara;
    SeguirObjetivo CamaraSeguirObjetivo;

    const float VelocidadLanzamiento = 50;

    readonly Vector3 CamaraPosicionAlGolpear = new Vector3(-35.9067535f, 15.2652054f, 0.0137868524f);
    readonly Quaternion CamaraRotacionAlGolpear = Quaternion.Euler(new Vector3(47.6128616f, 269.58783f, 0.000894127472f));
    #endregion

    #region Eventos
    private void Awake()
    {
        Camara = Camera.main;
        CamaraSeguirObjetivo = Camara.GetComponent<SeguirObjetivo>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Sólo comprobamos las colisiones contra bolos
        if (collision.collider.GetComponent<LogicaBolo>() == null) return;

        // Al golpear la cámara dejará de seguir a la bola
        CamaraSeguirObjetivo.SiguiendoObjetivo = false;

        // Establecemos la nueva posición y rotación de la cámara para ver bien los bolos
        Camara.transform.SetPositionAndRotation
        (
            CamaraPosicionAlGolpear,
            CamaraRotacionAlGolpear
        );
    }
    #endregion

    #region Métodos
    public void Impulsar(Vector3 direccion)
    {
        Rbody.velocity = direccion * VelocidadLanzamiento;
    }
    #endregion
}
