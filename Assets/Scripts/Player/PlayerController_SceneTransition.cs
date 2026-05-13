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
        player_SceneTypeChecker = FindObjectOfType<PlayerController_SceneTypeChecker>();
        playerControllerWater = FindObjectOfType<PlayerControllerWater>();
        playerControllerGround = FindObjectOfType<PlayerController_Ground>();
        playerController_Triggers = FindObjectOfType<PlayerController_Triggers>();
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
        if (playerControllerWater)
        {
            playerControllerWater.enabled = false;
        }
        if (playerControllerGround)
        {
            playerControllerGround.enabled = false;
        }

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(playerController_Triggers.sceneToTPPlayer);

        //muevo al player al punto de la pantalla en que quiero que aparezca
        //playerPositionOnEnter = GameObject.FindWithTag("PositionPlayerOnEntry");
        /*if (player_SceneTypeChecker.playerGroundIsActive)
        {
            playerGround.GetComponent<Transform>().position = playerTriggersWater.playerPositionOnEnter.transform.position;
        }
        else if (player_SceneTypeChecker.playerWaterIsActive)
        {
            playerWater.GetComponent<Transform>().position = playerTriggersWater.playerPositionOnEnter.transform.position;
        }*/

        player.GetComponent<Transform>().position = playerController_Triggers.playerPositionOnEnter.transform.position;

        //Reactivo los controles del player
        if (playerControllerWater)
        {
            playerControllerWater.enabled = true;
        }
        if (playerControllerGround)
        {
            playerControllerGround.enabled = true;
        }
    }

    #endregion
}
