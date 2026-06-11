using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] FishName fish; //Nombre del fish
    [SerializeField] FishCapture fishCapture; //Ref al script de captura de pez
    [SerializeField] int quantity = 1; //Cantidad de peces que añadir
    [SerializeField] Sprite sprite; //SPRITE QUE SE MOSTRARA EN EL INVENTARIO
    [SerializeField] InventoryManager inventoryManager; //Ref al script de inventory Manager
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        // COJO LAS REFERENCIAS DE COMPONENTES Y SCRIPTS
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        fish = GetComponent<FishName>();
        fishCapture = GetComponent<FishCapture>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryManager) // Si no tiene asignado el inventory manager, busca uno e intenta asignarlo
        {
            inventoryManager = FindAnyObjectByType<InventoryManager>();
        }
    }

    public void AddItem() //Metodo para añadir el pez al inventario
    {
        //Calcula si se van a añadir mas peces del cupo de un slot de inventario
        int leftOverItems = inventoryManager.AddItem(fish.fishName, quantity, sprite); 
        if (leftOverItems <= 0)
        {
            Destroy(gameObject); //Destruye el pez tras añadirlo
        }
        else
        {
            quantity = leftOverItems; //Guarda la cantidad de peces sobrantes
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) //Deteccion de colision con el player. Si el bool listado es verdadero y el pez colisiona con el player, se añade el item al inventario
    {
        if (trigger.gameObject.tag == "Player" && fishCapture.moveToPlayer)
        {
            Debug.Log("Pez en fase de captura");
            AddItem();
        }
    }

    private void OnTriggerStay2D(Collider2D trigger) //Deteccion de colision con el player. Si el bool listado es verdadero y el pez colisiona con el player, se añade el item al inventario
    {
        if (trigger.gameObject.tag == "Player" && fishCapture.moveToPlayer)
        {
            Debug.Log("Pez en fase de captura");
            AddItem();
        }
    }
    #endregion
}
