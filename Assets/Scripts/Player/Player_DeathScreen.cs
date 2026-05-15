using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject canvasDeath; //Canvas de muerte
    [SerializeField] Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        if (!canvasDeath)
        {
            canvasDeath = GameObject.FindWithTag("CanvasDeath");
        }
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.playerDead) //CHECK SI ESTÁ MUERTO PARA MOSTRAR PANTALLA MUERTE
        {
            canvasDeath.SetActive(true);
        }
    }
}
