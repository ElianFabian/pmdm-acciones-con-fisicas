using UnityEngine;

public class JugadorMovimiento : JugadorComponente
{
    #region Atributos
    internal Vector3 Velocidad = new Vector3();
    [SerializeField] internal float VelocidadDeslizamiento = 7;
    internal bool UsarGravedad = true;
    internal float x;
    internal float z;
    #endregion

    #region Eventos
    private void FixedUpdate()
    {
        if (UsarGravedad) AplicarGravedad();
        Mover();
    }
    #endregion

    #region M�todos
    void Mover()
    {
        var velocidadExtra = 0;
        if (jugador.Entrada.EstaClickDerechoPresionadose) velocidadExtra = 5; 

        // Velocidad caminando
        Velocidad.x = Direccion.x * (Jugador.MAX_VELOCIDAD_XZ + velocidadExtra);
        Velocidad.z = Direccion.z * (Jugador.MAX_VELOCIDAD_XZ + velocidadExtra);

        jugador.controlador.Move(Velocidad * Time.deltaTime);

        // Rotaremos s�lo si hay direcci�n de movimiento
        // Se comprueba para no aplicar un vector cero a la rotaci�n
        #region Rotaci�n
        if (EstaMoviendose)
        {
            // El jugador mira en el sentido del movimiento
            var rotacion = Quaternion.LookRotation(Direccion);

            // Suaviza el giro
            jugador.transform.rotation = Quaternion.Slerp
            (
                jugador.transform.rotation,
                rotacion,
                Jugador.SUAVIDAD_GIRO * Time.deltaTime
            );
        }
        #endregion
    }
    void AplicarGravedad()
    {
        Velocidad.y += Jugador.GRAVEDAD * Time.deltaTime;

        // Se usa isGrounded ya que EstaEnSuelo es s�lo para la etiqueta "Suelo"
        if (jugador.controlador.isGrounded && Velocidad.y < 0)
        {
            Velocidad.y = 0;
        }
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el vector de movimiento del jugador respecto de la c�mara.
    /// </summary>
    internal Vector3 Direccion
    {
        get
        {
            var camaraActual = jugador.CamaraActual;

            // Se obtienen los vectores de derecha y delante de la c�mara
            var CamaraDerecha = camaraActual.transform.right;
            var CamaraArriba  = camaraActual.transform.up;      // Se usar� up en lugar de forward si la c�mara mira perpendicularmente al suelo/techo
            var CamaraDelante = camaraActual.transform.forward;

            // Se pone a 0 la componente Y ya que s�lo nos interesa mover al jugador en los ejes XZ
            CamaraDerecha.y = 0;
            CamaraArriba.y  = 0;
            CamaraDelante.y = 0;
            
            CamaraDerecha = CamaraDerecha.normalized;
            CamaraArriba  = CamaraArriba.normalized;
            CamaraDelante = CamaraDelante.normalized;

            var angulo = Vector3.Angle(camaraActual.transform.forward, Vector3.up);

            // Se devuelve el vector correspondiente seg�n la orientaci�n de la c�mara
            return angulo == 0 || angulo == 180
            ?
            (CamaraDerecha*x + CamaraArriba*z).normalized
            :
            (CamaraDerecha*x + CamaraDelante*z).normalized;
        }
        private set { }
    }
    internal bool EstaMoviendose { get => Direccion != Vector3.zero; }
    #endregion
}

/*
 * PROBLEMAS
 * - Si la c�mara est� boca abajo y mira al objetivo presenta problemas de movimiento
 */
