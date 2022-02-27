using UnityEngine;
using UnityEngine.UI;

public class ControladorJuegoBolos : ControladorJuegoBase
{
    #region Atributos
    [SerializeField] Text TxtBolosDerribados;

    LineRenderer    Linea;
    LogicaBolaBolos Bola;

    bool SePuedeLanzar = true; // Sólo se podrá lanzar la primera vez
    #endregion

    #region Eventos
    private void Awake()
    {
        Linea      = FindObjectOfType<LineRenderer>();
        Bola       = FindObjectOfType<LogicaBolaBolos>();
        Bola.Rbody = Bola.GetComponent<Rigidbody>();
    }
    internal override void Update()
    {
        base.Update();

        Ray     ray                 = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direcionLanzamiento = Vector3.zero;

        #region Se lanza un rayo que colisiona contra el objeto al que apunte el cursor
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 bolaPosicion = Bola.transform.position;

            // Se establece la posición del ratón en el tablero y su componente Y será igual al de la bola
            Vector3 ratonPosicion = new Vector3(hit.point.x, bolaPosicion.y, hit.point.z);

            // Se dibuja una línea desde la bola hasta el ratón que servirá de guía para lanzar la bola
            EstablecerPuntosLinea(bolaPosicion, ratonPosicion);

            // Se obtiene el vector de direccón que va desde la bola hasta ratón
            direcionLanzamiento = ObtenerDireccion(bolaPosicion, ratonPosicion);
        }
        #endregion

        #region Se lanza la bola
        if (Input.GetMouseButtonDown(0) && SePuedeLanzar)
        {
            // Se impulsa la bola
            Bola.Impulsar(direcionLanzamiento);

            SePuedeLanzar = false;

            // Se oculta la línea después el primer lanzamiento
            OcultarLinea();
        }
        #endregion

        // Se muestran los bolos derribados
        TxtBolosDerribados.text = $"Bolos derribados: {LogicaBolo.BolosDerribados}";
    }
    #endregion

    #region Métodos
    void EstablecerPuntosLinea(Vector3 pos1, Vector3 pos2)
    {
        Linea.SetPosition(0, pos1);
        Linea.SetPosition(1, pos2);
    }
    void OcultarLinea()
    {
        Linea.gameObject.SetActive(false);
    }
    Vector3 ObtenerDireccion(Vector3 pos1, Vector3 pos2)
    {
        return (pos2 - pos1).normalized;
    }
    #endregion
}
