using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubble : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] Timer timer;
    [SerializeField] PlayerController_Equipment playerEquipment;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        playerEquipment = FindObjectOfType<PlayerController_Equipment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer || !playerEquipment)
        {
            timer = FindObjectOfType<Timer>();
            playerEquipment = FindObjectOfType<PlayerController_Equipment>();
        }
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //Detects if player gathers an air bubble
        if (trigger.gameObject.tag == ("Player") && !playerEquipment.cameraEquipped)
        {
            timer.currentTime += timer.addAir;
            Debug.Log("O2 añadido");
            Destroy(this.gameObject);
        }
    }
    #endregion
}
