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
    [SerializeField] GameObject playerWater;
    [SerializeField] GameObject playerGround;

    #endregion

    #region METHODS
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        playerWater = GameObject.FindWithTag("PlayerWater");
        playerGround = GameObject.FindWithTag("PlayerGround");
    }

    // Update is called once per frame
    void Update()
    {
        //playerControllerWater.enabled = false; //MUERE

        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (1 es la terrestre)

        if (sceneIndex == 1)
        {
            playerGround.SetActive(true); //Activa el control terrestre
            playerWater.SetActive(false); //Desactiva el control acuático
        }
        else
        {
            playerWater.SetActive(true); //Activa el control terrestre
            playerGround.SetActive(false); //Desactiva el control acuático
        }

        IgualarTransform();
    }

    void IgualarTransform()
    {
        playerGround.transform.position = playerWater.transform.position;
        playerWater.transform.position = playerGround.transform.position;

        playerGround.transform.localScale = playerWater.transform.localScale;
        playerWater.transform.localScale = playerGround.transform.localScale;
    }
    #endregion
}
