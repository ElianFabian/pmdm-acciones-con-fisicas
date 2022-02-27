using UnityEngine;

public class ControladorJuegoMenu : MonoBehaviour
{
    #region Eventos
    private void Start()
    {
        // Se muesra y desbloquea el cursor del centro
        // (esto es en caso de que venga de la escena del Laberinto)
        Cursor.visible   = true;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion

    #region Métodos
    public void IniciarBillar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Billar");
    }
    public void IniciarBolos()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Bolos");
    }
    public void IniciarLaberinto()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Laberinto");
    }
    public void Salir()
    {
        Application.Quit();
    }
    #endregion
}
