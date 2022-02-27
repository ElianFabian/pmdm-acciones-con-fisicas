using UnityEngine;

public class Jugador : MonoBehaviour
{
    #region Atributos
    [SerializeField] internal Bala Bala;

    #region Constantes
    internal const float MAX_VELOCIDAD_XZ = 10;
    internal const float SUAVIDAD_GIRO    = 20;
    internal const float GRAVEDAD         = -9.81f;
    internal const float MAX_ALTURA       = 2;
    #endregion

    #region Lógica
    internal JugadorEntrada Entrada;
    internal JugadorColision Colision;
    internal JugadorMovimiento Movimiento;
    internal JugadorAccion Accion;
    #endregion

    internal CharacterController controlador;
    internal Camera[] Camaras;
    #endregion

    #region Eventos
    void Awake()
    {
        Entrada    = GetComponent<JugadorEntrada>();
        Movimiento = GetComponent<JugadorMovimiento>();
        Colision   = GetComponent<JugadorColision>();
        Accion     = GetComponent<JugadorAccion>();
        
        controlador = GetComponent<CharacterController>();

        Camaras = FindObjectsOfType<Camera>();
    }
    #endregion

    #region Propiedades
    internal Camera CamaraActual
    {
        get
        {
            foreach (var camara in Camaras)
            {
                if (camara.enabled) return camara;
            }
            return null;
        }
    }
    #endregion
}
