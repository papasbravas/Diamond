using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Instancia singleton del MusicManager

    [Header("Musica")]
    [SerializeField] private AudioSource musicaAmbiente; // Musica de ambiente normal
    [SerializeField] private AudioSource musicaPersecucion; // Musica que suena durante la persecucion
    [SerializeField] private AudioSource musicaMenu; // Musica del menu principal

    [Header("Fade")]
    [SerializeField] private float duracion = 2f; // Duracion del fade entre pistas
    private Coroutine fadeActual; // Referencia al coroutine de fade actual

    private int enemigos = 0; // Contador de enemigos que inician persecucion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicaAmbiente.Play(); // Iniciar la musica de ambiente al inicio
        musicaPersecucion.Stop(); // Asegurarse de que la musica de persecucion este detenida al inicio
    }

    public void InicioPersecucion()
    {
        enemigos ++; // Contar el numero de enemigos que inician persecucion
        if (enemigos == 1) // Solo cambiar la musica si es el primer enemigo
        {
            musicaAmbiente.Stop();
            musicaPersecucion.Play();
        }
    }

    public void FinPersecucion()
    {
        enemigos --; // Disminuir el contador de enemigos en persecucion
        if (enemigos == 0) // Solo cambiar la musica si no hay enemigos en persecucion
        {
            musicaPersecucion.Stop();
            musicaAmbiente.Play();
        }
    }

    public void IniciarFade(AudioSource from, AudioSource to) // Iniciar el fade entre dos pistas de audio
    {
        if (fadeActual != null) // Si ya hay un fade en progreso, detenerlo
            StopCoroutine(fadeActual); // Detener el coroutine de fade actual
        fadeActual = StartCoroutine(FadeCoroutine(from, to)); // Iniciar un nuevo coroutine de fade
    }

    private IEnumerator FadeCoroutine(AudioSource from, AudioSource to) // Coroutine para hacer el fade entre dos pistas de audio
    {
        float tiempo = 0f; // Tiempo
        while (tiempo < duracion) // Mientras no se complete la duracion del fade
        {
            tiempo += Time.deltaTime; // Incrementar el tiempo transcurrido
            float t = tiempo / duracion; // Calcular el progreso del fade

            from.volume = Mathf.Lerp(1f, 0f, t); // Interpolar el volumen de la fuente "from"
            to.volume = Mathf.Lerp(0f, 1f, t); // Interpolar el volumen de la fuente "to"
            yield return null; 
        }

        from.volume = 0f; // Asegurarse de que el volumen de "from" sea 0
        to.volume = 1f; // Asegurarse de que el volumen de "to" sea 1
    }

}
