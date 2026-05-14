using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Net : MonoBehaviour
{
	public int damage = 1;
	public float speed = 2f;
	public Vector2 direction;

	public float livingTime = 2f;
	public Color initialColor = Color.white;
	public Color finalColor;

	private SpriteRenderer _renderer;
	private Rigidbody2D _rigidbody;
	private float _startingTime;
	Animator animator;
	[SerializeField] GameObject player;
	[SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] NetLauncherFollowMouse netLauncherFollowMouse;

    void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
        netLauncherFollowMouse = FindObjectOfType<NetLauncherFollowMouse>();
        animator = GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
		spriteRenderer = GetComponent<SpriteRenderer>();
    }

	// Start is called before the first frame update
	void Start()
    {
		//  Save initial time
		_startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if (player.GetComponent<Transform>().localScale.x == -1)
		{
            spriteRenderer.flipX = true;
        }
		else if (player.GetComponent<Transform>().localScale.x == 1)
		{
            spriteRenderer.flipX = false;
        }

			//Almacena position de la net
			Vector2 netPosition = transform.position;

        if (netPosition != netLauncherFollowMouse.GetWorldPositionFromMouse()) //Si la posicion de la net difiere de la del objetivo, sigue moviendose hacia este
		{
			transform.position = Vector2.MoveTowards(transform.position, netLauncherFollowMouse.GetWorldPositionFromMouse(), speed * Time.deltaTime);
		}

        // Change bullet's color over time
        float _timeSinceStarted = Time.time - _startingTime;
		float _percentageCompleted = _timeSinceStarted / livingTime;
		_renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
		
		//Si el tiempo de vida de la bala se agota, desaparece
		if (_percentageCompleted >= 1f) 
		{
            Vanish(); 
		}
	}

	private void FixedUpdate() //Movimiento de la net
	{
		//  Move object
		Vector2 movement = direction.normalized * speed;
		_rigidbody.velocity = movement;
	}

    /*private void OnTriggerEnter2D(Collider2D trigger) //AÑADIR PEZ DIRECTAMENTE AL INVENTARIO
	{
        if (trigger.gameObject.tag == "Fish")
        {
			trigger.GetComponent<Fish>().AddItem();
			Vanish();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D trigger) //DEteccion de colision con el pez y llamada a su metodo de captura
	{
        if (trigger.gameObject.tag == "Fish")
        {
            trigger.GetComponent<FishCapture>().Captured();
            Vanish();
        }
    }

    public void Vanish() //Eliminacion paulatina de la red
	{
		speed = 0f;
        _renderer.enabled = false;
		Destroy(this.gameObject, 1f);
	}
}
