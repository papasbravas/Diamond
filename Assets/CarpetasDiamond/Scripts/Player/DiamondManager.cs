using UnityEngine;
using UnityEngine.SceneManagement;

public class DiamondManager : MonoBehaviour
{
    public static DiamondManager Instance;

    private int totalDiamonds;
    private int collectedDiamonds;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalDiamonds = GameObject.FindGameObjectsWithTag("Diamond").Length; // Contar todos los diamantes en la escena
        collectedDiamonds = 0;
    }

    public void AddDiamond() // Método para añadir un diamante al contador
    {
        collectedDiamonds++;

        HUDDiamonds.Instance.UpdateHUD(collectedDiamonds, totalDiamonds); // Actualizar el HUD con el nuevo conteo

        if (collectedDiamonds >= totalDiamonds) // Si se han recogido todos los diamantes
        {
            SceneManager.LoadScene("PantallaVictoria");
        }
    }
}
