using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] Animator canvasAnimator;

    [SerializeField] AnimationClip animacionFinal;
    [SerializeField] GameObject canvasFades;
    [SerializeField] PlayerControllerWater playerControllerWater;
    [SerializeField] PlayerController_Ground playerControllerGround;
    [SerializeField] GameObject player;
    [SerializeField] PlayerController_SceneTypeChecker player_SceneTypeChecker;
    [SerializeField] PlayerController_Triggers playerController_Triggers;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        player_SceneTypeChecker = player.GetComponent<PlayerController_SceneTypeChecker>();
        playerControllerWater = player.GetComponent<PlayerControllerWater>();
        playerControllerGround = player.GetComponent<PlayerController_Ground>();
        playerController_Triggers = player.GetComponent<PlayerController_Triggers>();
    }

    public void SceneChange()
    {
        playerController_Triggers.isTransitioningToScene = true;

        //Desactivo los controles del player
        playerControllerWater.enabled = false;
        playerControllerGround.enabled = false;

        StartCoroutine(ChangeScenePlayer());
    }

    IEnumerator ChangeScenePlayer()
    {
        canvasAnimator.SetTrigger("Iniciar");

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(playerController_Triggers.sceneToTPPlayer);

        player.GetComponent<Transform>().position = playerController_Triggers.playerPositionOnEnter.transform.position;

        /*//Reactivo los controles del player. Desactivado porque puede que se haga solo desde scen checker con el isTransitioningToScene
        if (playerControllerWater)
        {
            playerControllerWater.enabled = true;
        }
        if (playerControllerGround)
        {
            playerControllerGround.enabled = true;
        }*/

        playerController_Triggers.isTransitioningToScene = false;
    }
    #endregion
}