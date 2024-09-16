using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int MaxVida = 100;
    public int VidaActual;
    public float IntervaloMorrer = 2;
    public event Action<int> OnPerdeuVida;
    AudioSource _audioSource;
    [SerializeField] AudioClip _somPerderVida;
    [SerializeField] AudioClip _somMorrer;
    [SerializeField] AudioClip _somGanharVida;
    // Start is called before the first frame update
    void Start()
    {
        VidaActual = MaxVida;
        AtualizaVida();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = transform.AddComponent<AudioSource>();
        }
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1;
        if (_somPerderVida != null )
            _audioSource.clip = _somPerderVida;

    }

    public void PerdeVida(int valor)
 {
    VidaActual =VidaActual - valor; 
    if (VidaActual<=0)
    {
            VidaActual = 0 ;
            
            Destroy(gameObject, IntervaloMorrer);
            Debug.Log("Morreu");
            if (_somMorrer != null )
            {
                _audioSource.clip=_somMorrer;
                _audioSource.Play();
            }
            AtualizaVida();
            return;
    }
        AtualizaVida();
        _audioSource.Play();
        //AudioSource.PlayOneShot(_somPerderVida); permite jogar varios sons ao mesmo tempo
 }

    public void GanhaVida(int valor)
{
    VidaActual += valor;
        if (VidaActual > MaxVida)
        {
            VidaActual = MaxVida;
        }
        if (gameObject.tag.Equals("Player"))
            SistemaMensagem.instance.MostrarMensagem($"Ganhou {valor} de vida ");
                
    
        AtualizaVida();
        _audioSource.PlayOneShot(_somGanharVida);
}

    public bool Morreu()
{
    if (VidaActual == 0) return true;   
    return false;
}       
    void AtualizaVida()
    {
        if (OnPerdeuVida != null && gameObject.tag.Equals("Player"))
            OnPerdeuVida(VidaActual);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
