using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSobre : MonoBehaviour
{
    public void Voltar()
    {
        SceneManager.LoadScene("menuprincipal");
        //SceneManager.LoadSceneAsync(0,LoadSceneMode.Additive) permite enquanto carrega passar alguma animacao
    }
    
        
    
}
