using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] string itemName; //Nombre del item
    [SerializeField] int quantity; //Cantidad a añadir del item
    [SerializeField] Sprite sprite; //SPRITE QUE SE MOSTRARA EN EL INVENTARIO
    [SerializeField] InventoryManager inventoryManager;
    public AudioManager audioManager;
    #endregion

    #region METHODS
    void Start() //Guardo referencias al script de inventory manager
    {
        audioManager = FindObjectOfType<AudioManager>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryManager) //Guardo referencias al script de inventory manager si no lo tiene
        {
            inventoryManager = FindAnyObjectByType<InventoryManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) //Si colisiona con el player, calcula los items que hacen overflow de un slot del inventario y los añade al siguiente slot libre
    {
        if(trigger.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject); //Destruye el item
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }
    #endregion
}
