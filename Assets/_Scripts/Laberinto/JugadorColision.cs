using UnityEngine;

public class JugadorColision : JugadorComponente
{
    #region Atributos
    internal bool EstaEnSuelo;

    const string TagSuelo = "Suelo";
    #endregion

    #region Eventos
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.collider.CompareTag(TagSuelo)) return;

        EstaEnSuelo = true;
    }
    #endregion
}
