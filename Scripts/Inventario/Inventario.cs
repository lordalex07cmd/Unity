using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventario : MonoBehaviour
{
    List<Item> inventario;
    List<Item> inventarioNaoVisivel;
    [SerializeField] GameObject panelInventario;
    Image[] imagens;
    [SerializeField] Texture2D imagemFundo;

    // Start is called before the first frame update
    void Start()
    {
        inventario = new List<Item>();
        inventarioNaoVisivel = new List<Item>();
        imagens = Utils.GetComponentsInChildWithoutRoot<Image>(panelInventario);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventario"))
        {
            if(panelInventario.activeSelf==false && Time.timeScale == 1)
            {
                panelInventario.SetActive(true);
                Cursor.lockState = CursorLockMode.None; //cursosr desaparece
                Time.timeScale = 0;
                ShowItems();
            }
            else
            {
                if (panelInventario.activeSelf==true && Time.timeScale == 0)
                {
                    panelInventario.SetActive(false);
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked; //cursor aparece
                }
            }
        }
    }
    //Mostrar os items do inventario no panel
    void ShowItems()
    {
        //limpar inventario
        for (int i = 0; i < imagens.Length; i++)
        {
            if (imagens[i] != null)
            {
                if (imagemFundo != null)
                {
                    Rect rect = new Rect(0, 0, imagemFundo.width, imagemFundo.height);
                    imagens[i].sprite = Sprite.Create(imagemFundo, rect, new Vector2(0, 0));
                }
                else
                {
                    imagens[i].sprite = null;
                }
                imagens[i].color=new Color(imagens[i].color.r,imagens[i].color.g,imagens[i].color.b);
                imagens[i].type= Image.Type.Sliced;
                imagens[i].GetComponentInChildren<Text>().text = "";

            }
        }
        //mostrar inventario
        for (int i = 0; i < inventario.Count; i++)
        {
            Rect rect = new Rect(0, 0, inventario[i].imagem.width, inventario[i].imagem.height);
            imagens[i].sprite = Sprite.Create(inventario[i].imagem, rect, new Vector2(0, 0));
            imagens[i].color=new Color(imagens[i].color.r, imagens[i].color.g, imagens[i].color.b,1);
            imagens[i].GetComponentInChildren<Text>().text = inventario[i].quantidade.ToString();

        }
    }

    //adiciona 1 item ao inventario
    public void Adicionar(Item item)
    {
        if (item.visivel)
        {
            if(inventario.Count>= imagens.Length)
            {
                SistemaMensagem.instance.MostrarMensagem("Inventario esta cheio");
                return;

            }
            //verifica se o item ja existe no inventario
            if (Existe(item.nome))
                AtualizaQuantidade(item); //atualiza quantidade
            else
            {
                inventario.Add(item);
            }
        }
        else
        {
            inventarioNaoVisivel.Add(item);
        }
    }
    public bool Existe(string nome)
    {
        for(int i = 0;i<inventario.Count;i++) 
            if (inventario[i].nome==nome)return true;
        
        for(int i = 0; i < inventarioNaoVisivel.Count; i++) 
            if (inventarioNaoVisivel[i].nome == nome) return true;

        return false;

    }
    //atualiza a quantidade de um inventario
    void AtualizaQuantidade(Item item)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            Item atual = inventario[i];
            if (inventario[i].nome == item.nome)
            {
                atual.quantidade += item.quantidade;
                inventario[i] = atual;
                return; 

            } 
        }
    }
    public void GastaItem(string nome,int quantidade=1)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            Item atual = inventario[i];
            if (inventario[i].nome == nome && inventario[i].gasta)
            {
                atual.quantidade -= quantidade;
                if(atual.quantidade<=0)
                    inventario.RemoveAt(i);
                else
                    inventario[i] = atual;
                return;

            }
        }
    }
}
        
 
