using UnityEngine;

/// <summary>
/// De esta clase heredar�n las clases de LogicaBolaNormal y LogicaBolaBlanca
/// </summary>
public abstract class BolaBase : MonoBehaviour
{
    protected Vector3 PosicionInicial;

    // Si la bola est� por debajo su posici�n ser� la inicial
    protected float AlturaMinima = 1.6254f;
}
