using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractMudaNivel : MonoBehaviour, IInteract
{
    [SerializeField] string ItemNecessario = "";
    Inventario inventario;

    public void Acao()
    {
        if (ItemNecessario != "")
        {
            if (inventario != null && inventario.Existe(ItemNecessario) == false)
            {
                SistemaMensagem.instance.MostrarMensagem($"falta {ItemNecessario}");
                return;
            }
        }
        //mudar de cena
        if(SceneManager.GetActiveScene().buildIndex==SceneManager.sceneCountInBuildSettings-1)
        {
            SistemaMensagem.instance.MostrarMensagem("Acabou o jogo , Parabéns.");
            Invoke(nameof(MudaCenaPrincipal), 3);

        }
        else
        {
            SistemaMensagem.instance.MostrarMensagem("Vamos Continuar Nossa Aventura!");
            Invoke(nameof(MudaCenaSeguinte), 3);

        }
    }
    public void MudaCenaSeguinte()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
    public void MudaCenaPrincipal()
    {
        SceneManager.LoadScene("menuprincipal");
    }
    // Start is called before the first frame update
    void Start()
    {
        inventario=FindObjectOfType<Inventario>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
