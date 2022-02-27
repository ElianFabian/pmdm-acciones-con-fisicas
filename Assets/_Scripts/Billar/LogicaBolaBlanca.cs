using UnityEngine;

public class LogicaBolaBlanca : BolaBase
{
    #region Atributos
    [HideInInspector] public Rigidbody Rbody;

    const float VelocidadLanzamiento = 10;

    public const float VelocidadMinima = 0.6f;

    internal Quaternion RotacionInicial;
    #endregion

    #region Eventos
    private void Start()
    {
        PosicionInicial = transform.position;
        RotacionInicial = transform.rotation;
    }
    private void Update()
    {
        // La bola se resetea cuando su posici�n est� por debajo de la m�nima o cuando se presiona el bot�n 
        bool resetearBola = transform.position.y < AlturaMinima;

        if (resetearBola) Resetear();
    }
    #endregion

    #region M�todos
    public void Impulsar(Vector3 direccion)
    {
        Rbody.velocity = direccion * VelocidadLanzamiento;
    }
    public virtual void Resetear()
    {
        transform.SetPositionAndRotation
        (
            PosicionInicial, RotacionInicial
        );
        
        Rbody.velocity        =
        Rbody.angularVelocity = Vector3.zero;
    }
    #endregion
}
