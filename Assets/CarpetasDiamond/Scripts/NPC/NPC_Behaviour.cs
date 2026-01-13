using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour
{
    [Header("Patrulla")]
    [SerializeField] private Transform camino; // Contenedor de waypoints
    [SerializeField] private float distanciaWaypoints = 1f; // Distancia para considerar que se llegó al waypoint
    private int currentIndex = 0; // Índice del waypoint actual

    [Header("Persecución")]
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private float distanciaDetectado = 8f; // Distancia para detectar al jugador
    [SerializeField] private float distanciaPerder = 12f; // Distancia para perder al jugador

    [Header("Velocidades")]
    [SerializeField] private float andar = 2f; // Velocidad al patrullar
    [SerializeField] private float correr = 4f; // Velocidad al perseguir

    [Header("Animaciones")]
    [SerializeField] private Animator animator; // Componente Animator del NPC
    [SerializeField] private string animacionAndar = "Z_Walk1_InPlace"; // Nombre de la animación de caminar
    [SerializeField] private string animacionCorrer = "Z_Run_InPlace"; // Nombre de la animación de correr

    [Header("Minimapa")]
    [SerializeField] private GameObject minimapIcon; // Icono del NPC en el minimapa

    private NavMeshAgent agent;
    private bool chasing = false;

    // Inicialización
    void Start()
    {
        // Obtener componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Validar path
        if (camino == null || camino.childCount == 0)
        {
            Debug.LogError("El NPC no tiene un Path asignado o está vacío.");
            enabled = false;
            return;
        }

        agent.speed = andar;
        animator.Play(animacionAndar);

        // Al iniciar, el icono del minimapa está oculto
        if (minimapIcon != null)
            minimapIcon.SetActive(false);

        GoToNextWaypoint();
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        // Detectar jugador
        if (!chasing && distToPlayer < distanciaDetectado)
        {
            chasing = true;
            agent.speed = correr;
            animator.Play(animacionCorrer);

            if (minimapIcon != null)
                minimapIcon.SetActive(true); // Aparece en el minimapa
            Debug.Log(name + " | Chasing: " + chasing + " | Distancia: " + distToPlayer);
            MusicManager.Instance.InicioPersecucion();
        }

        // Perder jugador
        if (chasing && distToPlayer > distanciaPerder)
        {
            chasing = false;
            agent.speed = andar;
            animator.Play(animacionAndar);
            GoToNextWaypoint();

            if (minimapIcon != null)
                minimapIcon.SetActive(false); // Desaparece del minimapa

            MusicManager.Instance.FinPersecucion();
        }

        // Movimiento
        if (chasing)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            PatrolBehaviour();
        }
    }

    // Comportamiento de patrulla
    private void PatrolBehaviour()
    {
        if (!agent.pathPending && agent.remainingDistance < distanciaWaypoints)
        {
            currentIndex = (currentIndex + 1) % camino.childCount;
            GoToNextWaypoint();
        }
    }

    // Ir al siguiente waypoint
    private void GoToNextWaypoint()
    {
        agent.SetDestination(camino.GetChild(currentIndex).position);
    }
}
