using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float damage = 10f; // Daño al jugador
    [SerializeField] private float knockbackForce = 5f; // Fuerza del empuje
    [SerializeField] private float knockbackDuration = 0.5f; // Duración del empuje
    [SerializeField] private float damageCooldown = 0.5f; // tiempo mínimo entre golpes

    private float lastHitTime = -999f; // tiempo del último golpe recibido

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) // Solo afectar al jugador
            return;

        // Evitar daño encadenado si entra varias veces rápido en el trigger
        if (Time.time < lastHitTime + damageCooldown)
            return;

        lastHitTime = Time.time; // Actualizar el tiempo del último golpe

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // Obtener el componente de salud del jugador
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>(); // Obtener el componente de movimiento del jugador

        Debug.Log("Zombie ha tocado al jugador");

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Aplicar daño al jugador
        }

        if (playerMovement != null)
        {
            // Dirección desde el zombie hacia el jugador
            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
            knockbackDirection.y = 0f; // solo en el plano horizontal

            playerMovement.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration);
        }
    }
}
