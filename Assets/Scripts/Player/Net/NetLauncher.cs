using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetLauncher : MonoBehaviour
{
    #region VARIABLES
    public GameObject netPrefab;
	public GameObject shooter;

	[SerializeField] private Transform _firePoint;
    #endregion

    #region METHODS
    void Awake()
	{
		_firePoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void Shoot()
	{
		if (netPrefab && _firePoint && shooter) 
		{
            GameObject myNet = Instantiate(netPrefab, _firePoint.position, Quaternion.identity) as GameObject;

            Net netComponent = myNet.GetComponent<Net>();

            //LIMITA A QUE LADO PUEDES DISPARAR
			if (shooter.transform.localScale.x > 0f) {
				// Left
				netComponent.direction = Vector2.left; // new Vector2(-1f, 0f)
			} else {
                // Right
                netComponent.direction = Vector2.right; // new Vector2(1f, 0f)
			}
        }
	}
    #endregion
}