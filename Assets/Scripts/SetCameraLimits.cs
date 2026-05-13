using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetpolygonColliders : MonoBehaviour
{
    #region VARIABLES
    int sceneIndex;
    Scene currentScene;
    [SerializeField] PolygonCollider2D[] polygonColliders;

    [SerializeField] CinemachineConfiner vCamConfinerComponent;
    [SerializeField] PolygonCollider2D vCamConfinerCollider;
    #endregion

    #region METHODS
    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene(); //Obtiene la escena actual
        sceneIndex = currentScene.buildIndex; //Obtiene el indice de la escena actual (0 es la terrestre)

        SetCollider2D();
    }

    void SetCollider2D()
    {
        switch (sceneIndex)
        {
            case 1: //Si es la escena 1
                /*for (int i = 0; i <= polygonColliders.Length; i++) //Cicla por todos los colliders dejando activo solo el indicado. Por precaucion.
                {
                    if (i == 0) //Deja activo el collider de la scene 1
                    {
                        polygonColliders[i].enabled = true;
                    }
                    else //Desactiva los demas colliders
                    {
                        polygonColliders[i].enabled = false;
                    }
                }*/
                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[0];
                break;
            case 2: //Si es la escena 2
                /*for (int i = 0; i <= polygonColliders.Length; i++) //Cicla por todos los colliders dejando activo solo el indicado. Por precaucion.
                {
                    if (i == 1) //Deja activo el collider de la scene 2
                    {
                        polygonColliders[i].enabled = true;
                    }
                    else //Desactiva los demas colliders
                    {
                        polygonColliders[i].enabled = false;
                    }
                }*/
                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[1];
                break;
            case 3: //Si es la escena 3
                /*for (int i = 0; i <= polygonColliders.Length; i++) //Cicla por todos los colliders dejando activo solo el indicado. Por precaucion.
                {
                    if (i == 2) //Deja activo el collider de la scene 3
                    {
                        polygonColliders[i].enabled = true;
                    }
                    else //Desactiva los demas colliders
                    {
                        polygonColliders[i].enabled = false;
                    }
                }*/
                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[2];
                break;
            case 4: //Si es la escena 4
                /*for (int i = 0; i <= polygonColliders.Length; i++) //Cicla por todos los colliders dejando activo solo el indicado. Por precaucion.
                {
                    if (i == 3) //Deja activo el collider de la scene 4
                    {
                        polygonColliders[i].enabled = true;
                    }
                    else //Desactiva los demas colliders
                    {
                        polygonColliders[i].enabled = false;
                    }
                }*/
                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[3];
                break;
            case 5: //Si es la escena 5
                /*for (int i = 0; i <= polygonColliders.Length; i++) //Cicla por todos los colliders dejando activo solo el indicado. Por precaucion.
                {
                    if (i == 4) //Deja activo el collider de la scene 5
                    {
                        polygonColliders[i].enabled = true;
                    }
                    else //Desactiva los demas colliders
                    {
                        polygonColliders[i].enabled = false;
                    }
                }*/
                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[4];
                break;
        }
    }
    #endregion
}
