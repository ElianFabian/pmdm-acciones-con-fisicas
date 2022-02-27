using UnityEngine;
using UnityEngine.UI;

public class ControladorJuegoLaberinto : ControladorJuegoBase
{
    #region Atributos
    [SerializeField] GameObject PantallaNormal;
    [SerializeField] GameObject PantallaFinal;
    [SerializeField] Transform Frutas;

    public Text TxtFrutaConseguida;
    public static int FrutaConseguida;

    bool EstaFrutaConseguida;
    #endregion

    #region Eventos
    internal void Start()
    {
        // Se hace esto ya que al finalizar el nivel y volver a hacerlo
        // la pantalla final permanece por alguna razón
        PantallaFinal.SetActive(false);
        PantallaNormal.SetActive(true);

        Fruta.FrutaTotal = Frutas.childCount;

        // La inicialización de atributos al declararlos da problemas para resetear sus valores al reiniciar el nivel
        FrutaConseguida     = 0;
        EstaFrutaConseguida = false;

        // Se oculta y bloquea el cursor en el centro
        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    internal override void Update()
    {
        base.Update();

        TxtFrutaConseguida.text = $"Fruta conseguida: {FrutaConseguida}/{Fruta.FrutaTotal}";

        if (FrutaConseguida == Fruta.FrutaTotal && !EstaFrutaConseguida)
        {
            MostrarPantallaFinal();
            EstaFrutaConseguida = true;
        }
    }
    #endregion

    #region Métodos
    void MostrarPantallaFinal()
    {
        PantallaFinal.SetActive(true);
        PantallaNormal.SetActive(false);

        // Se restablece el la configuración del cursor
        Cursor.visible   = true;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion
}
