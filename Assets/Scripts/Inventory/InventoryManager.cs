using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    #region VARIABLES
    public ItemSlot[] itemSlots;
    public ItemSO[] itemSOs;
    public AudioManager audioManager;
    #endregion

    #region METHODS
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public bool UseItem(string itemName)  //Usar un Item
    {
        for (int i = 0; i < itemSOs.Length; i++)
        { 
            if(itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;

    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite) //Metodo para añadir items (considerando la posibilidad de sobrepasar el maximo de un slot, añadiendo sobrante al siguiente)
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
        switch (itemName)
        {
            case "O2 Bottle":
                audioManager.PlaySFX(audioManager.BombonaOxigeno);
                Debug.Log("Suena mostro");
                break;
            case "MenaHierro":
                audioManager.PlaySFX(audioManager.Plastico);
                break;
            case "Cobre":
                audioManager.PlaySFX(audioManager.Cobre);
                Debug.Log("Suena mostro");
                break;
            case "Hierro":
                audioManager.PlaySFX(audioManager.Hierro);
                break;
            case "Cristal":
                audioManager.PlaySFX(audioManager.Cristal);
                break;
            case "Burbuja":
                audioManager.PlaySFX(audioManager.Oxigeno);
                break;
            
        }
        return quantity;
    }

    public void DeselectAllSlots() //Desseleciona todos los slots
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisItemSelected = false;
        }
    }
    #endregion
}
