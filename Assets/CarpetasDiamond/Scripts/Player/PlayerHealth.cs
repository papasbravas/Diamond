using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de Salud")] // Encabezado para los ajustes de salud
    [SerializeField] private float maxHealth = 100f; // Salud máxima del jugador
    private float currentHealth; // Salud actual del jugador

    [SerializeField] private UnityEngine.UI.Image OverlayDaño;

    void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud actual al máximo
        HUDHealth.Instance.UpdateHealth(currentHealth, maxHealth); // Actualizar el HUD al iniciar
    }

    public void TakeDamage(float damage) // Método para recibir daño
    {
        currentHealth -= damage; // Restar el daño a la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurar que la salud esté entre 0 y la máxima

        Debug.Log("Vida actual: " + currentHealth);

        HUDHealth.Instance.UpdateHealth(currentHealth, maxHealth); // Actualizar el HUD con la nueva salud
        
        float healthPercent = currentHealth / maxHealth; // Calcular el porcentaje de salud
        float alpha = Mathf.Lerp(0f, 0.6f, 1f - healthPercent); // Calcular la opacidad del overlay basado en la salud restante

        if (OverlayDaño != null) // Si el overlay de daño está asignado
        {
            Color c = OverlayDaño.color; // Obtener el color actual del overlay
            c.a = alpha; // Ajustar la opacidad
            OverlayDaño.color = c; // Aplicar el nuevo color al overlay
        }


        if (currentHealth <= 0) // Si la salud llega a 0 o menos
        {
            SceneManager.LoadScene("MainMenu"); // Recargar la escena del menú
        }
    }
}
