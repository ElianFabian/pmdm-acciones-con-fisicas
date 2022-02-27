public class LogicaBolaNormal : BolaBase
{
    protected void Update()
    {
        // La bola se desactivar� cuando entre en un agujero (por lo que su altura ser� menor a la m�nima)
        bool esBolaInactiva = transform.position.y < AlturaMinima;

        if (esBolaInactiva) gameObject.SetActive(false);
    }
}
