using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogoPorTrigger : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    InputAction actionInteract;

    [Header("Dialogue Test Variables")]
    [SerializeField, TextArea(2, 4 )] private string[] dialogueLines; // Referencia al texto que se mostrrará del character hablando.
    [SerializeField] private Sprite[] portraitsSprites; // Referencia a la imagen portrait que se mostrará del character hablando.
    [SerializeField] private AudioClip[] voices; // Referencia al audio que se reproducirá durante el diálogo, si es necesario y para cada personaje
    private bool isPlayerInDialogueRange; //Bool para saber cuando mostrar la alerta de dialogo
    private bool didDialogueStart = false; // Variable para controlar si el diálogo está activo o no.
    [SerializeField] private int lineIndex = 0; // Índice de la línea de diálogo actual. 
    [SerializeField] private float typingTime = 0.05f;
    public PlayerControllerWater playerControllerWater; // Referencia al controlador del jugador, si es necesario para otras interacciones.
    public PlayerController_Ground playerControllerGround; // Referencia al controlador del jugador, si es necesario para otras interacciones.
    [SerializeField] private int charsToPlayAudio; // Número de caracteres a escribir antes de reproducir el audio del NPC.
    [SerializeField] private bool isPlayerTalking = false;
    
    [Space]

    [Header("Dialogue Test References")]
    [SerializeField] private GameObject dialoguePanel; // Referencia al panel de diálogo que se mostrará al jugador.
    [SerializeField] private TMP_Text dialogueText; // Referencia al cuadro de diálogo que se mostrará al jugador.
    [SerializeField] private AudioSource audioSource; // Referencia al audio que se reproducirá durante el diálogo, si es necesario.
    [SerializeField] private Image portrait; // Referencia a la iamgen portrait que se mostrrará del player hablando.

    #endregion

    #region METHODS
    private void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionInteract = InputSystem.actions.FindAction("Interact");
        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        playerControllerGround = FindObjectOfType<PlayerController_Ground>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el componente AudioSource del objeto actual.
    }

    // Update is called once per frame. Used to see what the player does each frame.
    void Update()
    {
        if(isPlayerInDialogueRange)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                if (actionInteract.WasPressedThisFrame())
                {
                    NextDialogueLine(); // Si el diálogo ya ha comenzado y la línea actual está completa, muestra la siguiente línea.
                }
            }
            else
            {
                if (actionInteract.WasPressedThisFrame())
                {
                    StopAllCoroutines(); // Si el jugador presiona F antes de que termine la línea actual, detiene la corrutina de escritura.
                    dialogueText.text = dialogueLines[lineIndex]; // Muestra la línea completa inmediatamente.
                }
            }
        }

        portrait.sprite = portraitsSprites[lineIndex];
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true); // Activa el panel de diálogo.
        lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
        StartCoroutine(ShowLine()); // Inicia la corrutina para mostrar la primera línea de diálogo.
        //Time.timeScale = 0f; // Pausa el juego para que el jugador pueda leer el diálogo sin distracciones.
        playerControllerWater.enabled = false; // Desactiva el controlador del jugador para evitar movimientos durante el diálogo.
        playerControllerGround.enabled = false; // Desactiva el controlador del jugador para evitar movimientos durante el diálogo.
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
            dialoguePanel.SetActive(false);
            //Time.timeScale = 1f; // Reanuda el juego una vez que se han mostrado todas las líneas de diálogo.
            lineIndex = 0; // Reinicia el índice de la línea de diálogo actual.
            playerControllerWater.enabled = true; // Reactiva el controlador del jugador para que pueda moverse nuevamente.
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
        //SelectPortrait(); // Selecciona el portrait correcto según quién esté hablando.
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInDialogueRange = false;
        }
    }
    #endregion
}