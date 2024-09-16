using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] float FadeTime = 2;
    [SerializeField] bool FadeInOnStart = true; 
    Color _corPanel;
    Image _imagePanel;
    Coroutine _funcCoroutine=null;

    //singletone
    public static FadeController instance;

    private void Awake()
    {
        if (instance != null) 
        { 
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _imagePanel = GetComponent<Image>();

        //definir cor fade totalmente preto
        _corPanel=new Color(0,0,0,1);
        _imagePanel.color = _corPanel;
        _imagePanel.enabled = true;
        if (FadeInOnStart ==true && _imagePanel!=null)
        {
            _funcCoroutine = StartCoroutine(Co_FadeIn());
        }

    }

    //Coroutine
    IEnumerator Co_FadeIn()
    {
        float speed = 1 / FadeTime;
        float percentagem = 1;
        _imagePanel.enabled = true;
        while (true)
        {
            percentagem -= speed*Time.unscaledDeltaTime ;
            if (percentagem <0)
            {
                break;
            }
            _corPanel=new Color(_corPanel.r,_corPanel.g,_corPanel.b,percentagem);
            _imagePanel.color= _corPanel;
            yield return null;  
        }
        _imagePanel.enabled=false;
    }

    IEnumerator Co_FadeOut()
    {
        float speed = 1 / FadeTime;
        float percentagem = 0;
        _imagePanel.enabled = true;
        while (true)
        {
            percentagem += speed * Time.unscaledDeltaTime;
            if (percentagem >1)
            {
                break;
            }
            _corPanel = new Color(_corPanel.r, _corPanel.g, _corPanel.b, percentagem);
            _imagePanel.color = _corPanel;
            yield return null;
        }
        //_imagePanel.enabled = false;
        _imagePanel.color= new Color(_corPanel.r, _corPanel.g, _corPanel.b, 1);
    }
    public void FadeIn(float? duracao=null) 
    { 
        if (duracao != null)
            FadeTime =(float)duracao;
        if(_funcCoroutine!=null) StopCoroutine(_funcCoroutine);

        _funcCoroutine = StartCoroutine(Co_FadeIn());

    }

    public void FadeOut(float? duracao = null)
    {
        if (duracao != null)
            FadeTime = (float)duracao;
        if (_funcCoroutine != null) StopCoroutine(_funcCoroutine);

        _funcCoroutine = StartCoroutine(Co_FadeOut());

    }
}
