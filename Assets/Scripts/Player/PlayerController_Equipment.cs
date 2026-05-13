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
    public InputAction actionAttack;
    InputAction actionEquipo1Camera;
    InputAction actionEquipo2NetLauncher;

    [Header("Variables generales")]
    [SerializeField] Vector2 moveAmmount;
    public GameObject menuPDA;
    [SerializeField] float shootingInterval; 

    //Variables base para controlar la cadencia de disparo del arma
    public bool menuPDAActivated;
    [SerializeField] bool canShoot;

    [Header("Variables del Animator")]
    public bool cameraEquipped;
    [SerializeField] bool cameraTakePhoto;
    [SerializeField] bool netLauncherEquipped;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] CameraFollowMouse cameraFollowMouse;
    [SerializeField] NetLauncherFollowMouse netLauncherFollowMouse;
    [SerializeField] Animator animator;
    [SerializeField] Animator animatorCamera;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] NetLauncher netLauncher;

    [Header("Otras Variables")]
    Vector2 movement;

    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionAttack = InputSystem.actions.FindAction("Attack");
        actionEquipo1Camera = InputSystem.actions.FindAction("Equipo1_Camera");
        actionEquipo2NetLauncher = InputSystem.actions.FindAction("Equipo2_NetLauncher");

        //ASIGNO LAS VARIABLES DE COMPONENTES
        animator = GetComponent<Animator>();
        animatorCamera = GameObject.FindWithTag("CameraPhotos").GetComponent<Animator>();
        cameraFollowMouse = GetComponentInChildren<CameraFollowMouse>();
        netLauncherFollowMouse = GetComponentInChildren<NetLauncherFollowMouse>();
        playerControllerWater = GetComponentInChildren<PlayerControllerWater>();
        netLauncher = GetComponentInChildren<NetLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        //EQUIPMENT FUNCTIONS
        CameraEquip();
        TakePhoto();
        NetLauncherEquip();
        NetShoot();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("CameraEquipped", cameraEquipped);
        animator.SetBool("NetLauncherEquipped", netLauncherEquipped);
    }

    #region EQUIPMENT
    void CameraEquip()
    {
        if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == false)
        {
            cameraEquipped = true;
            cameraFollowMouse.enabled = true;
        }
        else if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == true)
        {
            cameraEquipped = false;
            cameraFollowMouse.enabled = false;
        }
        else if (playerControllerWater.moveAmmount != Vector2.zero)
        {
            cameraEquipped = false;
            cameraFollowMouse.enabled = false;
        }
    }

    void TakePhoto()
    {
        if (cameraEquipped && actionAttack.WasPressedThisFrame())
        {
            animator.SetTrigger("CameraTakePhoto"); 
            animatorCamera.SetTrigger("TakePhoto");
        }
    }

    void NetLauncherEquip()
    {
        if (actionEquipo2NetLauncher.WasPressedThisFrame() && netLauncherEquipped == false)
        {
            netLauncherEquipped = true;
            netLauncherFollowMouse.enabled = true;
        }
        else if (actionEquipo2NetLauncher.WasPressedThisFrame() && netLauncherEquipped == true)
        {
            netLauncherEquipped = false;
            netLauncherFollowMouse.enabled = false;
        }
        else if (playerControllerWater.moveAmmount != Vector2.zero)
        {
            netLauncherEquipped = false;
            netLauncherFollowMouse.enabled = false;
        }
    }

    void NetShoot()
    {
        if (netLauncherEquipped && !netLauncher.netAlreadyInstantiated && actionAttack.WasPressedThisFrame())
        {
            animator.SetTrigger("NetLauncherShoot");
            netLauncher.Shoot();
        }
    }
    #endregion
}