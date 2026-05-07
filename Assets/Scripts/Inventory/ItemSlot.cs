using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //ITEM DATA
    public string itemName;
    public int quantity;
    [SerializeField] int maxNumberOfItems;
    [SerializeField] Sprite itemSprite;
    public bool isFull;

    //ITEM SLOT
    [SerializeField] TMP_Text quantityText;
    [SerializeField] Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;
    [SerializeField] InventoryManager inventoryManager;
    #endregion

    #region METHODS
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        //Check if the slot is already full
        if (isFull)
        {
            return quantity;
        }

        //Update NAME
        this.itemName = itemName;

        //Update IMAGE
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //Update QUANTITY
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //Return the LEFTOVERS
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //Update QUANTITY TEXT
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    { 
        inventoryManager.DeselectAllSlots();
        
        selectedShader.SetActive(true);
        thisItemSelected = true;
    }

    public void OnRightClick()
    { 
    
    }
    #endregion
}
