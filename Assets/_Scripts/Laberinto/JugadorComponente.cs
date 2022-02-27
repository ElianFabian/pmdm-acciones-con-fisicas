using UnityEngine;

public abstract class JugadorComponente : MonoBehaviour
{
    protected Jugador jugador;

    protected virtual void Start()
    {
        jugador = GetComponent<Jugador>();
    }
}
