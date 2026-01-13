using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // El jugador
    [SerializeField] private float height = 30f; // Altura de la cámara

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 newPos = target.position;
        newPos.y += height;

        transform.position = newPos;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Vista cenital
    }
}
