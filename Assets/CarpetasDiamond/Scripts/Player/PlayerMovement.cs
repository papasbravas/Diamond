using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;    // Velocidad de movimiento

    private CharacterController characterController; // Componente CharacterController
    [SerializeField] private Vector2 moveInput; // Entrada de movimiento
     
    [SerializeField] private AudioSource pasos; // Sonido de pasos
    [SerializeField] private int minSpeedSound = 1; // Velocidad mínima para reproducir sonido de pasos

    private Vector3 knockbackVelocity; // Velocidad de empuje
    private float knockbackTimeRemaining = 0f; // Tiempo restante del empuje
    private bool isKnockbackActive => knockbackTimeRemaining > 0f;  // Comprobar si el empuje está activo

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Obtener el componente CharacterController
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (characterController == null)
            return;

        ControlMovimiento();
        SonidoPasos();
    }

    private void ControlMovimiento() // Controlar movimiento del jugador
    {
        Vector3 finalVelocity = Vector3.zero;

        // Aplicar knockback si está activo
        if (isKnockbackActive)
        {
            knockbackTimeRemaining -= Time.deltaTime; // Reducir el tiempo restante del knockback
            finalVelocity += knockbackVelocity;
        }
        else
        {
            // Movimiento local XZ
            Vector3 localMove = new Vector3(moveInput.x, 0, moveInput.y);
            Vector3 worldMove = transform.TransformDirection(localMove);

            if (worldMove.sqrMagnitude > 1f) // Normalizar para evitar velocidad mayor a la deseada
                worldMove.Normalize();

            finalVelocity += worldMove * moveSpeed; // Velocidad horizontal
        }

        characterController.Move(finalVelocity * Time.deltaTime);
    }

    private void SonidoPasos()
    {
        if (pasos == null)
            return;

        Vector3 v = characterController.velocity;
        v.y = 0;

        bool andando = characterController.isGrounded && v.magnitude > minSpeedSound; // Comprobar si el jugador está andando

        if (andando)
        {
            if (!pasos.isPlaying)
                pasos.Play();
        }
        else
        {
            if (pasos.isPlaying)
                pasos.Stop();
        }
    }

    public void ApplyKnockback(Vector3 knockbackDirection, float knockbackForce, float knockbackDuration)
    {
        // Guardamos la velocidad de empuje
        knockbackVelocity = knockbackDirection.normalized * knockbackForce;

        // Tiempo que durará el empuje
        knockbackTimeRemaining = knockbackDuration;
    }
}

