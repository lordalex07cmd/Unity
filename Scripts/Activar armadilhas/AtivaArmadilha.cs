using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaArmadilha : MonoBehaviour
{
    public GameObject[] Pedras;
    public Vector3 ForcaTorque=new Vector3(25,0,0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Pedras.Length; i++)
            {
                var p = Pedras[i].GetComponent<Rigidbody>();
                p.isKinematic = false;
                p.useGravity = true;
                p.AddTorque(ForcaTorque);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
