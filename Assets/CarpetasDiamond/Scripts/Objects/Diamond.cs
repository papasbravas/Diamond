using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // Detectar colisión con el jugador
    {
        if (other.CompareTag("Player")) // Si el objeto que colisiona es el jugador
        {
            CollectDiamond collector = other.GetComponent<CollectDiamond>(); // Obtener el componente CollectDiamond del jugador
            if (collector != null) // Si el componente existe
            {
                collector.AddDiamond(); // Llamar al método AddDiamond para incrementar el contador de diamantes
            }

            Destroy(gameObject); // Destruir el diamante después de ser recogido
        }
    }
}
