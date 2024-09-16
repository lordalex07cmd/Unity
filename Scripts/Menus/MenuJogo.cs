using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJogo : MonoBehaviour
{
    public GameObject PanelMenuJogo;
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect(); //liberta memoria


        GuardarNivel();
        _player=FindObjectOfType<PlayerMoviment>().gameObject;
        ContinuarJogo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (PanelMenuJogo.activeSelf)
            {
                ContinuarJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }
    public void ContinuarJogo()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PanelMenuJogo.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PausarJogo()
    { 
        Cursor.lockState = CursorLockMode.None;
        PanelMenuJogo.SetActive(true);
        Time.timeScale = 0;
    }
    public void Terminar()
    {
        FadeController.instance.FadeOut();
        Time.timeScale = 1;
        Invoke(nameof(CarregarMenuPrincipal), 4);
    }
    void CarregarMenuPrincipal()
    { 
        SceneManager.LoadScene("menuprincipal");    
        
    }
    public void GuardarNivel()
    { 
        int indice=SceneManager.GetActiveScene().buildIndex;
        int indiceGuardado = PlayerPrefs.GetInt("indice", 0);
        if (indiceGuardado < indice)
        {
            PlayerPrefs.SetInt("nivel", indice);
            PlayerPrefs.Save();
        }
    }
}

