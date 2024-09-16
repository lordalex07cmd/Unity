using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempoLimite : MonoBehaviour
{
    [SerializeField] float TempoLimiteEmSegundos = 120;
    [SerializeField] Text txt_TempoLimite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TempoLimiteEmSegundos -= Time.deltaTime; //para de contar qdo abre o menu , o unsscaleDeltatime permite contar mesmo com o menu aberto
        if(TempoLimiteEmSegundos<=0 )
        {
            SistemaMensagem.instance.MostrarMensagem("Ja nao tem mais tempo para acabar o nivel !");
            Invoke(nameof(CarregarMenuPrincipal), 3);

        }
        if(txt_TempoLimite!=null)
        {
            int minutos = (int)TempoLimiteEmSegundos / 60;
            int segundos = (int)TempoLimiteEmSegundos - (minutos*60);
            txt_TempoLimite.text = $"{minutos}:{segundos.ToString("00")}";
        }
                
    }
    void CarregarMenuPrincipal()
    {
        SceneManager.LoadScene("menuprincipal");
    }
}
