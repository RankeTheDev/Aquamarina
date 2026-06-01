using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    #region VARIABLES
   

    public float totalTime = 180f; // Tiempo total del timer
    public float timeDecreaseSpeed = 1f; //Modificador de velocidad a la que disminuye el aire
    public float currentTime; //Cantidad de aire que tiene el player actualmente
    public float addAir = 30f; //Cantidad de aire que consigue el player al tomar burbujas
    public float depleteAir = 10f; //Cantidad de aire que pierde el player al recibir daño (a futuro variaría según origen del daño)
    public bool playerDead = false;
    public Image Oxigen;
    public Sprite[] OxigenState;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
   
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizar el contador
        currentTime -= Time.deltaTime * timeDecreaseSpeed;

        if (currentTime <= 0f)
        {
            playerDead = true;
        }
        else
        {
            playerDead = false;
        }

        //Evitar que el contador baje de 0
        if (currentTime < 0f)
        {
            currentTime = 0f;
            timeDecreaseSpeed = 0f;
        }

        //Evitar que el contador suba de la capacidad máxima del tanque de 02
        if (currentTime > totalTime)
        {
            currentTime = totalTime;
        }

        //Transforma el aire en un numero entre 0 y 1
        float porcentaje = currentTime / totalTime;

        //Parar convertir el porcentaje en sprite
        int index = Mathf.FloorToInt((1-porcentaje)*(OxigenState.Length-1));

        //Para que no se rompa dand negativos o superiores
        index = Mathf.Clamp(index, 0, OxigenState.Length - 1);

        //Cambio de sprite
        Oxigen.sprite = OxigenState[index];
    }

    

    public void AddTime(float ammountTOChangeStat)
    {
        currentTime += ammountTOChangeStat;
    }
    #endregion
}
