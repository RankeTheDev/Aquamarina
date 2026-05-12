using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] FishName fish;
    [SerializeField] FishCapture fishCapture;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite; //SPRITE QUE SE MOSTRARA EN EL INVENTARIO
    [SerializeField] InventoryManager inventoryManager;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        fish = GetComponent<FishName>();
        fishCapture = GetComponent<FishCapture>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryManager)
        {
            inventoryManager = FindAnyObjectByType<InventoryManager>();
        }
    }

    public void AddItem()
    {
        int leftOverItems = inventoryManager.AddItem(fish.fishName, quantity, sprite);
        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player" && fishCapture.moveToPlayer)
        {
            AddItem();
        }
    }
    #endregion
}
