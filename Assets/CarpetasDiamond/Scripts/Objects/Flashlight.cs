using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light linterna; // Componente de luz de la linterna
    private bool encendida = false; // Estado de la linterna

    void Start()
    {
        linterna.enabled = false; // Asegurarse de que la linterna esté apagada al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Cambiar el estado de la linterna al presionar la tecla F
        {
            encendida = !encendida; // Alternar el estado
            linterna.enabled = encendida; // Encender o apagar la linterna según el estado
        }
    }
}
