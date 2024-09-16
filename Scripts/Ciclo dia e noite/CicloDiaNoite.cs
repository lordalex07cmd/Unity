using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaNoite : MonoBehaviour
{
    [SerializeField] float DuracaoDia = 1f;
    [SerializeField] float MomentoDia = 0;
    [SerializeField] Vector3 VetorRotacao;
    [SerializeField] float VelocidadeRotacao;
    // Start is called before the first frame update
    void Start()
    {
        VelocidadeRotacao = 360 / (DuracaoDia * 60);

    }

    // Update is called once per frame
    void Update()
    {
        MomentoDia += Time.deltaTime / 60;
        if (MomentoDia > DuracaoDia) MomentoDia = 0;
        transform.Rotate(VetorRotacao * VelocidadeRotacao* Time.deltaTime);
    }
}
