using TMPro;
using UnityEngine;

public class HUDDiamonds : MonoBehaviour
{
    public static HUDDiamonds Instance; // Instanciar la clase para acceder a ella desde otros scripts
    [SerializeField] private TextMeshProUGUI puntosText; // Referencia al texto del HUD para mostrar los puntos

    private void Awake() 
    {
        Instance = this; // Asignar la instancia en el método Awake
    }

    public void UpdateHUD(int collectedDiamonds, int totalDiamonds) 
    {
        puntosText.text = collectedDiamonds + " / " + totalDiamonds; // Actualizar el texto del HUD con los diamantes recogidos y el total
    }
}
