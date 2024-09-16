using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLigarDesligar : MonoBehaviour, IInteract
{
    [SerializeField] Light Luz;
    [SerializeField] string ItemNecessario;
    Inventario _inventario;
public void Acao()
    {
        if (Luz != null)
        {
            if (_inventario == null || ItemNecessario == "")
                Luz.enabled = !Luz.enabled;

            else
            {
                if (_inventario.Existe(ItemNecessario))
                {
                    Luz.enabled = !Luz.enabled;
                    _inventario.GastaItem(ItemNecessario);
                }
                else 
                {
                    SistemaMensagem.instance.MostrarMensagem($"Falta {ItemNecessario}");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _inventario = GameObject.FindObjectOfType<Inventario>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
