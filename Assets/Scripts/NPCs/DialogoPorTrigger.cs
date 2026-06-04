using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogoPorTrigger : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset; // Referencia al InputActionAsset para las teclas de interacción

    InputAction actionInteractGround; // Tecla de interaccion del mapa de teclas terrestre
    InputAction actionInteractWater; // Tecla de interaccionn del mapa de teclas acuatico

    [Header("Dialogue Triggers Variables")]
    [SerializeField, TextArea(2, 4 )] private string[] dialogueLines; // Referencia al texto que se mostrrará del character hablando.
    [SerializeField] private Sprite[] portraitsSprites; // Referencia a la imagen portrait que se mostrará del character hablando.
    [SerializeField] private AudioClip[] voices; // Referencia al audio que se reproducirá durante el diálogo, si es necesario y para cada personaje
    private bool isPlayerInDialogueRange; //Bool para saber cuando mostrar la alerta de dialogo
    private bool didDialogueStart = false; // Variable para controlar si el diálogo está activo o no.
    [SerializeField] private int lineIndex = 0; // Índice de la línea de diálogo actual. 
    [SerializeField] private float typingTime = 0.05f; // Cadencia de escritura de los caracteres de la caja de dialogo
    [SerializeField] DialogueController dialogueController; // Referencia al controlador de dialogos para comprobar la variable de dialogos activados/disponibles
    public PlayerControllerWater playerControllerWater; // Referencia al controlador del jugador, si es necesario para otras interacciones.
    public PlayerController_Ground playerControllerGround; // Referencia al controlador del jugador, si es necesario para otras interacciones.
    [SerializeField] private int charsToPlayAudio; // Número de caracteres a escribir antes de reproducir el audio del NPC.
    [SerializeField] private bool isPlayerTalking = false;

    [Space]

    [Header("Dialogue Triggers References")]
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
        actionInteractWater = InputSystem.actions.FindAction("Player_Water/Interact");

        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
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
        isDialogueAvailable();

        portrait.sprite = portraitsSprites[lineIndex];

        if (isPlayerInDialogueRange)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex] && (actionInteractWater.WasPressedThisFrame() || actionInteractGround.WasPressedThisFrame()))
            {
                NextDialogueLine(); // Si el diálogo ya ha comenzado y la línea actual está completa, muestra la siguiente línea.
            }
            else if (actionInteractWater.WasPressedThisFrame() || actionInteractGround.WasPressedThisFrame())
            {
                StopAllCoroutines(); // Si el jugador presiona la tecla de interaccion antes de que termine la línea actual, detiene la corrutina de escritura.
                dialogueText.text = dialogueLines[lineIndex]; // Muestra la línea completa inmediatamente.
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true; //Dialogo empezado
        dialoguePanel.SetActive(true); // Activa el panel de diálogo.
        lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
        StartCoroutine(ShowLine()); // Inicia la corrutina para mostrar la primera línea de diálogo.
    }

    private void NextDialogueLine()
    {
        lineIndex++; // Incrementa el índice de la línea de diálogo actual.
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine()); // Muestra la siguiente línea de diálogo.
        }
        else
        {
            didDialogueStart = false; // Resetea la variable de control del diálogo.
            lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
            this.gameObject.SetActive(false);
        }
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

    private void OnTriggerEnter2D(Collider2D collision) //Si entra el player en la zona pone a correr el dialogo
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInDialogueRange = true;
        }
    }

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
    }
    #endregion
}