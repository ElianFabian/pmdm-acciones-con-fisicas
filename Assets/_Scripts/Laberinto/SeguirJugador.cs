using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    #region Atributos
    [SerializeField] Transform Objetivo;

    [SerializeField] Vector3 Desfase;            // Distancia entre el jugador y la cámara
    [SerializeField] float Suavidad      = 0.2f; // Modifica la suavidad con la que la cámara sigue al objeto
    [SerializeField] bool MirarAObjetivo = true;
    [SerializeField] bool RotarConRaton  = true;
    [SerializeField] bool AjustarAltura  = true;
    [SerializeField][Range(0, 20)] float VelocidadAjustarAltura = 5;

    public bool SeguirObjetivo = true;

    Jugador jugador;

    float DesfaseMagnitudInicial;
    float DesfaseYInicial;

    Vector3 VelocidadObjetivo;

    readonly Vector2 VelocidadRaton = new Vector2(5, 2);
    #endregion

    #region Eventos
    private void Start()
    {
        Desfase                = transform.position - Objetivo.position;
        DesfaseMagnitudInicial = Desfase.magnitude;
        DesfaseYInicial        = transform.position.y - Objetivo.position.y;
        jugador                = Objetivo.GetComponent<Jugador>();
        VelocidadObjetivo      = jugador.Movimiento.Velocidad;
    }

    private void LateUpdate()
    {
        if (RotarConRaton) MoverCamaraConRaton();
    }

    private void FixedUpdate()
    {
        if (AjustarAltura) AjustarAlturaRelativa();
        if (SeguirObjetivo) Seguir();
    }
    #endregion

    #region Métodos
    void Seguir()
    {
        var nuevaPosicion = Objetivo.position + Desfase;

        transform.position = Vector3.SmoothDamp
        (
            transform.position,
            nuevaPosicion,
            ref VelocidadObjetivo,
            Suavidad
        );
    }

    void MoverCamaraConRaton()
    {
        var ratonX = Input.GetAxis("Mouse X");
        //var ratonY = Input.GetAxis("Mouse Y");

        transform.RotateAround(Objetivo.position, Vector2.up, ratonX * VelocidadRaton.x);
        //transform.RotateAround(Objetivo.position, Vector2.right, ratonY * VelocidadRaton.y);

        #region Limitar movimiento de la cámara (no funciona)
        //var rotacion = transform.rotation.eulerAngles;

        //if (0 <= rotacion.x && rotacion.x <= 50)
        //{
        //  transform.RotateAround(Objetivo.position, Vector2.right, ratonY * VelocidadRaton.y);
        //}

        //rotacion.x = Mathf.Clamp(rotacion.x, 0, 50);

        //transform.rotation = Quaternion.Euler(rotacion);
        #endregion

        // Si el jugador se mueve se cambia el desfase para que la cámara no esté siempre
        // en la misma posición relativa
        if (!jugador.Movimiento.EstaMoviendose || jugador.Movimiento.EstaMoviendose && Mathf.Abs(ratonX) > 0)
        {
            Desfase = transform.position - Objetivo.position;
            Desfase = Desfase.normalized * DesfaseMagnitudInicial;
        }
    }

    /// <summary>
    /// La altura relativa de la cámara respecto del jugador siempre será la misma.
    /// </summary>
    void AjustarAlturaRelativa()
    {
        transform.position = Vector3.Lerp
        (
            transform.position,
            new Vector3
            (
                transform.position.x,
                Objetivo.position.y + DesfaseYInicial,
                transform.position.z
            ),
            VelocidadAjustarAltura * Time.deltaTime
        );
    }
    #endregion
}
