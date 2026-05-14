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

    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            Timer timer = GameObject.Find("Player").GetComponent<Timer>();
            if (timer.currentTime == timer.totalTime)
            {
                return false;
            }
            else
            {
                timer.AddTime(ammountTOChangeStat);

                Debug.Log("Se añadieron " + ammountTOChangeStat + " puntos de vida");
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health
    };

    #endregion
}
