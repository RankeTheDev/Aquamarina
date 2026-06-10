using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_PDA : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;
    InputAction actionPDAWater;
    InputAction actionPDAGround;
    InputAction actionAjustesWater;
    InputAction actionAjustesGround;

    public GameObject menuPDA;
    public GameObject menuPausa;
    //public GameObject menuAjustes;
    public bool menuPDAActivated;
    public bool menuPausaActivated;
    [SerializeField] MenuPausa menuPausaScript;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start() ///GUARDO PREFERENCIAS
    {
        actionPDAWater = InputSystem.actions.FindAction("Player_Water/PDA");
        actionPDAGround = InputSystem.actions.FindAction("Player_Ground/PDA");
        actionAjustesWater = InputSystem.actions.FindAction("Player_Water/Ajustes");
        actionAjustesGround = InputSystem.actions.FindAction("Player_Ground/Ajustes");
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuPDA)
        {
            menuPDA = GameObject.FindWithTag("PDAMenu");
        }
        if (!menuPausa)
        {
            menuPausa = GameObject.FindWithTag("MenuPausa");
        }
        /*if (!menuAjustes)
        {
            menuAjustes = GameObject.FindWithTag("MenuAjustes");
        }*/
        if (!menuPausaScript)
        {
            menuPausaScript = FindObjectOfType<MenuPausa>();
        }

        OpenClosePDA();
        OpenCloseMenu();
    }

    void OpenClosePDA()
    {
        menuPDA.SetActive(true);

        if (!menuPDAActivated)
        {
            menuPDA.SetActive(false);
        }

        if ((actionPDAWater.WasPressedThisFrame() || actionPDAGround.WasPressedThisFrame()) && menuPDAActivated)
        {
            Time.timeScale = 1;
            menuPDA.SetActive(false);
            menuPDAActivated = false;
        }
        else if ((actionPDAWater.WasPressedThisFrame() || actionPDAGround.WasPressedThisFrame()) && !menuPDAActivated)
        {
            Time.timeScale = 0;
            menuPDA.SetActive(true);
            menuPDAActivated = true;
        }
    }

    void OpenCloseMenu()
    {
        //menuPausa.SetActive(true);

        if (!menuPausaActivated)
        {
            menuPausa.SetActive(false);
        }
        if ((actionAjustesGround.WasPressedThisFrame() || actionAjustesWater.WasPressedThisFrame()) && menuPausaActivated) //Salgo de pausa
        {
            Time.timeScale = 1;
            menuPausa.SetActive(false);
            menuPausaActivated = false;
            menuPausaScript.juegoPausado = false;
        }
        else if ((actionAjustesGround.WasPressedThisFrame() || actionAjustesWater.WasPressedThisFrame()) && !menuPausaActivated) //Entro a pausa
        {
            Time.timeScale = 0;
            menuPausa.SetActive(true);
            menuPausaActivated = true;
            menuPausaScript.juegoPausado = true;
        }
    }
    #endregion
}
