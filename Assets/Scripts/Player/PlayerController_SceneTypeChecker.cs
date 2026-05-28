using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController_SceneTypeChecker : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    //Valores de gravedad según el tipo de nivel
    public float gravityWater = 0.005f;
    public float gravityGround = 1f;

    //Para Flipear el player
    public bool facingRight = false;

    //Comprobaciones de Ground/Water para desactivar controles segun tipo de nivel
    public int sceneIndex;
    [SerializeField] Scene currentScene;

    //Almacena referencias a los scripts de control de player para desactivarlos segun tipo de nivel y otros datos necesarios
    public bool playerGroundIsActive;
    public bool playerWaterIsActive;
    [SerializeField] GameObject player; //GameObject del player
    [SerializeField] PlayerControllerWater playerControllerWater; //Script controller de water
    [SerializeField] PlayerController_Ground playerControllerGround; //Script controller de tierra
    [SerializeField] PlayerController_Equipment playerControllerEquipment; //Script controller de tierra
    [SerializeField] SceneTransition sceneTransition; //Script de transicion de escena
    [SerializeField] Animator animatorPlayer; //Animator del player

    #endregion

    #region METHODS 
    // Awake is called when the script instance is being loaded
    void Awake() //Guardo preferencias de objects y componentes/scripts
    {
        player = GameObject.FindWithTag("Player");
        animatorPlayer = player.GetComponent<Animator>();
        playerControllerWater = player.GetComponent<PlayerControllerWater>();
        playerControllerEquipment = player.GetComponent<PlayerController_Equipment>();
        playerControllerGround = player.GetComponent<PlayerController_Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (1 es la terrestre)


        if (sceneIndex == 1) //Si la escena actual es la 1
        {
            if (facingRight)
            {
                playerControllerWater.enabled = false; //Desactivo el controller de water
                playerControllerEquipment.enabled = false; //Desactivo el controller de equipos 
            }
            playerControllerGround.enabled = true; //Activo el controller de tierra
            animatorPlayer.SetBool("IsGrounded", true); //Activo el bool de grounded para el animator del player
        }
        else //En demás casos
        {
            playerControllerWater.enabled = true; //Activo el controller de water
            playerControllerGround.enabled = false; //Desactivo el controller de tierra;
            playerControllerEquipment.enabled = true; //Activo el controller de equipos
            animatorPlayer.SetBool("IsGrounded", false); //Desactivo el bool de grounded para el animator del player
        }
    }
    #endregion
}
