using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnim : MonoBehaviour
{
    Animator _animator;
    PlayerMoviment _playerMoviment;
    
    // Start is called before the first frame update
    void Start()
    {
       _animator=GetComponentInChildren<Animator>();
        _playerMoviment = GetComponent<PlayerMoviment>();
        GetComponent<Vida>().OnPerdeuVida += PlayerAnim_OnPerdeuVida;
    }

    private void PlayerAnim_OnPerdeuVida(int VidaActual)
    {   
        //player morreu
        if (VidaActual == 0)
        {
            _animator.SetBool("morreu", true);
            Destroy(_playerMoviment);
            if (gameObject.tag.Equals("Player"))
                SistemaMensagem.instance.MostrarMensagem("Game Over !");
            Invoke(nameof(VoltaAoMenuPrincipal), 8);
        }
    }

    void VoltaAoMenuPrincipal()
    {
        SceneManager.LoadScene("menuprincipal");
    }
    // Update is called once per frame
    void Update()
    {
        //animacoes de movimento
        _animator.SetFloat("velocidade",_playerMoviment._movimentoInput);
        //animacao de saltar
        if(_playerMoviment.Saltou)
        {
            _animator.SetTrigger("saltou");
            _playerMoviment.Saltou=false;
        }
        
    }
}
