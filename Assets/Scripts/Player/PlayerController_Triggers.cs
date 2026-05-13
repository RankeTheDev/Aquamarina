using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Triggers : MonoBehaviour
{
    #region VARIABLES
    public int sceneToTPPlayer;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] Timer timer;
    [SerializeField] Animator animator;
    [SerializeField] Animator bubblesDamageAnimator;
    [SerializeField] SceneTransition sceneTransition;
    [SerializeField] PlayerController_Equipment playerEquipment;

    [SerializeField] GameObject bubblesDamage;
    public GameObject playerPositionOnEnter;
    [SerializeField] GameObject[] playerPositionsArrayOnEnter;

    #endregion 

    #region METHODS
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        timer = GetComponent<Timer>();
        animator = GetComponent<Animator>();
        sceneTransition = FindObjectOfType<SceneTransition>();
        playerEquipment = FindObjectOfType<PlayerController_Equipment>();
    }

    // Update is called once per frame
    void Update()
    {
        //If scene transition script isn´t assigned, find it and assign it
        if (sceneTransition == null)
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
        }
    }

    #region TRIGGERS COLLISIONS CHECKING
    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Detects if player takes damage
        if (trigger.gameObject.tag == ("Damage"))
        {
            timer.currentTime -= timer.depleteAir;
            animator.SetTrigger("DamageTaken");
            bubblesDamageAnimator.SetTrigger("Damage");
        }

        //Detects if player touches a teleporter collider
        if (trigger.gameObject.tag == ("Teleporter"))
        {
            switch (trigger.name)
            {
                case "TP Scene 1":
                    sceneToTPPlayer = 1;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[0];
                    break;
                case "TP Scene 2L":
                    sceneToTPPlayer = 2;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[1];
                    break;
                case "TP Scene 2R":
                    sceneToTPPlayer = 2;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[2];
                    break;
                case "TP Scene 3In":
                    sceneToTPPlayer = 3;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[3];
                    break;
                case "TP Scene 3Out":
                    sceneToTPPlayer = 3;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[4];
                    break;
                case "TP Scene 4L":
                    sceneToTPPlayer = 4;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[5];
                    break;
                case "TP Scene 4R":
                    sceneToTPPlayer = 4;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[6];
                    break;
                case "TP Scene 5":
                    sceneToTPPlayer = 5;
                    playerPositionOnEnter = playerPositionsArrayOnEnter[7];
                    break;
            }

            sceneTransition.SceneChange();
        }
    }
    #endregion TRIGGERS COLLISIONS CHECKING
    #endregion METHODS
}
