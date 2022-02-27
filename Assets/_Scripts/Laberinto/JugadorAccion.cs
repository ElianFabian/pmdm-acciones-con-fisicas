using UnityEngine;

public class JugadorAccion : JugadorComponente
{
    #region Atributos
    [SerializeField][Range(0, 60)] byte TasaDeDisparo = 20;

    bool SaltoInfinito = false;
    float SiguienteVezParaDisparar;
    #endregion

    #region Eventos
    protected override void Start()
    {
        base.Start();

        SiguienteVezParaDisparar = 0;
    }
    private void LateUpdate()
    {
        if (jugador.Entrada.EstaF2Presionado) SaltoInfinito = !SaltoInfinito;

        if (jugador.Entrada.EstaEspacioPresionandose && (jugador.Colision.EstaEnSuelo || SaltoInfinito)) Saltar();
        if (jugador.Entrada.EstaTabPresionado) AlternarCamara();

        // Disparo normal
        if (jugador.Entrada.EstaClickIzquierdoPresionado) Disparar(jugador.Bala);
        // Disparo de ráfaga
        if (jugador.Entrada.EstaClickCentralPresionandose && Time.time >= SiguienteVezParaDisparar)
        {
            SiguienteVezParaDisparar = Time.time + 1/(float)TasaDeDisparo;
            Disparar(jugador.Bala);
        }
    }
    #endregion

    #region Métodos
    void AlternarCamara()
    {
        foreach (var camara in jugador.Camaras)
        {
            camara.enabled = !camara.enabled;
        }
    }
    void Saltar(float altura = Jugador.MAX_ALTURA)
    {
        // Fórmula física que obtiene la velocidad necesaria para saltar a una determinada altura
        jugador.Movimiento.Velocidad.y = Mathf.Sqrt(-2 * Jugador.GRAVEDAD * altura);

        jugador.Colision.EstaEnSuelo = false;
    }
    void Disparar(Bala bala)
    {
        var desfaseAltura = Vector3.up * 0.35f;
        var nuevaBala = Instantiate(bala, transform.position + desfaseAltura, transform.rotation);
        nuevaBala.Disparar();
    }
    #endregion
}
