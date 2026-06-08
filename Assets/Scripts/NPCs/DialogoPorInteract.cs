using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogoPorInteract : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset; // Referencia al InputActionAsset para las teclas de interacción

    InputAction actionInteractGround; // Tecla de interaccion del mapa de teclas terrestre

    [Header("Dialogue Interact Variables")]
    [SerializeField, TextArea(2, 4 )] private string[] dialogueLines; // Referencia al texto que se mostrrará del character hablando.
    [SerializeField] private Sprite[] portraitsSprites; // Referencia a la imagen portrait que se mostrará del character hablando.
    [SerializeField] private AudioClip[] voices; // Referencia al audio que se reproducirá durante el diálogo, si es necesario y para cada personaje
    private bool isPlayerInDialogueRange; //Bool para saber cuando mostrar la alerta de dialogo
    public bool didDialogueStart = false; // Variable para controlar si el diálogo está activo o no.
    [SerializeField] private int lineIndex = 0; // Índice de la línea de diálogo actual. 
    [SerializeField] private float typingTime = 0.05f; // Cadencia de escritura de los caracteres de la caja de dialogo
    [SerializeField] DialogueController dialogueController; // Referencia al controlador de dialogos para comprobar la variable de dialogos activados/disponibles
    public PlayerController_Ground playerControllerGround; // Referencia al controlador del jugador, si es necesario para otras interacciones.
    [SerializeField] private int charsToPlayAudio; // Número de caracteres a escribir antes de reproducir el audio del NPC.
    [SerializeField] private bool isPlayerTalking = false;
    
    [Space]

    [Header("Dialogue Interact References")]
    [SerializeField] private GameObject dialogueMark; // Referencia al objeto visual que se mostrará al jugador cuando esté en rango.
    [SerializeField] private GameObject dialoguePanel; // Referencia al panel de diálogo que se mostrará al jugador.
    [SerializeField] private TMP_Text dialogueText; // Referencia al cuadro de diálogo que se mostrará al jugador.
    [SerializeField] private AudioSource audioSource; // Referencia al audio que se reproducirá durante el diálogo, si es necesario.
    [SerializeField] private Image portrait; // Referencia a la iamgen portrait que se mostrrará del player hablando.

    #endregion

    #region METHODS
    private void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionInteractGround = InputSystem.actions.FindAction("Player_Ground/Interact");
        playerControllerGround = FindObjectOfType<PlayerController_Ground>();
        dialogueController = FindObjectOfType<DialogueController>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el componente AudioSource del objeto actual.
    }

    // Update is called once per frame. Used to see what the player does each frame.
    void Update()
    {
        //isDialogueAvailable();

        portrait.sprite = portraitsSprites[lineIndex];

        if(isPlayerInDialogueRange && actionInteractGround.WasPressedThisFrame())
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine(); // Si el diálogo ya ha comenzado y la línea actual está completa, muestra la siguiente línea.
            }
            else
            {
                StopAllCoroutines(); // Si el jugador presiona la tecla de interaccion antes de que termine la línea actual, detiene la corrutina de escritura.
                dialogueText.text = dialogueLines[lineIndex]; // Muestra la línea completa inmediatamente.
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true); // Activa el panel de diálogo.
        dialogueMark.SetActive(false); // Desactiva el objeto visual de entrada.
        lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
        StartCoroutine(ShowLine()); // Inicia la corrutina para mostrar la primera línea de diálogo.
        playerControllerGround.enabled = false; // Desactiva el controlador del jugador para evitar movimientos durante el diálogo.
    }

    private void NextDialogueLine()
    {
        lineIndex++; // Incrementa el índice de la línea de diálogo actual.
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine()); // Muestra la siguiente línea de diálogo.
        }
        else //Cuando acaba el dialogo
        {
            didDialogueStart = false; // Resetea la variable de control del diálogo.
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true); // Reactiva el objeto visual de entrada.
            Time.timeScale = 1f; // Reanuda el juego una vez que se han mostrado todas las líneas de diálogo.
            lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
        }   playerControllerGround.enabled = true; // Reactiva el controlador del jugador para que pueda moverse nuevamente.
    }

    private void SelectAudioClip()
    {
        if (lineIndex != 0)
        {
            isPlayerTalking = !isPlayerTalking; // Alterna el estado de si el jugador está hablando o no.
        }

        audioSource.clip = voices[lineIndex]; // Cambia el clip de audio según quién esté hablando.
    }

    private IEnumerator ShowLine()
    {
        SelectAudioClip(); // Selecciona el clip de audio correcto según quién esté hablando.
        dialogueText.text = string.Empty;
        int charIndex = 0; // Índice del carácter actual que se está mostrando.

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch; // Muestra cada carácter uno por uno.

            if(charIndex % charsToPlayAudio == 0) // Reproduce el audio del NPC cada 'charsToPlayAudio' caracteres.
            {
                audioSource.Play(); // Reproduce el clip de audio del NPC.
            }

            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime); // Espera un poco antes de mostrar el siguiente carácter.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInDialogueRange = true;
            dialogueMark.SetActive(true); // Activa el objeto visual para indicar que el jugador está en rango.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInDialogueRange = false;
            dialogueMark.SetActive(false); // Desactiva el objeto visual para indicar que el jugador ya no está en rango.
        }
    }

    /*
    void isDialogueAvailable() //Metodo para comprobar la posibilidad de tener un dialogo 
    {
        if (didDialogueStart)
        {
            dialogueController.isDialogueActive = true;
        }
        else
        {
            dialogueController.isDialogueActive = false;
        }
    }*/
    #endregion
}