using UnityEngine;

public class PuertaBisagra : MonoBehaviour
{
    #region Atributos
    [Range(-1, 1)]
    [SerializeField] short SentidoDeApertura = 1;

    const string TagJugador = "Jugador";

    Transform Visagra;

    Quaternion RotacionInicial;
    float Velocidad = 4;

    bool IsTriggerEnter = false;
    bool IsTriggerExit  = false;
    #endregion

    #region Eventos
    private void Start()
    {
        RotacionInicial = transform.rotation;
        Visagra = transform.GetChild(0);
    }
    private void FixedUpdate()
    {
        if (IsTriggerEnter) Abrir();
        if (IsTriggerExit) Cerrar(); ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagJugador)) return;

        IsTriggerEnter = true;
        IsTriggerExit  = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(TagJugador)) return;

        IsTriggerEnter = false;
        IsTriggerExit  = true;
    }
    #endregion

    #region Métodos
    void Abrir()
    {
        Visagra.rotation = Quaternion.Slerp
        (
            Visagra.rotation,
            RotacionInicial * Quaternion.Euler(0, -90 * SentidoDeApertura, 0),
            Velocidad * Time.deltaTime
        );
    }
    void Cerrar()
    {
        Visagra.rotation = Quaternion.Slerp
        (
            Visagra.rotation,
            RotacionInicial,
            Velocidad * Time.deltaTime
        );
    }
    #endregion
}
