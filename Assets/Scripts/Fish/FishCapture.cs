using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCapture : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] SpriteRenderer fishStandard;
    [SerializeField] GameObject fishCaptured;
    [SerializeField] Transform player;
    [SerializeField] float speed = 10f; // Units per second

    public bool moveToPlayer = false;
    [SerializeField] Fish fishScript;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        fishStandard = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("FishGatherer").GetComponent<Transform>();
        fishScript = GetComponent<Fish>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveToPlayer)
        {
            fishScript.enabled = false;
            fishCaptured.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (moveToPlayer)
        {
            if (transform.position != player.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }

    public void Captured()
    {
        //fishStandard.enabled = false; //Desactivo sprite normal
        fishCaptured.GetComponent<SpriteRenderer>().enabled = true; //Activo sprite de la version capturada
        fishScript.enabled = true;
        moveToPlayer = true;
    }
    #endregion
}
