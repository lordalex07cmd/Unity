using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaMensagem : MonoBehaviour
{
    TMP_Text txt_mensagem;


    //singleton so uma instancia no projecto
    public static SistemaMensagem instance;

    private void Awake()
    {
       if (instance!=null)
        {
            Destroy(gameObject);
            return;
            
        }
        instance = this;
    }   
    // Start is called before the first frame update
    void Start()
    {
       txt_mensagem=GetComponent<TMP_Text>();
        EsconderMensagem();
    }
    public void MostrarMensagem(string texto,float duracao=4)
    {
        txt_mensagem.enabled = true;
        txt_mensagem.text = texto;
        Invoke(nameof(EsconderMensagem), duracao);
    }

    void EsconderMensagem()
    {
        txt_mensagem.enabled = false;
        txt_mensagem.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
