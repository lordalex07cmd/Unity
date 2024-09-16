using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] int MaxItems = 5 ; // no max de NPC
    [SerializeField] float Intervalo = 5 ; // intervalo tempo entre spawn de npc
    [SerializeField] GameObject[] Modelo;
    [SerializeField] Transform[] Pontos;
    float NextSpawn = 0;
    [SerializeField] List<GameObject> ListaItems;
    // Start is called before the first frame update
    void Start()
    {
        NextSpawn = Intervalo ;
    }

    // Update is called once per frame
    void Update()
    {
        NextSpawn -= Time.deltaTime;
        if (NextSpawn <= 0 )
        {
            NextSpawn=Intervalo+ Random.Range(0, 5) ;
            //retirar os npc que morreram da lista
            for(int i=ListaItems.Count-1 ; i>=0 ; i-- )
            {
                if( ListaItems[i] == null )
                    ListaItems.RemoveAt(i);
            }
            if( ListaItems.Count >= MaxItems ) return;
            //spawnar novo item
            Vector3 posicao=new Vector3(transform.position.x + Random.Range(-1,1),
                                        transform.position.y,
                                        transform.position.z + Random.Range(-1,1));
            var npc= Instantiate(Modelo[Random.Range(0,Modelo.Length)],
                posicao, Quaternion.identity);
            ListaItems.Add(npc);

            if (Pontos.Length > 0) 
            {
                npc.GetComponent<NPCMovement>().Pontos= Pontos;
            }
        }
    }
}
