using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    //variables
    private float startPosx, length;
    private float startPosy;
    public GameObject cam;
    public float parallaxEffectx; //velocidad a la que se mueve el background en x
    public float parallaxEffecty; //velocidad a la que se mueve el background en y
    private float distancex;
    private float distancey;
    private float movement;

    void Start()
    {
        startPosx = transform.position.x;
        startPosy = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //Coge los límites del sprite
        cam = GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        distancex = cam.transform.position.x * parallaxEffectx; //1 = movimiento junto a la cam | 0 = No se mueve | 0,5 = punto medio
        distancey = cam.transform.position.y * parallaxEffecty; //1 = movimiento junto a la cam | 0 = No se mueve | 0,5 = punto medio

        movement = cam.transform.position.x * (1 - parallaxEffectx);

        transform.position = new Vector3 (startPosx + distancex, startPosy + distancey, transform.position.z);
        /*
        if (movement > startPosx + (length / 2))
        {
            startPosx += length;
        }
        else if (movement < startPosx - (length / 2))
        {
            startPosx -= length;
        }

        if (movement > startPosx + length)
        {
            startPosx += length;
        }
        else if (movement < startPosx + length)
        {
            startPosx -= length;
        }
        */
    }
}
