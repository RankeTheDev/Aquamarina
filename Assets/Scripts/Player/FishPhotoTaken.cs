using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishPhotoTaken : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] FishName fishNames;

    // VARIABLES BOOL
    bool pezPayaso = false;
    bool caballitoDeMar = false;
    bool pezGlobo = false;
    bool pezCirujanoAzul = false;
    bool medusaLuna = false;
    bool anchoa = false;
    bool raya = false;
    bool pezCometa = false;
    bool atun = false;
    bool barracuda = false;
    bool delfin = false;
    bool tortugaBoba = false;
    bool tiburonBlanco = false;
    bool pezLuna = false;
    bool orca = false;
    bool angelMarino = false;
    bool pezHacha = false;
    bool pezFantasmaAzul = false;
    bool pezFantasmaNaranja = false;
    bool pezFantasmaRojo = false;
    bool pezFantasmaRosa = false;
    bool pezGema = false;
    bool pezFoco = false;
    bool gamba = false;
    bool pulpo = false;
    bool pezComecocos = false;
    bool calamar = false;
    bool pezqueleto = false;
    bool pezBala = false;
    bool pezLinterna = false;
    bool pezFosil = false;
    bool anguilaPelicano = false;
    bool pezGato = false;

    // VARIABLES BUTTONS
    [SerializeField] Button[] buttonsPecesRegistro;

    // VARIABLES IMAGE
    [SerializeField] Image[] imagesPecesRegistro;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        fishNames = FindObjectOfType<FishName>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //Detects if player gathers an air bubble
        if (trigger.gameObject.tag == "Fish")
        {
            fishNames = trigger.GetComponent<FishName>();
            switch (fishNames.fishName)
            {
                case "PezPayaso":
                    if (!pezPayaso)
                    {
                        pezPayaso = true;
                        buttonsPecesRegistro[0].enabled = true;
                        imagesPecesRegistro[0].color = Color.white;
                    }
                    break;

                case "CaballitoDeMar":
                    if (!caballitoDeMar)
                    {
                        caballitoDeMar = true;
                        buttonsPecesRegistro[1].enabled = true;
                        imagesPecesRegistro[1].color = Color.white;
                    }
                    break;

                case "PezGlobo":
                    if (!pezGlobo)
                    {
                        pezGlobo = true;
                        buttonsPecesRegistro[2].enabled = true;
                        imagesPecesRegistro[2].color = Color.white;
                    }
                    break;

                case "PezCirujanoAzul":
                    if (!pezCirujanoAzul)
                    {
                        pezCirujanoAzul = true;
                        buttonsPecesRegistro[3].enabled = true;
                        imagesPecesRegistro[3].color = Color.white;
                    }
                    break;

                case "Anchoa":
                    if (!anchoa)
                    {
                        anchoa = true;
                        buttonsPecesRegistro[4].enabled = true;
                        imagesPecesRegistro[4].color = Color.white;
                    }
                    break;

                case "MedusaLuna":
                    if (!medusaLuna)
                    {
                        medusaLuna = true;
                        buttonsPecesRegistro[5].enabled = true;
                        imagesPecesRegistro[5].color = Color.white;
                    }
                    break;

                case "Raya":
                    if (!raya)
                    {
                        raya = true;
                        buttonsPecesRegistro[6].enabled = true;
                        imagesPecesRegistro[6].color = Color.white;
                    }
                    break;

                case "PezCometa":
                    if (!pezCometa)
                    {
                        pezCometa = true;
                        buttonsPecesRegistro[7].enabled = true;
                        imagesPecesRegistro[7].color = Color.white;
                    }
                    break;

                case "Atun":
                    if (!atun)
                    {
                        atun = true;
                        buttonsPecesRegistro[8].enabled = true;
                        imagesPecesRegistro[8].color = Color.white;
                    }
                    break;

                case "Barracuda":
                    if (!barracuda)
                    {
                        barracuda = true;
                        buttonsPecesRegistro[9].enabled = true;
                        imagesPecesRegistro[9].color = Color.white;
                    }
                    break;

                case "Delfin":
                    if (!delfin)
                    {
                        delfin = true;
                        buttonsPecesRegistro[10].enabled = true;
                        imagesPecesRegistro[10].color = Color.white;
                    }
                    break;

                case "TortugaBoba":
                    if (!tortugaBoba)
                    {
                        tortugaBoba = true;
                        buttonsPecesRegistro[11].enabled = true;
                        imagesPecesRegistro[11].color = Color.white;
                    }
                    break;

                case "TiburonBlanco":
                    tiburonBlanco = true;
                    if (!tiburonBlanco)
                    {
                        tiburonBlanco = true;
                        buttonsPecesRegistro[12].enabled = true;
                        imagesPecesRegistro[12].color = Color.white;
                    }
                    break;

                case "PezLuna":
                    if (!pezLuna)
                    {
                        pezLuna = true;
                        buttonsPecesRegistro[13].enabled = true;
                        imagesPecesRegistro[13].color = Color.white;
                    }
                    break;

                case "Orca":
                    if (!orca)
                    {
                        orca = true;
                        buttonsPecesRegistro[14].enabled = true;
                        imagesPecesRegistro[14].color = Color.white;
                    }
                    break;

                case "AngelMarino":
                    if (!angelMarino)
                    {
                        angelMarino = true;
                        buttonsPecesRegistro[15].enabled = true;
                        imagesPecesRegistro[15].color = Color.white;
                    }
                    break;

                case "PezHacha":
                    if (!pezHacha)
                    {
                        pezHacha = true;
                        buttonsPecesRegistro[16].enabled = true;
                        imagesPecesRegistro[16].color = Color.white;
                    }
                    break;

                case "PezFantasmaAzul":
                    if (!pezFantasmaAzul)
                    {
                        pezFantasmaAzul = true;
                        buttonsPecesRegistro[17].enabled = true;
                        imagesPecesRegistro[17].color = Color.white;
                    }
                    break;

                case "PezFantasmaNaranja":
                    if (!pezFantasmaNaranja)
                    {
                        pezFantasmaNaranja = true;
                        buttonsPecesRegistro[18].enabled = true;
                        imagesPecesRegistro[18].color = Color.white;
                    }
                    break;

                case "PezFantasmaRojo":
                    if (!pezFantasmaRojo)
                    {
                        pezFantasmaRojo = true;
                        buttonsPecesRegistro[19].enabled = true;
                        imagesPecesRegistro[19].color = Color.white;
                    }
                    break;

                case "PezFantasmaRosa":
                    if (!pezFantasmaRosa)
                    {
                        pezFantasmaRosa = true;
                        buttonsPecesRegistro[20].enabled = true;
                        imagesPecesRegistro[20].color = Color.white;
                    }
                    break;

                case "Gamba":
                    if (!gamba)
                    {
                        gamba = true;
                        buttonsPecesRegistro[21].enabled = true;
                        imagesPecesRegistro[21].color = Color.white;
                    }
                    break;

                case "PezFoco":
                    if (!pezFoco)
                    {
                        pezFoco = true;
                        buttonsPecesRegistro[22].enabled = true;
                        imagesPecesRegistro[22].color = Color.white;
                    }
                    break;

                case "Pulpo":
                    if (!pulpo)
                    {
                        pulpo = true;
                        buttonsPecesRegistro[23].enabled = true;
                        imagesPecesRegistro[23].color = Color.white;
                    }
                    break;

                case "Calamar":
                    if (!calamar)
                    {
                        calamar = true;
                        buttonsPecesRegistro[24].enabled = true;
                        imagesPecesRegistro[24].color = Color.white;
                    }
                    break;

                case "PezComecocos":
                    if (!pezComecocos)
                    {
                        pezComecocos = true;
                        buttonsPecesRegistro[25].enabled = true;
                        imagesPecesRegistro[25].color = Color.white;
                    }
                    break;

                case "AnguilaPelicano":
                    if (!anguilaPelicano)
                    {
                        anguilaPelicano = true;
                        buttonsPecesRegistro[25].enabled = true;
                        imagesPecesRegistro[25].color = Color.white;
                    }
                    break;

                case "PezBala":
                    if (!pezBala)
                    {
                        pezBala = true;
                        buttonsPecesRegistro[26].enabled = true;
                        imagesPecesRegistro[26].color = Color.white;
                    }
                    break;

                case "PezLinterna":
                    if (!pezLinterna)
                    {
                        pezLinterna = true;
                        buttonsPecesRegistro[27].enabled = true;
                        imagesPecesRegistro[27].color = Color.white;
                    }
                    break;

                case "PezGato":
                    if (!pezGato)
                    {
                        pezGato = true;
                        buttonsPecesRegistro[28].enabled = true;
                        imagesPecesRegistro[28].color = Color.white;
                    }
                    break;

                case "PezGema":
                    if (!pezGema)
                    {
                        pezGema = true;
                        buttonsPecesRegistro[29].enabled = true;
                        imagesPecesRegistro[29].color = Color.white;
                    }
                    break;

                case "Pezqueleto":
                    if (!pezqueleto)
                    {
                        pezqueleto = true;
                        buttonsPecesRegistro[30].enabled = true;
                        imagesPecesRegistro[30].color = Color.white;
                    }
                    break;

                case "PezFosil":
                    if (!pezFosil)
                    {
                        pezFosil = true;
                        buttonsPecesRegistro[31].enabled = true;
                        imagesPecesRegistro[31].color = Color.white;
                    }
                    break;
            }
        }
        #endregion
    }
}
