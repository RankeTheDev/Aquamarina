using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    #region VARIABLES
    public GameObject inventoryMenu;
    bool menuActivated;
    public ItemSlot[] itemSlots;

    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;

    InputAction actionInventory;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionInventory = InputSystem.actions.FindAction("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        OpenCloseInventory();
    }

    void OpenCloseInventory()
    {
        if (actionInventory.WasPressedThisFrame() && menuActivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (actionInventory.WasPressedThisFrame() && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        //Debug.Log("itemName = " + itemName + ", quantity = " + quantity + ", itemSprite = " + itemSprite);
        for (int i = 0; i < itemSlots.Length; i++) //CIclo por todas las casillas del inventario
        {
            if (itemSlots[i].isFull == false && itemSlots[i].itemName == itemName || itemSlots[i].quantity == 0)
            { 
                int leftOverItems = itemSlots[i].AddItem(itemName, quantity, itemSprite);
                if (leftOverItems > 0) 
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite);
                }
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisItemSelected = false;
        }
    }
}
