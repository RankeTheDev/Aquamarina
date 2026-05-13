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
            case 1: //Si es la escena 1 deja activo el collider de la scene 1 y Desactiva los demas colliders
                polygonColliders[0].enabled = true;
                polygonColliders[1].enabled = false;
                polygonColliders[2].enabled = false;
                polygonColliders[3].enabled = false;
                polygonColliders[4].enabled = false;

                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[0]; //Asigna el collider correcto para la escena como camLimits
                break;
            case 2: //Si es la escena 2 deja activo el collider de la scene 2 y Desactiva los demas colliders
                polygonColliders[0].enabled = false;
                polygonColliders[1].enabled = true;
                polygonColliders[2].enabled = false;
                polygonColliders[3].enabled = false;
                polygonColliders[4].enabled = false;

                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[1]; //Asigna el collider correcto para la escena como camLimits
                break;
            case 3: //Si es la escena 3 deja activo el collider de la scene 3 y Desactiva los demas colliders
                polygonColliders[0].enabled = false;
                polygonColliders[1].enabled = false;
                polygonColliders[2].enabled = true;
                polygonColliders[3].enabled = false;
                polygonColliders[4].enabled = false;

                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[2]; //Asigna el collider correcto para la escena como camLimits
                break;
            case 4: //Si es la escena 4 deja activo el collider de la scene 4 y Desactiva los demas colliders
                polygonColliders[0].enabled = false;
                polygonColliders[1].enabled = false;
                polygonColliders[2].enabled = false;
                polygonColliders[3].enabled = true;
                polygonColliders[4].enabled = false;

                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[3]; //Asigna el collider correcto para la escena como camLimits
                break;
            case 5: //Si es la escena 5 deja activo el collider de la scene 5 y Desactiva los demas colliders
                polygonColliders[0].enabled = false;
                polygonColliders[1].enabled = false;
                polygonColliders[2].enabled = false;
                polygonColliders[3].enabled = false;
                polygonColliders[4].enabled = true;

                vCamConfinerComponent.m_BoundingShape2D = polygonColliders[4]; //Asigna el collider correcto para la escena como camLimits
                break;
        }
    }
    #endregion
}
