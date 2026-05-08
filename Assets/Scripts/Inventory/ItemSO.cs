using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    #region VARIABLES
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public float ammountTOChangeStat;
    [SerializeField] Timer timer;

    #endregion

    #region METHODS
    void Update()
    {
        if (timer != null)
        {
            timer = FindObjectOfType<Timer>();
            Debug.Log("Timer found");
        }
        else 
        {
            Debug.Log("FAH");
        }

    }

    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            GameObject.Find("Player").GetComponent<Timer>().AddTime(ammountTOChangeStat);
            Debug.Log("Se añadieron " + ammountTOChangeStat + " puntos de vida");
        }
    }

    public enum StatToChange
    {
        none,
        health
    };

    #endregion
}
