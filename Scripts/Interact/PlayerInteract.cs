using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//script player que permite interagir com objectos atraves da mira

public class PlayerInteract : MonoBehaviour
{
    Camera _camera;
    [SerializeField] float distancia = 5f;
    [SerializeField] Color CorBase;
    [SerializeField] Color CorInteract;
    [SerializeField] RawImage Mira;
    [SerializeField] LayerMask IgnoreLayer;
    // Start is called before the first frame update
    void Start()
    {
        //_camera=FindObjectOfType<Camera>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Mira.color = CorBase;
        Vector3 origem = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        //Debug.DrawRay(origem, _camera.transform.forward * distancia, Color.red);
        if (Physics.Raycast(origem, _camera.transform.forward, out hit, distancia,IgnoreLayer))
        {

            var objeto = hit.collider.GetComponent<IInteract>();
            if (objeto != null)
            {
                Mira.color= CorInteract;
                if (Input.GetButtonDown("Fire2"))
                {
                    objeto.Acao();
                }
            }
        }
    }
}
