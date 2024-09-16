using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float RaioExplosao = 10.0f;
    [SerializeField] float ForcaExplosao = 100.0f;
    [SerializeField] int TiraVida = 50;
    [SerializeField] bool TemTimer = false; //com timer so explode no fim do tempoexplodir de 1 s 
    [SerializeField] float TempoExplodir = 1f;
    [SerializeField] GameObject EfeitoExplosao;
    [SerializeField] AudioClip SomBomba;
    AudioSource _audioSource;
    bool Explodiu = false;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource != null )
            _audioSource=transform.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
        _audioSource.clip = SomBomba;
        _audioSource.spatialBlend = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (TemTimer==true && Explodiu==false)
        {
            TempoExplodir = TempoExplodir - Time.deltaTime;
            if (TempoExplodir < 0)
            {
                Explodir();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (TemTimer ==false)
        {
            Explodir();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (TemTimer == false)
        {
            Explodir();
        }
    }
    private void Explodir()
    {
        Explodiu = true;
        Vector3 posicaoExplosao = transform.position;
        Collider[] colliders = Physics.OverlapSphere(posicaoExplosao, RaioExplosao);
        foreach (Collider obj in colliders)
        {
            //para evitar que o player perca duas vezes vida tem duas capsulas e ignorado o caracter controlo
            if (obj is CharacterController) continue;

            Rigidbody rb=obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ForcaExplosao, posicaoExplosao, RaioExplosao, 3.0f);
            }
            Vida vd = obj.GetComponent<Vida>();
            if (vd != null)
            {
                vd.PerdeVida(TiraVida);
            }
        }
        if (EfeitoExplosao != null)
        { 
            var efeito=Instantiate(EfeitoExplosao,transform.position,Quaternion.identity);
            Destroy(efeito, 3 );
        }
        if (SomBomba != null)
        {
            _audioSource.Play();
        }
        Destroy(this.gameObject,SomBomba.length);
        transform.GetComponent<Renderer>().enabled =false ;
    }
}
    