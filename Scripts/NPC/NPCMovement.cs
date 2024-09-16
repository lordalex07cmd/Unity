using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public enum NPCEstados { Idle = 0, Patrulha = 1, Atacar = 2, Morto = 3 }
    public Transform[] Pontos; //pontos npc vai patrulhar
    public float DistanciaMinima = 1;
    public float Velocidade = 3;
    public int ProximoPonto = 0;
    Animator _animator;
    Vida _vida;
    public NPCEstados Estado = NPCEstados.Idle;
    NavMeshAgent _agente;
    public bool Inimigo;
    public Transform Olhos;
    public float DistanciaMaximaVe = 50;
    public bool VePlayer;
    TiraVida _tiraVida;
    public float DistanciaAtaque = 1;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _vida = GetComponent<Vida>();
        _agente = GetComponent<NavMeshAgent>();
        _tiraVida = GetComponent<TiraVida>();
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>().OnPerdeuVida += NPCMovement_OnPerdeuVida;

    }

    private void NPCMovement_OnPerdeuVida(int VidaPlayer)
    {
        if(VidaPlayer==0)
        {
            player=null;
            Estado = NPCEstados.Idle;
            if (_animator != null)
                _animator.SetFloat("velocidade", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //npc esta morto
        if (_vida != null && _vida.Morreu())
        {
            if (Estado == NPCEstados.Morto) return;
            if (_animator != null)
                _animator.SetBool("morreu", true);
            Estado = NPCEstados.Morto;
            if (_agente != null)
                Utils.StopNavMeshAgent(_agente);
            Destroy(GetComponent<TiraVida>());
        }
        if(Estado==NPCEstados.Morto) { return; }
        if (player == null) return;
        if (Inimigo)
        {
            //ve player
            VePlayer = Utils.CanYouSeeThis(Olhos, player.transform, null, 90, DistanciaMaximaVe);
            if (VePlayer == true && _agente != null)
            {
                _agente.isStopped = false;
                _agente.SetDestination(player.transform.position);
                _agente.speed = Velocidade * 2;
                Estado = NPCEstados.Atacar;
                if (_animator != null)
                    _animator.SetFloat("velocidade", 1);
            }
            else
            {
                Estado = NPCEstados.Patrulha;
                //Utils.StopNavMeshAgent(_agente);
            }
        }
        if (Estado == NPCEstados.Idle)
        {
            if (_animator != null)
                _animator.SetFloat("velocidade", 0);
            return;
        }
        if (Estado == NPCEstados.Patrulha)
        {
            if (Pontos.Length > 0)
                Patrulhar();
            else
                Estado = NPCEstados.Idle;
        }
        if (Estado == NPCEstados.Atacar && Inimigo)
            Atacar();
    }
    void Patrulhar()
    {
        //Debug.Log("a patrulhar");
        if (Vector3.Distance(transform.position, Pontos[ProximoPonto].position)<DistanciaMinima)
        {
            ProximoPonto++;
            if (ProximoPonto > Pontos.Length - 1)
                ProximoPonto = 0;
        }
        if (_animator != null)
            _animator.SetFloat("velocidade", 0.5f);
        _agente.isStopped = false;
        _agente.speed = Velocidade;
        _agente.SetDestination(Pontos[ProximoPonto].position);

    }
    void Atacar()
    {
        if(Vector3.Distance(transform.position,player.transform.position)<DistanciaAtaque)
        {
        Utils.StopNavMeshAgent(_agente);
        transform.LookAt(new Vector3(player.transform.position.x,
             transform.position.y,
             player.transform.position.z));
       // if(_animator!=null)
       _animator?.SetTrigger("atacou");
            _tiraVida.ProcessaColisao(player);
        }
    }
}
