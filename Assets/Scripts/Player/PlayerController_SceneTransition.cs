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
    [SerializeField] GameObject player;
    [SerializeField] PlayerController_Triggers playerController_Triggers;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        canvasFades = GameObject.FindWithTag("PanelFades");
        canvasAnimator = canvasFades.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerController_Triggers = player.GetComponent<PlayerController_Triggers>();
    }

    public void SceneChange()
    {
        StartCoroutine(ChangeScenePlayer());
    }

    IEnumerator ChangeScenePlayer()
    {
        playerController_Triggers.isTransitioningToScene = true;
        
        canvasAnimator.SetTrigger("Iniciar");

        yield return new WaitForSeconds(animacionFinal.length);
                
        SceneManager.LoadScene(playerController_Triggers.sceneToTPPlayer);

        player.GetComponent<Transform>().position = playerController_Triggers.playerPositionOnEnter.transform.position;

        playerController_Triggers.isTransitioningToScene = false;
    }
    #endregion
}