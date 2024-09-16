using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPorta : MonoBehaviour, IInteract
{
    [SerializeField] bool Estado=true; // true = fechada false= aberta
    [SerializeField] string ItemNecessario;
    [SerializeField] string triggerFalso = "fechar";
    [SerializeField] string triggerVerdadeiro = "abrir";
    Animator _animator;
    Inventario _inventario;

    public void Acao()
    {
        if (ItemNecessario != "")
        { 
            if(_inventario != null && _inventario.Existe(ItemNecessario)==false)
            {
                SistemaMensagem.instance.MostrarMensagem($"Falta {ItemNecessario}");
                return;
            }
        }
        if (Estado == true)
        {
            _animator.SetTrigger(triggerVerdadeiro);
        }
        else
        { 
            _animator.SetTrigger(triggerFalso);
        }
        Estado = !Estado;
        if (ItemNecessario != "")
            _inventario.GastaItem(ItemNecessario);

    }

    // Start is called before the first frame update
    void Start()
    {
        _inventario=GameObject.FindObjectOfType<Inventario>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
