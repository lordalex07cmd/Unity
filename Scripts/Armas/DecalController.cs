using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalController : MonoBehaviour
{
    [SerializeField] GameObject ModeloDecal;
    [SerializeField] int MaxDecalAtivos = 10;

    Queue<GameObject> decalsDisponiveis;
    Queue<GameObject> decalsAtivos;

    // Start is called before the first frame update
    void Start()
    {
        InicializarDecals();
    }


    void InicializarDecals()
    {
        decalsAtivos = new Queue<GameObject>();
        decalsDisponiveis = new Queue<GameObject>();
        for (int i = 0; i < MaxDecalAtivos; i++)
        {
            InstanciaDecals();
        }
    }
    void InstanciaDecals()
    {
        var DecalNovo = Instantiate(ModeloDecal);
        DecalNovo.transform.SetParent(transform);
        DecalNovo.SetActive(false);
        decalsDisponiveis.Enqueue(DecalNovo);

    }
    public void SpawnDecal(RaycastHit hit)
    {
        GameObject decal = GetNextDecal();
        if(decal != null)
        {
            decal.transform.position = hit.point;
            decal.transform.rotation = Quaternion.FromToRotation(-Vector3.forward,hit.normal);
            decal.SetActive(true);
            decal.transform.parent=hit.transform;
            decalsAtivos.Enqueue(decal);
        }
    }
    GameObject GetNextDecal()
    {
        if(decalsDisponiveis.Count > 0)
        {
            return decalsDisponiveis.Dequeue();
        }
        var DecalsMaisAntigo=decalsAtivos.Dequeue();
        return DecalsMaisAntigo;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
