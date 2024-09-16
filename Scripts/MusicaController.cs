using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);                    
    }
    // Start is called before the first frame update
    void Start()
    {
        var lista=FindObjectsOfType<MusicaController>();
        if (lista.Length>1 )
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
