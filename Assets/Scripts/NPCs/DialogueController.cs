using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GameObject coralius;
    [SerializeField] GameObject mortimer;
    [SerializeField] DialogoPorInteract coraliusDialogue;
    [SerializeField] DialogoPorInteract mortimerDialogue;
    public bool isDialogueActive = false;
    #endregion

    #region METHODS
    private void Awake()
    {
        coralius = GameObject.FindWithTag("DialogueCoralius");
        mortimer = GameObject.FindWithTag("DialogueMortimer");
        coraliusDialogue = coralius.GetComponent<DialogoPorInteract>();
        mortimerDialogue = mortimer.GetComponent<DialogoPorInteract>();
    }

    private void Update()
    {
        GatherMissingRefs();
        CheckDialogueRunning();
    }

    void CheckDialogueRunning() //Comprueba si cualquiera de los dialogos esta teniendo lugar
    {
        if (coraliusDialogue.didDialogueStart || mortimerDialogue.didDialogueStart)
        {
            isDialogueActive = true;
        }
        else
        {
            isDialogueActive = false;
        }
    }
    
    void GatherMissingRefs()
    {
        if (!coralius)
            {
                coralius = GameObject.FindWithTag("DialogueCoralius");
            }
        if (!mortimer)
            {
                mortimer = GameObject.FindWithTag("DialogueMortimer");
            }
        if (!coraliusDialogue)
            {
                coraliusDialogue = coralius.GetComponent<DialogoPorInteract>();
            }
        if (!mortimerDialogue)
            {
                mortimerDialogue = mortimer.GetComponent<DialogoPorInteract>();
            }
    }
    #endregion
}
