using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    private void Awake()
    {
        MenuConfigurar.CarregarConfiguracao();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void Comecar()
    {
        FadeController.instance.FadeOut();
        Destroy(FindObjectOfType<MusicaController>().gameObject);
        Invoke(nameof(CarregarComecar), 3);
    }
    
        void CarregarComecar()
    {
        SceneManager.LoadScene("nivel1");
    }
    
    public void Continuar()
    {
        //FadeController.instance.FadeOut();
        //Invoke(nameof(Continuar), 3);
        Destroy(FindObjectOfType<MusicaController>().gameObject);
        int indiceGuardado = PlayerPrefs.GetInt("indice", 0);
        if (indiceGuardado == 0)
            Comecar();
        else
            SceneManager.LoadScene(indiceGuardado);
           
    }
    public void Configurar()
    {
        //FadeController.instance.FadeOut();
        //Invoke(nameof(Configurar), 3);
        SceneManager.LoadScene("configurar");
    }
    public void Sobre()
    {
        //FadeController.instance.FadeOut();
        //Invoke(nameof(Configurar), 3);
        SceneManager.LoadScene("Sobre");
    }
    public void Sair()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
