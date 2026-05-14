using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCapture : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] SpriteRenderer fishStandard; //Guarda el spriterenderer del pez no capturado
    [SerializeField] GameObject fishCaptured; //Almaceno el hijo con el spritrenderer de la red
    [SerializeField] Transform player; //Posicion del player
    [SerializeField] float speed = 10f; // Units per second

    public AudioManager audioManager; //Manager de audio

    public bool moveToPlayer = false; //Ha sido capturado y esta yendo hacia el player
    [SerializeField] Fish fishScript; //Script de Fish

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start() //Guardo preferencias de objects y componentes/scripts
    {
        fishStandard = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("FishGatherer").GetComponent<Transform>();
        fishScript = GetComponent<Fish>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveToPlayer) //Si no se esta moviendo al player desactiva el script de fish y el spriterenderer de la version capturada
        {
            fishScript.enabled = false;
            fishCaptured.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (moveToPlayer) //Si se esta moviendo al player y su posicion no es igual, sigue moviendose hacia este
        {
            if (transform.position != player.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }

    public void Captured()
    {
        fishCaptured.GetComponent<SpriteRenderer>().enabled = true; //Activo sprite de la version capturada
        fishScript.enabled = true; //Pez del Fish activo
        moveToPlayer = true; // Bool para indicar que debe moverse hacia el player
    }
    #endregion
}
