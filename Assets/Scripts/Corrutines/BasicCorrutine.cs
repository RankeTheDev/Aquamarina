using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BasicCorrutine : MonoBehaviour
{
    #region variables
    int speed;
    public Transform[] points;
    NavMeshAgent agent;
    private int currentPosition = 0;
    public Transform tarject; //tarject es el Player
    public Rigidbody2D tarjectRigidbody;
    public bool scared = false;
    public Vector2 distanceDifference;
    public float scaredVelocity; //Velocidad en la que se mueve cuendo se asusta
    public float detectVelocity; //Velocidad límite para detectar al Player
    public PlayerControllerWater playerScript;
    [SerializeField] SpriteRenderer spriteRenderer;
    #endregion

    #region variables no usadas
  /*public float alfa;
    public float alfaMax = 1f;
    public float alfaMin = 0f;
    public float tiempoDesaparición = 1f;*/
    #endregion

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        #region Ajustes Iniciales
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //alfa = spriteRenderer.color.a;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(points[currentPosition].position);
        playerScript = FindObjectOfType<PlayerControllerWater>();
        #endregion
    }

    void Update()
    {
        #region Animator Bools And Triggers

        animator.SetBool("Scared", scared);

        #endregion

        if (!tarject)
        {
            tarject = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        if (scared == false)
        {
            //Cambio de posición entre puntos
            if (!agent.pathPending && agent.remainingDistance <= 0.1)
            {
                currentPosition = (currentPosition + 1) % points.Length;
                agent.SetDestination(points[currentPosition].position);
                Flip();
            }
        }
        else
        {
            Scared();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == ("Player") || collision.gameObject.tag == ("Net"))
       {
            if (detectVelocity < playerScript.speedMultiplier) //Detecta si el multiplicador de velocidad del player es mayor que su límite de detección, en tal caso, se cumple el if
            {
                scared = true;
            }
       }
    }

    void Scared()
    {
        //Huida
        distanceDifference = (transform.position - tarject.position).normalized;
        transform.Translate(distanceDifference * scaredVelocity * Time.deltaTime);
    }

    public void Flip()
    {
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void TriggerEvent() //Se triggerea al final de la animación del pez huyendo
    {
        Destroy(this.gameObject);
    }
}