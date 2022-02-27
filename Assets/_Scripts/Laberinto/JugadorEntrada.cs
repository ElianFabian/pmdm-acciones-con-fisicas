using UnityEngine;

public class JugadorEntrada : JugadorComponente
{
    #region Atributos
    internal bool EstaEspacioPresionandose;
    internal bool EstaClickIzquierdoPresionado;
    internal bool EstaClickCentralPresionandose;
    internal bool EstaClickDerechoPresionadose;
    internal bool EstaTabPresionado;
    internal bool EstaF2Presionado;
    #endregion

    #region Eventos
    private void Update()
    {
        jugador.Movimiento.x = Input.GetAxisRaw("Horizontal");
        jugador.Movimiento.z = Input.GetAxisRaw("Vertical");

        EstaClickIzquierdoPresionado  = Input.GetMouseButtonDown(0);
        EstaClickDerechoPresionadose  = Input.GetMouseButton(1);
        EstaClickCentralPresionandose = Input.GetMouseButton(2);
        EstaEspacioPresionandose      = Input.GetButton("Jump");
        EstaTabPresionado             = Input.GetKeyDown(KeyCode.Tab);
        EstaF2Presionado              = Input.GetKeyDown(KeyCode.F2);
    }
    #endregion
}
