using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineZoneDiscovered : MonoBehaviour
{
    #region VARIABLES
    //VARIABLES DE COMPROBACION DE ZONAS DESCUBIERTAS
    public bool zone0Discovered = true;
    public bool zone1Discovered = false;
    public bool zone2Discovered = false;
    public bool zone3Discovered = false;
    public bool zone4Discovered = false;
    public bool zone5Discovered = false;

    //VARIABLES DE ACTIVACION DE TRIGGERS DE DESCUBRIR ZONAS
    [SerializeField] Collider2D zone2Discover;
    [SerializeField] Collider2D zone3Discover;
    [SerializeField] Collider2D zone4Discover;
    [SerializeField] Collider2D zone5Discover;

    //VARIABLES DE SCRIPTS Y COMPONENTES
    [SerializeField] PlayerController_SceneTypeChecker sceneTypeChecker;
    #endregion

    #region METHODS
    // Awake
    void Awake()
    {
        sceneTypeChecker = GetComponent<PlayerController_SceneTypeChecker>();

        zone2Discover = GameObject.FindWithTag("SubmarineZone2").GetComponent<Collider2D>();
        zone3Discover = GameObject.FindWithTag("SubmarineZone3").GetComponent<Collider2D>();
        zone4Discover = GameObject.FindWithTag("SubmarineZone4").GetComponent<Collider2D>();
        zone5Discover = GameObject.FindWithTag("SubmarineZone5").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SubmarineDiscoverZoneTriggers();
    }

    void SubmarineDiscoverZoneTriggers()
    {
        switch (sceneTypeChecker.sceneIndex)
        {
            case 2:
                zone2Discover.enabled = true;
                zone3Discover.enabled = false;
                zone4Discover.enabled = false;
                zone5Discover.enabled = false;
                break;
            case 3:
                zone2Discover.enabled = false;
                zone3Discover.enabled = true;
                zone4Discover.enabled = false;
                zone5Discover.enabled = false;
                break;
            case 4:
                zone2Discover.enabled = false;
                zone3Discover.enabled = false;
                zone4Discover.enabled = true;
                zone5Discover.enabled = false;
                break;
            case 5:
                zone2Discover.enabled = false;
                zone3Discover.enabled = false;
                zone4Discover.enabled = false;
                zone5Discover.enabled = true;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == ("SubmarineZone1"))
        {
            zone1Discovered = true;
        }

        if (trigger.gameObject.tag == ("SubmarineZone2"))
        {
            zone2Discovered = true;
        }
    }
    #endregion
}
