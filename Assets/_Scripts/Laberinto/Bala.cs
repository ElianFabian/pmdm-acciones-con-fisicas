using UnityEngine;

public class Bala : MonoBehaviour
{
    #region Atributos
    internal AudioSource Sonido;
    const float Velocidad    = 30;
    const float TiempoDeVida = 5;
    const string TagColisionable = "Colisionable";

    bool destruir = false;
    float tiempoDeAudioReproducido = 0;
    #endregion

    #region Eventos
    private void Awake()
    {
        Sonido = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // Se destruye la bala cuando ya se ha reproducido su sonido
        if (destruir) tiempoDeAudioReproducido += Time.deltaTime;
        if (!destruir || tiempoDeAudioReproducido < Sonido.clip.length) return;

        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Se evita que pueda colisionar contra los collider
        // que detectan al jugador para abrir las puertas
        if (!other.CompareTag(TagColisionable)) return;

        // Cuando colisiona se encoge la bala a 0
        // para que antes de destruirla le de tiempo a reproducir su sonido
        transform.localScale = Vector3.zero;

        destruir = true;
    }
    #endregion

    #region Métodos
    public void Disparar()
    {
        Sonido.playOnAwake = true; // Permite que cada bala suene por su cuenta
        Sonido.Play();             // Permite que suene aún estando enfrente de una pared


        transform.LookAt(transform.position + transform.forward);

        transform.position += transform.forward;          // Se posiciona delante del jugador
        transform.rotation *= Quaternion.Euler(90, 0, 0); // Se rota para que la cabeza de la bala mire en sentido del movimiento

        transform.GetComponent<Rigidbody>().AddForce
        (
            transform.up * Velocidad,
            ForceMode.Impulse
        );
        Destroy(gameObject, TiempoDeVida);
    }
    #endregion
}
