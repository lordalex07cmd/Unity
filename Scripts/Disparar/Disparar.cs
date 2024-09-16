using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Disparar : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float Distancia = 50;
    [SerializeField] int DanoVida = 10;
    [SerializeField] float ForcaTiro = 50;
    [SerializeField] AudioClip SomTiro;
    [SerializeField] Texture2D Imagem;
    [SerializeField] RawImage UI;
    [SerializeField] string ItemNecessario;
    [SerializeField] float IntervaloAnimacao = 0.2f;
    [SerializeField] string trigger_animacao;
    [SerializeField] GameObject ModeloArma;
    [SerializeField] LayerMask IgnoreLayer;

    Inventario _inventario;
    AudioSource _audioSource;
    Animator _animator;
    DecalController _decals;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _audioSource=transform.root.GetComponent<AudioSource>();
        _inventario=GameObject.FindObjectOfType<Inventario>();
        _animator=transform.parent.GetComponent<Animator>();
        _decals=FindObjectOfType<DecalController>();

    }
    private void OnEnable()
    {
        if (Imagem!=null && UI !=null)
            UI.texture = Imagem;
        if (ModeloArma!=null)
            ModeloArma.SetActive(true);
    }
    private void OnDisable()
    {
        if (ModeloArma != null)
            ModeloArma.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (Input.GetButtonDown("Fire1"))
        {
            
            if (ItemNecessario != "" && _inventario != null && _inventario.Existe(ItemNecessario) == false)
            {
                SistemaMensagem.instance.MostrarMensagem($"Falta {ItemNecessario}");
                return;
            }


            _animator.SetTrigger(trigger_animacao);
            Invoke(nameof(SimulaTiro), IntervaloAnimacao);
            
        }
    }
    public void SimulaTiro()
    {
        if (ItemNecessario != "" && _inventario != null)
            _inventario.GastaItem(ItemNecessario);
        if (_audioSource != null && SomTiro != null)
            _audioSource.PlayOneShot(SomTiro);
        Vector3 origem = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(origem, _camera.transform.forward, out hit, Distancia,IgnoreLayer))
        {
            var vida = hit.collider.GetComponent<Vida>();
            if (vida != null)
            {
                vida.PerdeVida(DanoVida);
            }
            else
            {
                if (_decals != null)
                {
                    _decals.SpawnDecal(hit);
                }
            }
            var rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direcao = hit.collider.transform.position - transform.position;
                rb.AddForceAtPosition(direcao.normalized * ForcaTiro, hit.point);
            }
            //to do efeito de particulas
        }
    }
}
