using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_PDA : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;
    InputAction actionPDA;
    InputAction actionAjustes;

    public GameObject menuPDA;
    public GameObject menuAjustes;
    public bool menuPDAActivated;
    public bool menuAjustesActivated;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start() ///GUARDO PREFERENCIAS
    {
        actionPDA = InputSystem.actions.FindAction("PDA");
        actionAjustes = InputSystem.actions.FindAction("Ajustes");
        if (!menuPDA)
        {
            menuPDA = GameObject.FindWithTag("PDAMenu");
        }
        if (!menuAjustes)
        {
            menuAjustes = GameObject.FindWithTag("MenuAjustes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        OpenClosePDA();
        //OpenCloseMenu();
    }

    void OpenClosePDA()
    {
        menuPDA.SetActive(true);

        if (!menuPDAActivated)
        {
            menuPDA.SetActive(false);
        }

        if (actionPDA.WasPressedThisFrame() && menuPDAActivated)
        {
            Time.timeScale = 1;
            menuPDA.SetActive(false);
            menuPDAActivated = false;
        }
        else if (actionPDA.WasPressedThisFrame() && !menuPDAActivated)
        {
            Time.timeScale = 0;
            menuPDA.SetActive(true);
            menuPDAActivated = true;
        }
    }

    /*void OpenCloseMenu()
    {
        menuAjustes.SetActive(true);

        if (!menuAjustesActivated)
        {
            menuAjustes.SetActive(false);
        }
        if (actionAjustes.WasPressedThisFrame() && menuAjustesActivated)
        {
            Time.timeScale = 1;
            menuAjustes.SetActive(false);
            menuAjustesActivated = false;
        }
        else if (actionAjustes.WasPressedThisFrame() && !menuAjustesActivated)
        {
            Time.timeScale = 0;
            menuAjustes.SetActive(true);
            menuAjustesActivated = true;
        }
    }*/
    #endregion
}
