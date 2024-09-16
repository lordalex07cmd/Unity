using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpurraObjetos : MonoBehaviour
{
    [SerializeField] float forca;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.isStatic == true || other.gameObject.CompareTag("Player") == true)
        {
            return;
        }
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direcao = other.transform.position - transform.position;
            rb.AddForce(direcao.normalized * forca);
        }
    }
}