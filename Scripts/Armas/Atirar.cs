using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atirar : MonoBehaviour
{
    [SerializeField] GameObject Objeto;
    [SerializeField] Transform PontoAtirar;
    [SerializeField] float ForcaAtirar = 100;
    [SerializeField] float IntervaloAtirar = 0.5f;
    [SerializeField] string nome_trigger;
    [SerializeField] float IntervaloAnimacao = 0.3f;
    [SerializeField] string ItemNecessario;
    [SerializeField] RawImage UI;
    [SerializeField] Texture2D Imagem;
    float NovoIntervalo = 0;
    public bool Atirou=false;
    Animator _animator;
    Inventario _inventario;
    
    private void OnEnable()
    {
        if (Imagem != null && UI !=null)
            UI.texture = Imagem;
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator =transform.parent.GetComponent<Animator>();
        _inventario = GameObject.FindObjectOfType<Inventario>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (Input.GetButtonDown("Fire1") && Atirou==false)
        {
            //AtirarObjecto();
            if (Time.time < NovoIntervalo) return;
            if (ItemNecessario!= "" && _inventario!=null && _inventario.Existe(ItemNecessario)== false)
            {
                SistemaMensagem.instance.MostrarMensagem($"Falta {ItemNecessario}");
                return;
            }
           

            _animator.SetTrigger(nome_trigger);
            Invoke(nameof(AtirarObjecto), IntervaloAnimacao);
            
        }
        
    }
    public void AtirarObjecto()
    {
        Atirou = false;
        var obj = Instantiate(Objeto,PontoAtirar.position,Quaternion.identity);//quaternion nao roda o objeto
        var tvida=obj.GetComponent<TiraVida>();

        if (tvida != null)
            tvida.Quem_Me_Atirou = transform.root.gameObject;

        //obj.GetComponent<TiraVida>().Quem_Me_Atirou = transform.root.gameObject;
        
        obj.GetComponent<Rigidbody>().AddForce(transform.forward *  ForcaAtirar);

        Destroy(obj, 5);

        NovoIntervalo = Time.time + IntervaloAtirar;
            
        if (ItemNecessario != "" && _inventario != null)
            _inventario.GastaItem(ItemNecessario);
    }
}
