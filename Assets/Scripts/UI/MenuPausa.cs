using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    InputAction actionAjustes;

    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuAjustes; // Añade esta referencia
    [SerializeField] private GameObject botonPausa;

    public bool juegoPausado = false;
    public bool ajustesAbiertos = false;

    [SerializeField] PlayerController_PDA playerControllerPDA;

    void Start() ///GUARDO PREFERENCIAS
    {
        actionAjustes = InputSystem.actions.FindAction("Ajustes");

        if (!playerControllerPDA)
        {
            playerControllerPDA = FindObjectOfType<PlayerController_PDA>();
        }
    }

    private void Update()
    {
        if (actionAjustes.WasPressedThisFrame())
        {
            // Si los ajustes están abiertos, cerrarlos
            if (ajustesAbiertos)
            {
                CerrarAjustes();
            }
            // Si no, toggle del menú de pausa
            else if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        menuAjustes.SetActive(false);
        ajustesAbiertos = false;
        playerControllerPDA.menuAjustesActivated = false;
    }

    // Método para abrir ajustes (llámalo desde el botón de ajustes)
    public void AbrirAjustes()
    {
        ajustesAbiertos = true;
        menuPausa.SetActive(false);
        menuAjustes.SetActive(true);
    }

    // Método para cerrar ajustes y volver al menú de pausa
    public void CerrarAjustes()
    {
        ajustesAbiertos = false;
        menuAjustes.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reiniciar()
    {
        Reanudar();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir(string NombreMenu)
    {
        Time.timeScale = 1f; // Importante: restaurar el tiempo
        SceneManager.LoadScene(NombreMenu);
    }
}