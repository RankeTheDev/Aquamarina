using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Carga librerias gestión de escenas

public class CambioEscenAnimatica : MonoBehaviour
{
    public float delay = 38f; //delay para cambiar solo de escena. 37 dura la animatica
    public int escenaACambiar = 1; //A que escena voy

    // Start is called before the first frame update
    public void Start()
    {
        //Invoke("IniciarCarga", delay); // Ejecuta la función IniciarCarga pasados 10 segundos
    }


    public void IniciarCarga()
    {
        Debug.Log("Pre invoke");
        Invoke("ChangeScene", delay); // Ejecuta la función IniciarCarga pasados 10 segundos
        Debug.Log("Post invoke");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(escenaACambiar); //Carga una escena
    }
}
