using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    CharacterController _cc;
    [SerializeField] float VelocidadeMovimento=2;
    [SerializeField] float VelocidadeRotacao = 15;
    [SerializeField] float VelocidadeSalto = -2 ; //tem de ser negativo para contrariar a gravidade
    Vector3 _velocidade;
    [SerializeField] bool _isGrounded;
    public float _movimentoInput; //permite ao outro script ter acesso a esta variavel
    public bool Saltou = false;
    [SerializeField] bool RodaComRato=false;
    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        if (_cc == null)
            Debug.Log("o player tem de ter o character controller");
    }

    // Update is called once per frame
    void Update()
    {
        //Rotacao
        float _rotacaoInput = Input.GetAxisRaw("Horizontal");
        if (RodaComRato)
            _rotacaoInput = Input.GetAxisRaw("Mouse X");
        transform.Rotate(Vector3.up * _rotacaoInput * VelocidadeRotacao * Time.deltaTime);
        
        //Movimento 
        _movimentoInput = Input.GetAxisRaw("Vertical");
        //Correr
        if(_movimentoInput>0)
        {
            if (Input.GetButton("Run"))
                _movimentoInput = _movimentoInput * 2; 
        }
        Vector3 movimento = transform.forward * _movimentoInput * Time.deltaTime * VelocidadeMovimento;
        _cc.Move(movimento);
        //Salto
        if (Input.GetButtonDown("Jump") && _isGrounded )
        {
            _velocidade.y = Mathf.Sqrt(VelocidadeSalto*Physics.gravity.y);
            Saltou = true;
        }
        //Gravidade
        _velocidade = _velocidade+Physics.gravity*Time.deltaTime;
        _cc.Move(_velocidade*Time.deltaTime);
        _isGrounded = _cc.isGrounded;
           
    }
}
