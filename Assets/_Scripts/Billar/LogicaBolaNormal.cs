public class LogicaBolaNormal : BolaBase
{
    protected void Update()
    {
        // La bola se desactivará cuando entre en un agujero (por lo que su altura será menor a la mínima)
        bool esBolaInactiva = transform.position.y < AlturaMinima;

        if (esBolaInactiva) gameObject.SetActive(false);
    }
}
