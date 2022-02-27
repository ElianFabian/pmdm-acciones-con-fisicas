using UnityEngine;

class LogicaBolo : MonoBehaviour
{
    #region Atributos
    const float AnguloLimite = 45;
    static public byte BolosDerribados;

    bool SeHaDerribado = false;
    #endregion

    #region Eventos
    private void Start()
    {
        BolosDerribados = 0;
    }
    private void Update()
    {
        if (SeHaDerribado || !EstaDerribado) return;

        BolosDerribados++;
        SeHaDerribado = true;
    }
    #endregion

    #region Propiedades
    bool EstaDerribado
    {
        get
        {
            // Comprobamos la inclinación del bolo para saber si está derribado
            return Vector3.Angle(Vector3.up, transform.up) > AnguloLimite;
        }
    }
    #endregion
}
