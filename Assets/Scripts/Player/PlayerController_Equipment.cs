using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController_Equipment : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;
    InputAction actionAttack;
    InputAction actionEquipo1Camera;
    InputAction actionEquipo2NetLauncher;
    InputAction actionPDA;

    [Header("Variables generales")]
    [SerializeField] Vector2 moveAmmount;
    public GameObject menuPDA;
    public bool menuPDAActivated;

    [Header("Variables del Animator")]
    public bool cameraEquipped;
    [SerializeField] bool cameraTakePhoto;
    [SerializeField] bool netLauncherEquipped;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] FollowMouse followMouse;
    [SerializeField] Animator animator;
    [SerializeField] PlayerControllerWater playerControllerWater;

    [Header("Otras Variables")]
    Vector2 movement;
    [SerializeField] GameObject cameraEquipment;
    [SerializeField] GameObject cameraEquipmentBasePosition;

    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionAttack = InputSystem.actions.FindAction("Attack");
        actionEquipo1Camera = InputSystem.actions.FindAction("Equipo1_Camera");
        actionEquipo2NetLauncher = InputSystem.actions.FindAction("Equipo2_NetLauncher");
        actionPDA = InputSystem.actions.FindAction("PDA");

        //ASIGNO LAS VARIABLES DE COMPONENTES
        animator = GetComponent<Animator>();
        followMouse = GetComponentInChildren<FollowMouse>();
        playerControllerWater = GetComponentInChildren<PlayerControllerWater>();
        cameraEquipment = GameObject.FindWithTag("CameraPhotos");
        menuPDA = GameObject.FindWithTag("PDAMenu");
        cameraEquipmentBasePosition = GameObject.FindWithTag("CameraPhotosBasePoint");
    }

    // Update is called once per frame
    void Update()
    {
        //EQUIPMENT FUNCTIONS
        OpenClosePDA();
        CameraEquip();
        TakePhoto();
        NetLauncherEquip();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("CameraEquipped", cameraEquipped);
        animator.SetBool("NetLauncherEquipped", netLauncherEquipped);
    }

    #region EQUIPMENT
    void CameraEquip()
    {
        if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == false)
        {
            cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
            cameraEquipped = true;
            followMouse.enabled = true;
        }
        else if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == true)
        {
            cameraEquipped = false;
            followMouse.enabled = false;
            cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
        }
        else if (playerControllerWater.moveAmmount != Vector2.zero)
        {
            cameraEquipped = false;
            followMouse.enabled = false;
            cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
        }
    }

    void TakePhoto()
    {
        if (cameraEquipped && actionAttack.WasPressedThisFrame())
        {
            animator.SetTrigger("CameraTakePhoto");
        }
    }

    void NetLauncherEquip()
    {
        if (actionEquipo2NetLauncher.WasPressedThisFrame() && netLauncherEquipped == false)
        {
            netLauncherEquipped = true;
            //cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
            //followMouse.enabled = true;
        }
        else if (actionEquipo2NetLauncher.WasPressedThisFrame() && netLauncherEquipped == true)
        {
            netLauncherEquipped = false;
            //followMouse.enabled = false;
            //cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
        }
        else if (playerControllerWater.moveAmmount != Vector2.zero)
        {
            netLauncherEquipped = false;
            //followMouse.enabled = false;
            //cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
        }
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
