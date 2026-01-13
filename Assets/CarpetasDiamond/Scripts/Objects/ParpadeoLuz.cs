using UnityEngine;

public class ParpadeoLuz : MonoBehaviour
{
    private Light fuenteLuz; // Componente de luz
    private float intensidadBase; // Intensidad base de la luz

    [Header("Ajustes de parpadeo")] // Encabezado para los ajustes de parpadeo
    public float velocidad = 10f; // Velocidad del parpadeo
    public float cantidad = 0.2f; // Cantidad máxima de variación en la intensidad

    void Start()
    {
        fuenteLuz = GetComponent<Light>(); // Obtener el componente de luz
        intensidadBase = fuenteLuz.intensity; // Guardar la intensidad base de la luz
    }

    void Update()
    {
        float noise = Mathf.Sin(Time.time * velocidad) * cantidad; // Calcular el ruido usando una función seno para un parpadeo suave
        fuenteLuz.intensity = intensidadBase + noise; // Ajustar la intensidad de la luz con el ruido calculado
    }
}
