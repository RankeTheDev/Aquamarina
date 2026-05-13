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

    public GameObject menuPDA;
    public bool menuPDAActivated;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        actionPDA = InputSystem.actions.FindAction("PDA");
        if (!menuPDA)
        {
            menuPDA = GameObject.FindWithTag("PDAMenu");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        OpenClosePDA();
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
    #endregion
}
