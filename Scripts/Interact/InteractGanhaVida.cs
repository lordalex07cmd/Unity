using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractGanhaVida : MonoBehaviour,IInteract
{
    [SerializeField] int VidaGanha = 5;
    [SerializeField] int NumeroVezes = -1;
    [SerializeField] float Intervalo = 5;
    [SerializeField] string ItemNecessario;
    Inventario inventario;
    Vida vida;
    float NextIntervalo;
    public void Acao()
    {
        if (NextIntervalo > 0) 
        {
            SistemaMensagem.instance.MostrarMensagem("Ainda nao esta disponivel");
            return;
        }
        if (NumeroVezes <= 0)
        {
            SistemaMensagem.instance.MostrarMensagem("Esta esgotada a possibilidade recuperar vida aqui.");
            return;

        }
        if (ItemNecessario != "" && inventario != null && inventario.Existe(ItemNecessario) == false)
        {
            SistemaMensagem.instance.MostrarMensagem($"Nao tem o item {ItemNecessario}");
            return;
        }
        vida.GanhaVida(VidaGanha);
        SistemaMensagem.instance.MostrarMensagem("Recuperou vida");
        NumeroVezes--;
        NextIntervalo= Intervalo;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventario=FindFirstObjectByType<Inventario>();
        vida=GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>();
        NextIntervalo = Intervalo;
    }

    // Update is called once per frame
    void Update()
    {
        if (NextIntervalo > 0)
            NextIntervalo -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other is CharacterController)
                Acao();
        }
    }
}
