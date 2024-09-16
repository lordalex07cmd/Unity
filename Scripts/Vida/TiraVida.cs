using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// scrip para estar nos objetos que tiram vida
/// </summary>

public class TiraVida : MonoBehaviour
{
    public int ValorTiraVida = 10;
    public float IntervaloPerdaVida = 3;
    float _proximoIntervalo = 0;
    public GameObject Quem_Me_Atirou;

    public void ProcessaColisao(GameObject gameObject)
    {
        if(Time.time<_proximoIntervalo) return;
        var vida = gameObject.GetComponent<Vida>();
        if( vida != null )
        {
            vida.PerdeVida(ValorTiraVida);
            _proximoIntervalo = Time.time + IntervaloPerdaVida;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Quando a colisao ocorre
        //if( collision.transform.tag=="IgnorarTag")return;
        if (Quem_Me_Atirou != null && collision.gameObject == Quem_Me_Atirou) return;

        ProcessaColisao(collision.gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        //enquanto a colisao ocorre
        //if (collision.transform.tag == "IgnorarTag") return;
        if (Quem_Me_Atirou != null && collision.gameObject == Quem_Me_Atirou) return;
        ProcessaColisao(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        //Colisao terminou
    }
    private void OnTriggerEnter(Collider other)
    {
        //colisao ocorre com um trigger
        //if (other.transform.tag == "IgnorarTag") return;
        if (Quem_Me_Atirou != null && other.gameObject == Quem_Me_Atirou) return;
        ProcessaColisao(other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        //colisao esta a ocorrer com um trigger
        //if (other.transform.tag == "IgnorarTag") return;
        if (Quem_Me_Atirou != null && other.gameObject == Quem_Me_Atirou) return;
        ProcessaColisao(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        //colisao com o trigger terminou
    }
}
