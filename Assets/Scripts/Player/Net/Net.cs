using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Net : MonoBehaviour
{
	public int damage = 1;
	public float speed = 2f;
	public Vector2 direction;

	public float livingTime = 3f;
	public Color initialColor = Color.white;
	public Color finalColor;

	private SpriteRenderer _renderer;
	private Rigidbody2D _rigidbody;
	private float _startingTime;

	void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
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
		// Change bullet's color over time
		float _timeSinceStarted = Time.time - _startingTime;
		float _percentageCompleted = _timeSinceStarted / livingTime;

		_renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);

		if (_percentageCompleted >= 1f) {
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
		Destroy(gameObject, 1.5f);
	}
}
