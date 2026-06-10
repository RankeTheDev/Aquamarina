using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnEnable : MonoBehaviour
{
    public float delay; //delay para cambiar solo de escena. 23 segundos los creditos
    public int escenaACambiar; //A que escena voy

    // Start is called before the first frame update
    public void OnEnable()
    {
        Invoke("IniciarCarga", delay); // Ejecuta la función IniciarCarga pasados 10 segundos
    }


    public void IniciarCarga()
    {
        //SceneManager.LoadScene(escenaACambiar); //Carga una escena
        Debug.Log("Kaput");
        Application.Quit();
    }
}
