using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerirArmas : MonoBehaviour
{
    [SerializeField] GameObject[] Armas;
    [SerializeField] int ArmaSelectionada=0;

    // Start is called before the first frame update
    void Start()
    {
        AtualizaArmas();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale ==0 ) return;
        float roda=Input.mouseScrollDelta.y;
        if (roda > 0) 
        {
            ArmaSelectionada = ArmaSelectionada + 1;
            if( ArmaSelectionada >= Armas.Length )
                ArmaSelectionada = 0;
            AtualizaArmas();
        }
        if (roda < 0)
        {
            ArmaSelectionada = ArmaSelectionada - 1;
            if (ArmaSelectionada < 0)
                ArmaSelectionada = Armas.Length-1;
            AtualizaArmas();
        }
    }
    void AtualizaArmas()
    {
        for (int i = 0; i < Armas.Length; i++)
        {
            Armas[i].SetActive(false);

        }
        Armas[ArmaSelectionada].SetActive(true);
    }
}

