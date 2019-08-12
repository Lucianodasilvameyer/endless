﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int taxaDePontos;

    [SerializeField]
    private float pontuacaoInicio;

    [SerializeField]
    Button botaoReiniciar;

    [SerializeField]
    private int pontuacaoMax;

    public TextMeshProUGUI textoScore;


    public TextMeshProUGUI textoGameOver;

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
            else scoreCount = value;       //tem q usar get e set por que toda a vez que muda a propriedade tem q fazer tambem outra tarefa(neste caso garantir q o novo valor não seja menor q zero e atualizar o texto da pontuação)

            textoScore.text = "Score " + scoreCount;

        }
    }

    private float timerRespawnInimigos;

    [SerializeField]
    private float timerRespawnInimigosMax;


    public GameObject[] inimigoPrefabs;

    public GameObject BackGroundPrefab;
    public GameObject groundPrefab;

    public Player player_ref;// o Player é o script

    [SerializeField]
    float distanceEnemyFromPlayer;
    [SerializeField]
    float groundLevel = -2.65f;

    [SerializeField]
    int maxPoolNumber = 2;
    [SerializeField]
    Vector2 nextPositionBG;
    [SerializeField]
    Vector2 nextPositionGR;
    bool gameOver = false;
    

    Queue<GameObject> poolBG = new Queue<GameObject>();
    Queue<GameObject> poolGR = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        
        for(int i = 0; i < maxPoolNumber; i++)  //
        {
            GameObject go = Instantiate(BackGroundPrefab, nextPositionBG, Quaternion.identity);//nextPositionGR representa a proxima posição q o objeto vai ser spawnado
            poolBG.Enqueue(go);

          
            nextPositionBG.x += go.GetComponent<SpriteRenderer>().bounds.size.x;  //aqui a função bounds.size é o tamanho do objeto e para utiliza-la deve-se usar o go.GetComponent<SpriteRenderer>() antes  e o primeiro x é porque só quer trabalhar com o x
                                                                                  //este  nextPositionBG influencia dentro do instantiate de cima;                                                                      
                                                                                  //o nextPositionBG.x começa em zero e o += serve para ir adicionando o tamanho do sprite?


            go = Instantiate(groundPrefab, nextPositionGR, Quaternion.identity);
            poolGR.Enqueue(go);

            nextPositionGR.x += go.GetComponent<SpriteRenderer>().bounds.size.x ;
            


        }

        Vector2 initialPos = player_ref.transform.position;//??
        initialPos.x += distanceEnemyFromPlayer;//??
        initialPos.y = groundLevel;//??

        //SpawnarInimigos(3,5, initialPos);//??

        timerRespawnInimigos = Time.time;//??

        ScoreCount=0;//??

    }

    // Update is called once per frame
    void Update()
    { 
        if (Time.time >= timerRespawnInimigos + timerRespawnInimigosMax)
        {
            timerRespawnInimigos = Time.time;
          

            Vector2 initialPos = player_ref.transform.position;
            initialPos.x += distanceEnemyFromPlayer;
            initialPos.y = groundLevel;// aqui o y é sempre o mesmo



            SpawnarInimigos(Random.Range(2,5), 1, 6 , Random.Range(0, 5), initialPos);// o 1,6 são respectivamente a distanceMin e distanceMax entre os inimigos?
        }

        if (Time.time >= pontuacaoInicio + pontuacaoMax && player_ref.isPlayerDead()==false)
        {
            pontuacaoInicio = Time.time;
            ScoreCount += taxaDePontos;
        }
        



    }
    public void SpawnarInimigos( int quantidadeIinimigos, float distanceMin, float distanceMax, float heightMax, Vector2 initialPos)
    {

        for(int i = 0; i < quantidadeIinimigos;i++)
        {
            int index = Random.Range(0, inimigoPrefabs.Length);//de zero ao tamanho do inimigoPrefabs?
            Vector3 position = initialPos;  //pq mecher com vector3 se este jogo é 2d?
            position.x += i * Random.Range(distanceMin,distanceMax); //colocando o random.range aqui em baixo a localização dos inimigos ira sempre mudando ao chamar a função
            position.y += Random.Range(0, heightMax);
            position.z = -1f;  //-1f pq o z é sempre o mesmo?
            GameObject go = Instantiate(inimigoPrefabs[index], position, Quaternion.identity);


        }
        

    }
   



    public void ReciclarGR()//aqui os objetos são tirados e colocados na fila
    {
        GameObject chao = poolGR.Dequeue();

                                     //o primeiro a sair da fila é o primeiro q entrou na fila
        poolGR.Enqueue(chao);
       

        chao.transform.position = nextPositionGR; 




        nextPositionGR.x += chao.GetComponent<SpriteRenderer>().bounds.size.x;//sempre q mecher tem q atualizar
       
    }

   public void ReciclarBG()
    {
        GameObject backGround = poolBG.Dequeue();
        poolBG.Enqueue(backGround);

        backGround.transform.position = nextPositionBG;
        nextPositionBG.x += backGround.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public bool isGameOver()
    {
        return gameOver;
    }

    public void  GameOver()
    {
        textoGameOver.text = "GameOver";
        gameOver = true;
        player_ref.GetComponent<SpriteRenderer>().enabled = false;
        // mensgaem de gameover

        botaoReiniciar.gameObject.SetActive(true);
    }
    public void Reiniciar()
    {
        botaoReiniciar.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

}