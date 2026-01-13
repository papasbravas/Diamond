using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del Menu")]
    [SerializeField] private GameObject panelOpciones; // Panel de opciones
    [SerializeField] private GameObject panelMenuPrincipal; // Panel del menu principal

    [Header("Musica de Fondo")]
    [SerializeField] private AudioSource musicaFondo; // Musica de fondo del menu
    [SerializeField] private float duracionFade = 2f; // Duracion del fade de la musica

    [Header("Configuracion de Audio")]
    [SerializeField] private Slider sliderVolumen; // Slider para controlar el volumen de la musica
    private float volumenActual = 1f; // Volumen actual de la musica

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // Obtener el indice de la escena actual

        if(sceneIndex == 0) // Si estamos en la escena del menu principal
        {
            if(panelMenuPrincipal != null && panelOpciones != null)
            {
                panelMenuPrincipal.SetActive(true); // Activar el panel del menu principal
                panelOpciones.SetActive(false); // Desactivar el panel de opciones
            }

            if(musicaFondo != null) // Si hay musica de fondo asignada
            {
                musicaFondo.volume = volumenActual; // Establecer el volumen inicial de la musica
                musicaFondo.loop = true; // Hacer que la musica se repita
                musicaFondo.Play(); // Reproducir la musica de fondo
            }
        }

        if(sliderVolumen != null) // Si hay un slider de volumen asignado
        {
            volumenActual = PlayerPrefs.GetFloat("VolumenMusica", 1f); // Cargar el volumen guardado en las preferencias del jugador
            sliderVolumen.value = volumenActual; // Establecer el valor del slider al volumen actual
            ActualizarVolumen(volumenActual); // Actualizar el volumen de la musica
            sliderVolumen.onValueChanged.AddListener(ActualizarVolumen); // Agregar un listener para actualizar el volumen cuando el slider cambie
        }
    }
    // Metodo para actualizar el volumen de la musica
    public void ActualizarVolumen(float valor)
    {
        volumenActual = valor;
        AudioListener.volume = volumenActual; // Actualizar el volumen global del audio
        PlayerPrefs.SetFloat("Volumen", volumenActual); // Guardar el volumen en las preferencias del jugador
    }

    public void AbrirOpciones()
    {
        if (panelMenuPrincipal != null) // Si hay un panel del menu principal asignado
        {
            panelMenuPrincipal.SetActive(false); // Desactivar el panel del menu principal 
        }
        panelOpciones.SetActive(true); // Activar el panel de opciones
    }

    public void VolverAlMenu()
    {
        panelMenuPrincipal.SetActive(true); // Activar el panel del menu principal
        panelOpciones.SetActive(false); // Desactivar el panel de opciones
    }

    public void Load()
    {
        StartCoroutine(FadeOutYLoad()); // Iniciar la corrutina para hacer el fade out y cargar la escena del nivel
    }

    private IEnumerator FadeOutYLoad()
    {
        if (musicaFondo != null) // Si hay musica de fondo asignada
        {
            float volumenInicial = musicaFondo.volume; // Guardar el volumen inicial de la musica

            for (float t = 0; t < duracionFade; t += Time.deltaTime) // Hacer un bucle durante la duracion del fade
            {
                musicaFondo.volume = Mathf.Lerp(volumenInicial, 0, t / duracionFade); // Interpolar el volumen de la musica
                yield return null;
            }

            musicaFondo.volume = 0; // Asegurarse de que el volumen sea 0 al final del fade
            musicaFondo.Stop(); // Detener la musica de fondo
        }

        SceneManager.LoadScene("Nivel"); // Cargar la escena del nivel
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");

        // Directiva de preprocesador
        #if UNITY_EDITOR
                // Si estamos en el editor de Unity, usamos el comando para detener el juego.
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                // Si estamos en un ejecutable (Build), cerramos la aplicación.
                                Application.Quit();
        #endif
    }
    public void Cargar() // Cargar la escena del nivel
    {
        SceneManager.LoadScene("Nivel");
    }

    public void VolverAMenuPrincipal() // Volver al menu principal
    {
        SceneManager.LoadScene("MainMenu");
    }

}
