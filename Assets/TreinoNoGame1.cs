using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoGame1 : MonoBehaviour
{
    public GameObject ChaoPrefab;

    public GameObject FundoPrefab; 

    public GameObject InimigoPrefab;
    [SerializeField]
    float SpawnarInimigosInicial;
    [SerializeField]
    float SpawnarInimigosMax;

    [SerializeField]
    float DistanciaDoinimigoDoPlayer;

    public Player player_ref;

    [SerializeField]
    float DistanciaDoChao =-2.63f;

    [SerializeField]
    int QuantidadeDeChao=2;

    Vector2 ProximaPosicaoBG;

    Vector2 ProximaPosicaoCH;

    Queue<GameObject> PoolBG = new Queue<GameObject>();
    Queue<GameObject> PoolCH = new Queue<GameObject>(); 


    // Start is called before the first frame update
    void Start()
    {
       for(int i=0; i<QuantidadeDeChao;i++)
       {
            GameObject Ru = Instantiate(FundoPrefab, ProximaPosicaoBG, Quaternion.identity);
            PoolBG.Enqueue(Ru);

            ProximaPosicaoBG.x += Ru.GetComponent<SpriteRenderer>().bounds.size.x; //pq ficar somando cada posição de sprites?

            GameObject Si = Instantiate(ChaoPrefab, ProximaPosicaoCH, Quaternion.identity);
            PoolCH.Enqueue(Si);

            ProximaPosicaoCH.x += Si.GetComponent<SpriteRenderer>().bounds.size.x;

           

       }

        SpawnarInimigosInicial = Time.time;
        Vector2 PosicaoInicial = player_ref.transform.position; //para q atualizar esta parte?
        PosicaoInicial.x = DistanciaDoinimigoDoPlayer;
        PosicaoInicial.y = DistanciaDoChao;


        SpawnarInimigos(3, 5, PosicaoInicial);//pq chama-lo aqui se ja esta sendo chamado no update??
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=SpawnarInimigosInicial+SpawnarInimigosMax)
        {
            SpawnarInimigosInicial = Time.time;

            Vector2 PosicaoInicial = player_ref.transform.position;
            PosicaoInicial.x += DistanciaDoinimigoDoPlayer; //+=??
            PosicaoInicial.y = DistanciaDoChao;

            SpawnarInimigos(Random.Range(2, 3), (Random.Range(1, 6), PosicaoInicial);

        }
         

        
    }
    public void SpawnarInimigos(int QuantidadeDeInimigos,int Distancia,Vector2 PosicaoInicial)
    {
        for(int i=0;i<QuantidadeDeInimigos;i++)
        {
            int Index = Random.Range(0, InimigoPrefab.Length);// length??
            Vector3 Posicao = PosicaoInicial;//pq usar vector3?
            Posicao.x += i * Distancia;// +=??
            Posicao.z = -1f;

            GameObject fa = Instantiate(InimigoPrefab[Index], Posicao, Quaternion.identity);

        }
    }
    public void ReposicionarFundo()
    {
        GameObject Fundo = PoolBG.Dequeue();

        PoolBG.Enqueue(Fundo);

        Fundo.transform.position = ProximaPosicaoBG;

        ProximaPosicaoBG.x += Fundo.GetComponent<SpriteRenderer>().bounds.size.x;

    }
    public void ReposicionarChao()
    {
        GameObject Chao = PoolCH.Dequeue();

        PoolCH.Enqueue(Chao);

        Chao.transform.position = ProximaPosicaoCH;

        ProximaPosicaoCH.x += Chao.GetComponent<SpriteRenderer>().bounds.size.x;//+=??
    }


}
