using UnityEngine;

public class PulsaCualquierBoton : MonoBehaviour
{
   
    public GameObject pantallaInicio;  
    public GameObject pantallaMenu; 

    void Start()
    {
        
        pantallaInicio.SetActive(true);   
        pantallaMenu.SetActive(false); 
    }

    void Update()
    {
        
        if (pantallaInicio.activeSelf)
        {
            
            if (Input.anyKeyDown)
            {
                Empezar();
            }
        }
    }

    void Empezar()
    {
        
        pantallaInicio.SetActive(false); 
        pantallaMenu.SetActive(true); 
    }
}