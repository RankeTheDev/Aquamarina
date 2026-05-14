using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class AtackCorrutine : MonoBehaviour
{
    #region variables
    int speed;
    public Transform[] points;
    NavMeshAgent agent;
    private int currentPosition = 0;
    public Transform tarject; //tarject es el Player
    public Rigidbody2D tarjectRigidbody;
    public bool agresive = false;
    public Vector2 distanceDirecction;
    [SerializeField] float distanceDifference;
    public float agresiveVelocity; //Velocidad en la que se mueve cuendo se asusta
    public float detectVelocity; //Velocidad límite para detectar al Player
    public PlayerControllerWater playerScript;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Vector3 tarjectPosition;
    [SerializeField] Vector2 tarjectPositionAlways;
    #endregion

    #region variables no usadas
    /*public float alfa;
      public float alfaMax = 1f;
      public float alfaMin = 0f;
      public float tiempoDesaparición = 1f;*/
    #endregion

    private Animator animator;

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

        animator.SetBool("Scared", agresive);

        #endregion

        StartCoroutine(AtackRutine());

        if (!playerScript)
        {
           playerScript = FindObjectOfType<PlayerControllerWater>();
        }

        if (!tarject)
        {
            tarject = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        tarjectPositionAlways = tarject.transform.position;
    }

    IEnumerator AtackRutine()
    {
            if (agresive == false)
            {
                agent.isStopped = false;

                //Cambio de posición entre puntos
                if (!agent.pathPending && agent.remainingDistance <= 0.1)
                {
                    currentPosition = (currentPosition + 1) % points.Length;
                    agent.SetDestination(points[currentPosition].position);
                    Flip();

                }
                yield return null;
            }

            else
            {
                agent.isStopped = true;

                //Jesus, danos un 10 porfaporfi. No se lo digo a Dani porque me manda a la mierda.

                distanceDifference = Vector2.Distance(transform.position, tarjectPosition);

                Vector2 distanceDirecction = (transform.position - tarjectPosition).normalized;
                Debug.Log("ÑomÑom");
            /*
                if (distanceDifference > 0.1)
                {
                    if (transform.localScale.x > 0.1)
                    {
                        Flip();
                    }

                }

                else if (distanceDifference < 0.1)
                {
                    if (transform.localScale.x < 0.1)
                    {
                        Flip();
                    }
                }
            */
            transform.Translate(distanceDirecction * -1f * agresiveVelocity * Time.deltaTime);

                if(distanceDifference < 0.4) //Deja de ser agresivo si es casi la distancia al player
                {
                    agresive = false;
                }
            }
        
    }


    public void Flip()
    {
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Detecta si el multiplicador de velocidad del player es mayor que su límite de detección, en tal caso   , se cumple el if
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            if (detectVelocity < playerScript.speedMultiplier)
            {
                Debug.Log("Collision");
                tarjectPosition = tarject.transform.position;
                agresive = true;
            }
        }
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            agresive = false;
        }
    }
    */
}



