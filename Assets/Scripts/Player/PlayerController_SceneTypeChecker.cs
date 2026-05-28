using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController_SceneTypeChecker : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    //Valores de gravedad según el tipo de nivel
    public float gravityWater = 0.005f;
    public float gravityGround = 1f;

    //Para Flipear el player
    public bool facingRight = false;

    //Comprobaciones de Ground/Water para desactivar controles segun tipo de nivel
    public int sceneIndex;
    [SerializeField] Scene currentScene;

    //Almacena referencias a los scripts de control de player para desactivarlos segun tipo de nivel y otros datos necesarios
    [SerializeField] GameObject player; //GameObject del player
    [SerializeField] PlayerControllerWater playerControllerWater; //Script controller de water
    [SerializeField] PlayerController_Ground playerControllerGround; //Script controller de tierra
    [SerializeField] PlayerController_Equipment playerControllerEquipment; //Script controller de tierra
    [SerializeField] SceneTransition sceneTransition; //Script de transicion de escena
    [SerializeField] PlayerController_Triggers playerController_Triggers;
    [SerializeField] Animator animatorPlayer; //Animator del player
    #endregion

    #region METHODS 
    // Awake is called when the script instance is being loaded
    void Awake() //Guardo preferencias de objects y componentes/scripts
    {
        player = this.gameObject;
        animatorPlayer = GetComponent<Animator>();
        playerControllerWater = GetComponent<PlayerControllerWater>();
        playerControllerEquipment = GetComponent<PlayerController_Equipment>();
        playerControllerGround = GetComponent<PlayerController_Ground>();
        playerController_Triggers = GetComponent<PlayerController_Triggers>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateControllers();
    }

    void ActivateControllers()
    {
        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (1 es la terrestre)
        
        if (playerController_Triggers.isTransitioningToScene == true)
        {
            //Desactivo los controles del player
            playerControllerWater.enabled = false;
            playerControllerGround.enabled = false;
        }
        else
        {
            if (sceneIndex == 1) //Si la escena actual es la 1
            {
                playerControllerGround.enabled = true; //Activo el controller de tierra
                playerControllerWater.enabled = false; //Desactivo el controller de water
                playerControllerEquipment.enabled = false; //Desactivo el controller de equipos 
                animatorPlayer.SetBool("IsGrounded", true); //Activo el bool de grounded para el animator del player
            }
            else //En demás casos
            {
                playerControllerWater.enabled = true; //Activo el controller de water
                playerControllerGround.enabled = false; //Desactivo el controller de tierra
                playerControllerEquipment.enabled = true; //Activo el controller de equipos
                animatorPlayer.SetBool("IsGrounded", false); //Desactivo el bool de grounded para el animator del player
            }
        }

        /*
        if (sceneIndex == 1) //Si la escena actual es la 1
        {
            //GroundContollerOn();
            //playerControllerGround.enabled = true; //Activo el controller de tierra
            //playerControllerWater.enabled = false; //Desactivo el controller de water
            playerControllerEquipment.enabled = false; //Desactivo el controller de equipos 
            animatorPlayer.SetBool("IsGrounded", true); //Activo el bool de grounded para el animator del player
        }
        else if (sceneIndex != 0 && sceneIndex != 1) //En demás casos
        {
            //WaterControllerOn();
            //playerControllerGround.enabled = false; //Desactivo el controller de tierra
            //playerControllerWater.enabled = true; //Activo el controller de water
            playerControllerEquipment.enabled = true; //Activo el controller de equipos
            animatorPlayer.SetBool("IsGrounded", false); //Desactivo el bool de grounded para el animator del player
        }
        else
        {
            //Desactivo los controles del player
            playerControllerWater.enabled = false;
            playerControllerGround.enabled = false;
        }
        */
    }

    void GroundContollerOn()
    {
        if (playerController_Triggers.isTransitioningToScene == true)
        {
            //Desactivo los controles del player
            playerControllerWater.enabled = false;
            playerControllerGround.enabled = false;
        }
        else
        {
            playerControllerGround.enabled = true; //Activo el controller de tierra
        }
    }

    void WaterControllerOn()
    {
        if (playerController_Triggers.isTransitioningToScene == true)
        {
            //Desactivo los controles del player
            playerControllerWater.enabled = false;
            playerControllerGround.enabled = false;
        }
        else
        {
            playerControllerWater.enabled = true; //Activo el controller de water
        }
    }
    #endregion
}
