using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_SceneTypeChecker : MonoBehaviour
{
    #region VARIABLES
    //Valores de gravedad según el tipo de nivel
    public float gravityWater = 0.005f;
    public float gravityGround = 1f;

    //Para Flipear el player
    public bool facingRight = false;

    //Comprobaciones de Ground/Water para desactivar controles segun tipo de nivel
    public int sceneIndex;
    [SerializeField] Scene currentScene;

    //Almacena referencias a los scripts de control de player para desactivarlos segun tipo de nivel y otros datos necesarios
    //[SerializeField] GameObject playerWater; //SI SE USASE LA MECANICA DE 2 PLAYERS
    //[SerializeField] GameObject playerGround; //SI SE USASE LA MECANICA DE 2 PLAYERS
    public bool playerGroundIsActive;
    public bool playerWaterIsActive;
    [SerializeField] GameObject player; //GameObject del player
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] Animator animatorPlayer;

    #endregion

    #region METHODS
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //playerWater = GameObject.FindWithTag("PlayerWater");
        //playerGround = GameObject.FindWithTag("PlayerGround");

        player = GameObject.FindWithTag("Player");
        animatorPlayer = player.GetComponent<Animator>();
        playerControllerWater = player.GetComponent<PlayerControllerWater>();
        playerControllerGround = player.GetComponent<PlayerController_Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerControllerWater.enabled = false; //MUERE

        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (1 es la terrestre)

        if (sceneIndex == 1)
        {
            playerControllerWater.enabled = false;
            playerControllerGround.enabled = true;
            animatorPlayer.SetBool("IsGrounded", true);
            //playerGround.SetActive(true); //Activa el control terrestre
            //playerWater.SetActive(false); //Desactiva el control acuático
            //playerGroundIsActive = true; //Valida como verdadera la variable que indica que playerGround es el activo
            //playerWaterIsActive = false; //Valida como falsa la variable que indica que playerWater es el activo
        }
        else
        {
            playerControllerWater.enabled = true;
            playerControllerGround.enabled = false;
            animatorPlayer.SetBool("IsGrounded", false);
            //playerWater.SetActive(true); //Activa el control terrestre
            //playerGround.SetActive(false); //Desactiva el control acuático
            //playerWaterIsActive = true; //Valida como falsa la variable que indica que playerGround es el activo
            //playerGroundIsActive = false; //Valida como verdadera la variable que indica que playerWater es el activo
        }
    }
    #endregion
}
