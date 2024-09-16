using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud_vida : MonoBehaviour
{
    TMP_Text txt_vida;
       
    // Start is called before the first frame update
    void Awake()
    {
        txt_vida = GetComponent<TMP_Text>();
        //subscrever o evento OnPerdeuVida
        FindObjectOfType<PlayerMoviment>().GetComponent<Vida>().OnPerdeuVida += Hud_vida_OnPerdeuVida;
           
    }

    private void Hud_vida_OnPerdeuVida(int obj)
    {
        txt_vida.text = obj.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
