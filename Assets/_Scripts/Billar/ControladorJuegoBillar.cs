using UnityEngine;
using UnityEngine.UI;

public class ControladorJuegoBillar : ControladorJuegoBase
{
    #region Atributos    
    [SerializeField] Text TxtLanzamientos;

    LineRenderer Linea;

    LogicaBolaBlanca BolaBlanca;

    float nLanzamientos = 0;

    static public System.Action EnReseteo;
    #endregion

    #region Eventos
    private void Awake()
    {
        Linea            = FindObjectOfType<LineRenderer>();
        BolaBlanca       = FindObjectOfType<LogicaBolaBlanca>();
        BolaBlanca.Rbody = BolaBlanca.GetComponent<Rigidbody>();
    }
    internal override void Update()
    {
        base.Update();

        Ray     ray                  = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direccionLanzamiento = Vector3.zero;

        #region Se lanza un rayo que colisiona contra el objeto al que apunte el cursor
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 bolaBlancaPosicion = BolaBlanca.transform.position;

            // Se establece la posición del ratón en el tablero y su componente Y será igual al de la bola
            Vector3 ratonPosicion = new Vector3(hit.point.x, bolaBlancaPosicion.y, hit.point.z);

            // Se dibuja una línea desde la bola blanca hasta el ratón que servirá de guía para lanzar la bola
            EstablecerLineaPuntos(bolaBlancaPosicion, ratonPosicion);

            // Se obtiene el vector de direccón que va desde la bola hasta ratón
            direccionLanzamiento = ObtenerDireccion(bolaBlancaPosicion, ratonPosicion);
        }
        #endregion

        // Se podrá lanzar si la velocidad es menor a la indicada
        bool sePuedeLanzar = BolaBlanca.Rbody.velocity.magnitude < LogicaBolaBlanca.VelocidadMinima;

        #region Se lanza la bola en caso de poderse
        if (Input.GetMouseButtonDown(0) && sePuedeLanzar)
        {
            // Se incrementa y muestra los lanzamientos
            nLanzamientos++;
            TxtLanzamientos.text = $"Lanzamientos: {nLanzamientos}";

            // Se impulsa la bola blanca
            BolaBlanca.Impulsar(direccionLanzamiento);
        }
        #endregion

        // Se mostrará la línea cuando se pueda lanzar
        Linea.gameObject.SetActive(sePuedeLanzar); // Podemos usar la variable directamente en lugar de usar if else
    }
    #endregion

    #region Métodos
    void EstablecerLineaPuntos(Vector3 pos1, Vector3 pos2)
    {
        Linea.SetPosition(0, pos1);
        Linea.SetPosition(1, pos2);
    }
    Vector3 ObtenerDireccion(Vector3 pos1, Vector3 pos2)
    {
        return (pos2 - pos1).normalized;
    }
    #endregion
}
