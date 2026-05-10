using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] string itemName;
    [SerializeField] int quantity;
    [SerializeField] Sprite sprite; //SPRITE QUE SE MOSTRARA EN EL INVENTARIO

    [SerializeField] InventoryManager inventoryManager;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryManager)
        {
            inventoryManager = FindAnyObjectByType<InventoryManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }

    #endregion
}
