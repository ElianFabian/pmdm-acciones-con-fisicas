using UnityEngine;

public class Fruta : MonoBehaviour
{
    #region Atributos
    [Range(0, 10)][SerializeField] float Amplitud           = 1.5f;
    [Range(0, 10)][SerializeField] float VelocidadFlotacion = 2.5f;
    [Range(0, 10)][SerializeField] float VelocidadRotacion  = 0.5f;
    [SerializeField] AudioClip Sonido;

    const float VELOCIDAD_ENCOGER = 450;
    const float TAMANIO_MINIMO    = 0.01f;
    static public int FrutaTotal = 0;

    float AlturaInicial;
    float AnguloFlotacion = 0;

    const string TagJugador = "Jugador";

    bool Eliminar = false;
    bool Flotando = true;
    #endregion

    #region Eventos
    private void Start()
    {
        AlturaInicial = transform.position.y;
    }
    private void Update()
    {
        if (Flotando) Flotar(Amplitud);
        if (Eliminar) Desaparecer();

        Rotar();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagJugador)) return;
        
        Eliminar = true;

        AudioSource.PlayClipAtPoint(Sonido, transform.position);
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Eleva la fruta desde su posición inicial hasta la altura indicada (Amplitud).
    /// </summary>
    void Flotar(float amplitud = 1)
    {
        transform.position = new Vector3
        (
            transform.position.x,
            // posI + A/2*sin(angulo) + A/2
            AlturaInicial + amplitud/2*(Mathf.Sin(AnguloFlotacion += VelocidadFlotacion * Time.deltaTime) + 1),
            transform.position.z
        );
    }
    void Rotar()
    {
        transform.Rotate(0, VelocidadRotacion, 0);
    }
    void Desaparecer()
    {
        Flotando = false;

        transform.localScale -= VELOCIDAD_ENCOGER * Time.deltaTime * Vector3.one;

        if (transform.localScale.x < TAMANIO_MINIMO)
        {
            ControladorJuegoLaberinto.FrutaConseguida++;
            Destroy(gameObject);
        }
    }
    #endregion
}
