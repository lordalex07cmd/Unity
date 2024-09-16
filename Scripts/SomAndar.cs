using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SomAndar : MonoBehaviour
{
    [SerializeField] AudioClip[] _sons;
    AudioSource _audioSource;
    int IndiceSom = 0;
    // Start is called before the first frame update
    void Start()
    {
        //configurar audio source
        if (_audioSource == null)
        {
            _audioSource = transform.AddComponent<AudioSource>();
        }
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1;
    }
    public void SomPassos() //som atribuido aos eventos
    {
        _audioSource.PlayOneShot(_sons[IndiceSom]);
        IndiceSom++;
        if (IndiceSom >= _sons.Length)
            IndiceSom = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
