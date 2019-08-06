using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoPlayer1 : MonoBehaviour
{
    [SerializeField]
    private float spawnarEspadaInicial;

    bool recarregar = false;

    Game game_ref;

    [SerializeField]
    private float spawnarEspadaMax;

    public GameObject EspadaPrefab;

    [SerializeField]
    private float distanciaChaoEspada; 



    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    Rigidbody2D corpo;

    [SerializeField]
    bool invencivel;

    [SerializeField]
    bool isGrounded;//esta no chão? //sem true ou false é true?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if(Input.touches.Length==1)
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)
            {
                Jump();
            }
            else if(Input.touches[0].phase==TouchPhase.Moved)
            {
                SpawnarEspada(2);
            }  
        }

#if UNITY_ANDROID//colocado depois?

#else
        
        
        if(isGrounded==true && Input.GetKeyDown(KeyCode.Escape) && game_ref.isGameOver()==false)
        {
            Jump();
        }
         

        if(Time.time>=spawnarEspadaInicial+spawnarEspadaMax && recarregar==true)
        {
            spawnarEspadaInicial = Time.time;
        } 
        else if(Input.GetKeyDown(KeyCode.P) && game_ref.isGameOver()==false && recarregar==false)
        {
            SpawnarEspada(2);
        }


#endif

    }
    private void FixedUpdate()
    {
        if (game_ref.isGameOver() == false)
            Movimento();      
    }
    public void Movimento()
    {
        Vector2 Input = new Vector2(1, 0);
        Vector2 Direction = Input.normalized;
        Vector2 Velocity = speed * Direction;
        Velocity.y = corpo.velocity.y;
        corpo.velocity = Velocity;

    }
    public void Jump()
    {
        Vector2 velocity = new Vector2(corpo.velocity.x, jumpForce);
        corpo.velocity = velocity;

    }
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("inimigo"))
        {
            if (!invencivel)
                game_ref.GameOver();   
        }  
    }
    //public void GameOver()
    //{
    // gameOver = true;
    //player_ref.getcomponent<SpriteRenderer>().enable = false;
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
       {
            isGrounded = true;
       }      
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    public void SpawnarEspada(float distanciaPlayerEspada)
    {
        Vector2 position =  transform.position;
        position.x += distanciaPlayerEspada;
        position.y = distanciaChaoEspada;

        GameObject du = Instantiate(EspadaPrefab, position, Quaternion.identity);
    }
}
