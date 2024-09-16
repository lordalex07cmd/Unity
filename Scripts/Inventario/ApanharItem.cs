using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
//script colocado nos item que se pode apanhar
public class ApanharItem : MonoBehaviour
{
    [SerializeField] string Nome;
    [SerializeField] Texture2D Imagem;
    [SerializeField] bool VisivelNoInventario = true;
    [SerializeField] int Quantidade = 1;
    [SerializeField] bool Gasta = false;
    Item item;
    [SerializeField] AudioClip _somApanhar;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        item = new Item();
        item.nome = Nome;
        item.imagem = Imagem;
        item.quantidade = Quantidade;
        item.gasta = Gasta;
        item.visivel = VisivelNoInventario;
        //configurar audio source
        if (_audioSource == null)
        {
            _audioSource = transform.AddComponent<AudioSource>();
        }
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1;
        if (_somApanhar != null)
            _audioSource.clip = _somApanhar;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var inv = other.GetComponent<Inventario>();
            inv.Adicionar(item);
            if (_audioSource != null && _somApanhar != null)
            {

                _audioSource.PlayOneShot(_somApanhar);
                Destroy(gameObject, _somApanhar.length);//destroy so depois do som acabar
                Destroy(transform.GetComponentInChildren<Renderer>());
                Destroy(transform.GetComponentInChildren<Collider>());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
  }
