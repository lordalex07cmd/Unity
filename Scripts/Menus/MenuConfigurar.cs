using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuConfigurar : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown ddQualidade;
    [SerializeField] TMPro.TMP_Dropdown ddResolucao;
    [SerializeField] Toggle tgFullScreen;
    [SerializeField] Toggle tgVSinc;
    [SerializeField] Toggle tgInvertido;

    public void NovaResolucao(int indice)
    {
        string resolucao=ddResolucao.options[indice].text;//800x600
        string[] items = resolucao.Split('x');  //[0]800 [1]600
        int largura = int.Parse(items[0]);
        int altura=int.Parse(items[1]);
        Screen.SetResolution(largura, altura,tgFullScreen.isOn);

        PlayerPrefs.SetString("Resolucao", resolucao);
        PlayerPrefs.Save();
                                               
    }
    public void NovaQualidade(int indice)
    {
        QualitySettings.SetQualityLevel(indice, true);

        PlayerPrefs.SetInt("Qualidade", indice);
        PlayerPrefs.Save();
    }
    public void NovoFullScreen()
    {
        Screen.fullScreen = tgFullScreen.isOn;

        PlayerPrefs.SetInt("EcranCompleto", tgFullScreen.isOn ? 1:0); //? é um if
        PlayerPrefs.Save();
    }
    public void NovoVSinc()
    {
        QualitySettings.vSyncCount = (tgVSinc.isOn ? 1 : 0);
        PlayerPrefs.SetInt("VSinc", tgVSinc.isOn ? 1 : 0); //? é um if
        PlayerPrefs.Save();
    }

    public void NovoRatoInvertido()
    {
        PlayerPrefs.SetInt("mouse_invertido",tgInvertido.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void Voltar()
    {
        SceneManager.LoadScene("menuprincipal");
        //SceneManager.LoadSceneAsync(0,LoadSceneMode.Additive) permite enquanto carrega passar alguma animacao
    }
    // Start is called before the first frame update
    void Start()
    {
        tgFullScreen.isOn = PlayerPrefs.GetInt("EcranCompleto", 1) == 1 ? true:false ;
        
        tgVSinc.isOn = PlayerPrefs.GetInt("VSinc",1)==1 ? true:false ;
        
        string resolucao = PlayerPrefs.GetString("Resolucao", "1920 x 1080");
        for (int i = 0;i< ddResolucao.options.Count; i++)
        {
            if (ddResolucao.options[i].text == resolucao)
            {
                ddResolucao.value = i;
                break; //sair do ciclo
            }
        }

        int qualidade = PlayerPrefs.GetInt("Qualidade", 3);
        ddQualidade.value = qualidade;

        tgInvertido.isOn=PlayerPrefs.GetInt("mouse_invertido",0)==1 ? true :false;
    }
    public static void CarregarConfiguracao()
    {
        bool ecrancompleto = PlayerPrefs.GetInt("EcranCompleto", 1) == 1 ? true : false;
        
        bool vsinc = PlayerPrefs.GetInt("VSinc", 1) == 1 ? true : false;

        string resolucao = PlayerPrefs.GetString("Resolucao", "1920 x 1080");

        int qualidade = PlayerPrefs.GetInt("Qualidade", 3);

        //resolucao ecran completo
        string[] items = resolucao.Split('x');
        int largura = int.Parse(items[0]);
        int altura = int.Parse(items[1]);
        Screen.SetResolution(largura, altura, ecrancompleto);

        //vsinc
        QualitySettings.vSyncCount = (vsinc ? 1 : 0);

        //qualidade
        QualitySettings.SetQualityLevel(qualidade);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
