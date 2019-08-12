using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Treino : MonoBehaviour
{
    public GameObject[] inimigoPrefab;

    [SerializeField]
    private float spawnarInimigosInicial;
    [SerializeField]
    private float spawnarInimigosMax;

    Player player_ref;

    [SerializeField]
    private float distanciaInimigoDoplayer;
    [SerializeField]
    private float yDoPLayer = -2.63f;

    public GameObject fundoPrefab;
    public GameObject chaoPrefab;

    Vector2 nextPositionFundo;
    Vector2 nextPositionChao;

    [SerializeField]
    private int numeroMaximoChao = 2;

    Queue<GameObject> PoolFundo = new Queue<GameObject>();
    Queue<GameObject> PoolChao = new Queue<GameObject>();

    public TextMeshProUGUI textoGameOver;

    public TextMeshProUGUI textoScore;

    bool gameOver = false;

    [SerializeField]
    private int scoreCount;

    public int ScoreCount
    {
        get
        {
            return scoreCount;
        }
        set
        {
            if (value < 0)
                scoreCount = 0;
            else scoreCount = value;
            textoScore.text = "Score" + scoreCount;
        }
    }


    
    Button botaoReiniciar;

    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numeroMaximoChao; i++)
        {

            GameObject ru = Instantiate(fundoPrefab, nextPositionFundo, Quaternion.identity);
            PoolFundo.Enqueue(ru);

            nextPositionFundo.x += GetComponent<SpriteRenderer>().bounds.size.x;

            GameObject so = Instantiate(chaoPrefab, nextPositionChao, Quaternion.identity);
            PoolChao.Enqueue(so);

            nextPositionChao.x += GetComponent<SpriteRenderer>().bounds.size.x;
        }

        ScoreCount = 0;

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= spawnarInimigosInicial + spawnarInimigosMax)
        {
            spawnarInimigosInicial = Time.time;
            Vector2 posicaoInicial = player_ref.transform.position;
            posicaoInicial.x += distanciaInimigoDoplayer;
            posicaoInicial.y = yDoPLayer;

            spawnarInimigos(Random.Range(1, 6), 1, 5, Random.Range(0, 5), posicaoInicial);

            
        }

        
    }
    public void spawnarInimigos(int quantidadeDeInimigos,float distanciaMinima,float distanciaMax,float alturaDoPulo,Vector2 posicaoInicial)
    {
        for (int i = 0; i < quantidadeDeInimigos; i++)//para q usar for se esta função vai dentro do update? 
        {
            int index = Random.Range(0, inimigoPrefab.Length);
            Vector3 position = posicaoInicial;
            position.x += i * Random.Range(distanciaMinima, distanciaMax);//i?
            position.y = Random.Range(0, alturaDoPulo);
            position.z = -1f;
            GameObject re = Instantiate(inimigoPrefab[index], position, Quaternion.identity);
        } 
    }
    public void ReutilizarChao()
    {
        GameObject chao = PoolChao.Dequeue();
        PoolChao.Enqueue(chao);

        chao.transform.position = nextPositionChao;

        nextPositionChao.x += chao.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    public void reutilizarFundo()
    {
        GameObject Fundo = PoolFundo.Dequeue();
        PoolFundo.Enqueue(Fundo);

        Fundo.transform.position = nextPositionFundo;
        nextPositionFundo.x += Fundo.GetComponent<SpriteRenderer>().bounds.size.x;
    }
   
    public void GameOver()
    {
        textoGameOver.text = "GameOver";
        gameOver = true;
        player_ref.GetComponent<SpriteRenderer>().enabled = false;// aqui serve para deligar o sprite renderer e assim para de aumentar o chao, o fundo e fabricar inimigos?
        botaoReiniciar.gameObject.SetActive(true);
    }
    public void isGameOver()
    {
        r
    }
    public void reiniciar()
    {
        botaoReiniciar.gameObject.SetActive(false);//aqui desaparece o botão reiniciar?
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
