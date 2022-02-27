using UnityEngine;

public class PuertaDeslizante : MonoBehaviour
{
    #region Atributos
    [SerializeField][Range(0, 10)] float Desfase   = 0;
    [SerializeField][Range(0, 50)] float Velocidad = 5;
    const string TagJugador = "Jugador";

    bool IsTriggerEnter = false;
    bool IsTriggerExit  = false;

    Transform Puerta;

    float AlturaInicial;

    Vector3 PosicionInicial;
    Vector3 PosicionFinal;
    #endregion

    #region Eventos
    private void Start()
    {
        Puerta = transform.GetChild(0);

        AlturaInicial = GetComponentInChildren<MeshRenderer>().bounds.size.y;

        PosicionInicial = Puerta.position;
        PosicionFinal   = Puerta.position - AlturaInicial*Puerta.up;
    }
    private void FixedUpdate()
    {
        if (IsTriggerExit) MoverHasta(PosicionInicial);
        if (IsTriggerEnter) MoverHasta(PosicionFinal + Desfase*Puerta.up);
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
    void MoverHasta(Vector3 nuevaPosicion)
    {
        Puerta.position = Vector3.Lerp
        (
            Puerta.position,
            nuevaPosicion,
            Velocidad * Time.deltaTime
        );
    }
    #endregion
}
