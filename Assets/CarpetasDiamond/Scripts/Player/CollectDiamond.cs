using UnityEngine;

public class CollectDiamond : MonoBehaviour
{
    public int diamondsCollected = 0;

    public void AddDiamond() // Método para añadir un diamante al contador
    {
        diamondsCollected++;
        Debug.Log("Diamantes recogidos: " + diamondsCollected);

        DiamondManager.Instance.AddDiamond(); // Llamar al método AddDiamond del DiamondManager para actualizar el contador global
    }
}
