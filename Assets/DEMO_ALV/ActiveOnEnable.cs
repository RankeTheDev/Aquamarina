using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnEnable : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Activa", 0.1f);
    }

    // Update is called once per frame
    void Activa()
    {
        this.gameObject.SetActive(false);
    }
}
