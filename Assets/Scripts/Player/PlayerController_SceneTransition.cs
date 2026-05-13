using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] Animator canvasAnimator;

    [SerializeField] AnimationClip animacionFinal;
    [SerializeField] GameObject playerWater;
    [SerializeField] GameObject playerGround;
    [SerializeField] GameObject canvasFades;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] PlayerController_Triggers playerTriggersWater;
    [SerializeField] PlayerController_Triggers playerTriggersGround;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();
        playerWater = GameObject.FindWithTag("PlayerWater");
        playerGround = GameObject.FindWithTag("PlayerGround");
        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        playerControllerGround = FindObjectOfType<PlayerController_Ground>();
        playerTriggersWater = playerControllerWater.GetComponent<PlayerController_Triggers>();
        playerTriggersGround = playerControllerWater.GetComponent<PlayerController_Triggers>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneChange()
    {
        StartCoroutine(ChangeScenePlayer());
    }

    IEnumerator ChangeScenePlayer()
    {
        canvasAnimator.SetTrigger("Iniciar");

        //Desactivo los controles del player
        if (playerControllerWater != null)
        {
            playerControllerWater.enabled = false;
        }
        if (playerControllerGround != null)
        {
            playerControllerGround.enabled = false;
        }

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(playerTriggersWater.sceneToTPPlayer);

        //muevo al player al punto de la pantalla en que quiero que aparezca
        //playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        playerWater.GetComponent<Transform>().position = playerTriggersWater.playerPositionOnEnter.transform.position;      

        //Reactivo los controles del player
        if (playerControllerWater != null)
        {
            playerControllerWater.enabled = true;
        }
        if (playerControllerGround != null)
        {
            playerControllerGround.enabled = true;
        }
    }

    #endregion
}
