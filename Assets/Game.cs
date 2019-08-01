using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{


    public GameObject[] inimigoPrefabs;

    public GameObject BackGroundPrefab;
    public GameObject groundPrefab;

    public Player player_ref;

    [SerializeField]
    float distanceEnemyFromPlayer;
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

        
        for(int i = 0; i < maxPoolNumber; i++)
        {
            GameObject go = Instantiate(BackGroundPrefab, nextPositionBG, Quaternion.identity);
            poolBG.Enqueue(go);

          
            nextPositionBG.x += go.GetComponent<SpriteRenderer>().bounds.size.x;

            
             
            go = Instantiate(groundPrefab, nextPositionGR, Quaternion.identity);
            poolGR.Enqueue(go);

            nextPositionGR.x += go.GetComponent<SpriteRenderer>().bounds.size.x ;
            


        }

        SpawnarInimigo();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnarInimigo()
    {
        int index = Random.Range(0, inimigoPrefabs.Length);


        Vector3 position = new Vector3(player_ref.transform.position.x + distanceEnemyFromPlayer, -2.63f,-1f);
        GameObject go = Instantiate(inimigoPrefabs[index], position, Quaternion.identity);

    }
    public void ReciclarGR()
    {
        GameObject chao = poolGR.Dequeue();//o primeiro a sair da fila é o primeiro q entrou na fila
       

        poolGR.Enqueue(chao);
       

        chao.transform.position = nextPositionGR; //aqui serve para repetir todo o chão?

      


        nextPositionGR.x += chao.GetComponent<SpriteRenderer>().bounds.size.x;
       
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
        gameOver = true;
        player_ref.GetComponent<SpriteRenderer>().enabled = false;
        // mensgaem de gameover
    }

}
