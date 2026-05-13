using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Submarine : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    InputAction actionInteract;

    [Header("Variables Generales")]
    [SerializeField] GameObject canvasFades;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] AnimationClip animacionFinal;
    [SerializeField] GameObject submarineMark;

    [SerializeField] GameObject buttonZone2;
    [SerializeField] GameObject buttonZone3;
    [SerializeField] GameObject buttonZone4;
    [SerializeField] GameObject buttonZone5;

    [SerializeField] GameObject playerWater;
    [SerializeField] GameObject playerGround;

    int sceneToTPSubmarine;
    public string sceneToTPCode;
    bool isPlayerInSubmarineRange;
    bool isSubmarineMapOpen;
    [SerializeField] GameObject submarineMap;
    [SerializeField] GameObject submarinePositionOnEnter;
    [SerializeField] GameObject[] submarinePositionsArrayOnEnter;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] PlayerController_SceneTypeChecker sceneTypeChecker;
    [SerializeField] SubmarineZoneDiscovered submarineZones;

    #endregion

    #region VARIABLES
    void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionInteract = InputSystem.actions.FindAction("Interact");
        //submarineMark = GameObject.FindWithTag("SubmarineRangeMark");
        //submarineMap = GameObject.FindWithTag("SubmarineMap");
        canvasFades = GameObject.FindWithTag("PanelFades");
        playerWater = GameObject.FindWithTag("PlayerWater");
        playerGround = GameObject.FindWithTag("PlayerGround");
        submarinePositionOnEnter = GameObject.FindWithTag("SubmarinePositions");

        canvasAnimator = canvasFades.GetComponent<Animator>();
        
        sceneTypeChecker = FindObjectOfType<PlayerController_SceneTypeChecker>();
        submarineZones = FindObjectOfType<SubmarineZoneDiscovered>();
    }

    private void Start()
    {
        buttonZone2.SetActive(false);
        buttonZone3.SetActive(false);
        buttonZone4.SetActive(false);
        buttonZone5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetReferences();
        MapHandler();

        SubmarineMapButtonsActivate();
        UpdateSubmarinePosition();

        if (isSubmarineMapOpen == false && sceneTypeChecker.sceneIndex == 0)
        {
            playerControllerGround.enabled = true;
        }
        else if (isSubmarineMapOpen == false && sceneTypeChecker.sceneIndex != 0)
        {
            playerControllerWater.enabled = true;
        }

        switch (sceneToTPCode)
        {
            case "TP Scene 1":
                sceneToTPSubmarine = 1;
                submarinePositionOnEnter = submarinePositionsArrayOnEnter[0];
                break;
            case "TP Scene 2":
                sceneToTPSubmarine = 2;
                submarinePositionOnEnter = submarinePositionsArrayOnEnter[1];
                break;
            case "TP Scene 3":
                sceneToTPSubmarine = 3;
                submarinePositionOnEnter = submarinePositionsArrayOnEnter[2];
                break;
            case "TP Scene 4":
                sceneToTPSubmarine = 4;
                submarinePositionOnEnter = submarinePositionsArrayOnEnter[3];
                break;
            case "TP Scene 5":
                sceneToTPSubmarine = 5;
                submarinePositionOnEnter = submarinePositionsArrayOnEnter[4];
                break;

        }
    }

    void UpdateSubmarinePosition()
    {
        switch (sceneTypeChecker.sceneIndex)
        {
            case 1:
                this.transform.position = submarinePositionsArrayOnEnter[0].transform.position;
                break;
            case 2:
                this.transform.position = submarinePositionsArrayOnEnter[1].transform.position;
                break;
            case 3:
                this.transform.position = submarinePositionsArrayOnEnter[2].transform.position;
                break;
            case 4:
                this.transform.position = submarinePositionsArrayOnEnter[3].transform.position;
                break;
            case 5:
                this.transform.position = submarinePositionsArrayOnEnter[4].transform.position;
                break;
        }
    }


    private void GetReferences()
    {
        if (!canvasAnimator)
        {
            canvasAnimator = GetComponentInChildren<Animator>();
        }
        if (!submarineMap)
        {
            submarineMap = GameObject.FindWithTag("SubmarineMap");
        }
        if (!playerControllerWater)
        {
            playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        }
        if (!playerControllerGround)
        {
            playerControllerGround = FindObjectOfType<PlayerController_Ground>();
        }
        if (!sceneTypeChecker)
        {
            sceneTypeChecker = FindObjectOfType<PlayerController_SceneTypeChecker>();
        }
        if (!submarineZones)
        {
            submarineZones = FindObjectOfType<SubmarineZoneDiscovered>();
        }
    }

    void MapHandler()
    {
        if (isPlayerInSubmarineRange && actionInteract.WasPressedThisFrame())
        {
            submarineMap.SetActive(true);
            isSubmarineMapOpen = true;
        }

        if (isSubmarineMapOpen == true)
        {
            if (playerGround)
            {
                playerControllerGround.enabled = false;
            }
            else
            {
                Debug.Log("PlayerGround esta off en esta escena");
            }

            if (playerWater)
            {
                playerControllerWater.enabled = false;
            }
            else
            {
                Debug.Log("PlayerWater esta off en esta escena");
            }
        }
    }

    public void DefinirSceneToTPCode(string nuevoValor)
    {
        sceneToTPCode = nuevoValor;
        Debug.Log("Texto asignado: " + sceneToTPCode);
    }

    public void SceneTPSubmarine()
    {
        StartCoroutine(ChangeSceneSubmarine());
    }

    public void CloseSubmarineMap()
    {
        submarineMap.SetActive(false);
        isSubmarineMapOpen = false;
    }

    IEnumerator ChangeSceneSubmarine()
    {
        canvasAnimator.SetTrigger("Iniciar");

        //Desactivo los controles del player
        if (playerControllerWater)
        {
            playerControllerWater.enabled = false;
        }
        if (playerControllerGround)
        {
            playerControllerGround.enabled = false;
        }

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(sceneToTPSubmarine);

        //muevo al player al punto de la pantalla en que quiero que aparezca
        //playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        if (playerWater)
        {
            playerWater.transform.position = submarinePositionOnEnter.transform.position;
        }
        else if (playerGround)
        {
            playerGround.transform.position = submarinePositionOnEnter.transform.position;
        }

        //Reactivo los controles del player
        if (playerControllerWater)
        {
            playerControllerWater.enabled = true;
        }
        if (playerControllerGround)
        {
            playerControllerGround.enabled = true;
        }


    }

    void SubmarineMapButtonsActivate()
    {
        //Activar - desacttivar boton de viaje rapido con submarino a la zona 2 en funcion de si esta descubierta o no
        if (submarineZones.zone2Discovered)
        {
            buttonZone2.SetActive(true);
        }
        else
        {
            buttonZone2.SetActive(false);
        }

        //Activar - desacttivar boton de viaje rapido con submarino a la zona 3 en funcion de si esta descubierta o no
        if (submarineZones.zone3Discovered)
        {
            buttonZone3.SetActive(true);
        }
        else
        {
            buttonZone3.SetActive(false);
        }

        //Activar - desacttivar boton de viaje rapido con submarino a la zona 4 en funcion de si esta descubierta o no
        if (submarineZones.zone4Discovered)
        {
            buttonZone4.SetActive(true);
        }
        else
        {
            buttonZone4.SetActive(false);
        }

        //Activar - desacttivar boton de viaje rapido con submarino a la zona 5 en funcion de si esta descubierta o no
        if (submarineZones.zone5Discovered)
        {
            buttonZone5.SetActive(true);
        }
        else
        {
            buttonZone5.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWater") || collision.gameObject.CompareTag("PlayerGround"))
        {
            isPlayerInSubmarineRange = true;
            submarineMark.SetActive(true); // Activa el objeto visual para indicar que el jugador está en rango.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWater") || collision.gameObject.CompareTag("PlayerGround"))
        {
            isPlayerInSubmarineRange = false;
            submarineMark.SetActive(false); // Desactiva el objeto visual para indicar que el jugador ya no está en rango.
        }
    }
    #endregion
}
