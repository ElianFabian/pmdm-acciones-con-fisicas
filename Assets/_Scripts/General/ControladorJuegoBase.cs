using UnityEngine;

public abstract class ControladorJuegoBase : MonoBehaviour
{
    #region Eventos
    internal virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) VolverAMenu();
        if (Input.GetKeyDown(KeyCode.R)) Reiniciar();
    }
    #endregion

    #region Métodos
    public void Reiniciar()
    {
        var escenaActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(escenaActual);
    }
    public void VolverAMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
    }
    #endregion
}
