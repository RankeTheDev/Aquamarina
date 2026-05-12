using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Net : MonoBehaviour
{
	public int damage = 1;
	public float speed = 3f;
	public Vector2 direction;

	public float livingTime = 3f;
	public Color initialColor = Color.white;
	public Color finalColor;

	private SpriteRenderer _renderer;
	private Rigidbody2D _rigidbody;
	private float _startingTime;

    [SerializeField] NetLauncherFollowMouse netLauncherFollowMouse;

    void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
        netLauncherFollowMouse = FindObjectOfType<NetLauncherFollowMouse>();
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
		Vector2 netPosition = transform.position;

        if (netPosition != netLauncherFollowMouse.GetWorldPositionFromMouse())
		{
			transform.position = Vector2.MoveTowards(transform.position, netLauncherFollowMouse.GetWorldPositionFromMouse(), speed * Time.deltaTime);
		}

        // Change bullet's color over time
        float _timeSinceStarted = Time.time - _startingTime;
		float _percentageCompleted = _timeSinceStarted / livingTime;

		_renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);

		if (_percentageCompleted >= 1f) 
		{
            Vanish(); 
		}
	}

	private void FixedUpdate()
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

    private void OnTriggerEnter2D(Collider2D trigger)
	{
        if (trigger.gameObject.tag == "Fish")
        {
			trigger.GetComponent<FishCapture>().Captured();
			Vanish();
        }
    }

	public void Vanish()
	{
		speed = 0f;
		_renderer.enabled = false;
		Destroy(this.gameObject);
	}
}
