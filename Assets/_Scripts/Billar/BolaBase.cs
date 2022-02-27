using UnityEngine;

/// <summary>
/// De esta clase heredarán las clases de LogicaBolaNormal y LogicaBolaBlanca
/// </summary>
public abstract class BolaBase : MonoBehaviour
{
    protected Vector3 PosicionInicial;

    // Si la bola está por debajo su posición será la inicial
    protected float AlturaMinima = 1.6254f;
}
