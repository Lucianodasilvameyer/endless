using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField]
    float spawnarEspadaInicial;

    [SerializeField]
    float spawnarEspadaMax;

    [SerializeField]
    float distanciaEspadaPlayer;

    [SerializeField]
    int vidaInicial; //quando não tem o tipo de visibilidade ela é private

    [SerializeField]
    int vidaMax;


    [SerializeField]
    Animator anim;


    [SerializeField]
    private int vida;

    public int Vida
    {
        get
        {
            return vida;
        }
        set
        {
            if (value <= 0)
            {    vida = 0;
                isDead = true;
                game_Ref.GameOver();
                
            }
            else if(value>vidaMax)
            {
                vida = vidaMax;
            }
            else
            vida = value;       //tem q usar get e set por que toda a vez que muda a propriedade tem q fazer tambem outra tarefa(neste caso garantir q o novo valor não seja menor q zero e atualizar o texto da pontuação)

            slider_.value = (float)vida / (float)vidaMax;//aqui a variavel vida ja avia sido criada

        }
    }


    public Slider slider_;

    bool touchMoving = true;
    bool touchingStationary = true;

    [SerializeField]
    bool recarregar=false; //recarregar sem false ou true é true



    [SerializeField]
    float distanciaEspadaChao= -2.63f;

    public GameObject EspadaPrefab;

    [SerializeField]
    float speed;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float jumpForce;

    [SerializeField]
    Rigidbody2D body;
    [SerializeField]
    bool isGrounded;
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }

        set
        {
            isGrounded = value;

            anim.SetBool("isGrounded", isGrounded);
        }
    }

    [SerializeField]
    bool invencivel;

    [SerializeField]
    Game game_Ref;

    bool isDead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Vida = vidaInicial; //esta é a vida public

        if (!game_Ref || game_Ref == null)   //aqui verifica se o Game tem uma referencia ou não(apontando para uma classe e ganhando dados)
            game_Ref = GameObject.FindGameObjectWithTag("Game"). GetComponent<Game>();//aqui o ! e o null devem ser sempre utilizados para ver se a referencia foi feita ou não

        if (!body || body == null)   //aqui verifica se o rigdbody tem uma referencia ou não(apontando para uma classe e ganhando dados)
        body = GetComponent<Rigidbody2D>();

        if (!anim || anim == null)
            anim = GetComponent<Animator>();

        //if(invencivel)
           // GetComponent<SpriteRenderer>().color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {

        if(body.velocity.y == 0 && transform.position.y <= distanciaEspadaChao + 0.5f)
        {
            IsGrounded = true;
        }


#if UNITY_ANDROID

          if(Input.touches.Length == 1 && !isDead)
        {
          
            if(Input.touches[0].phase == TouchPhase.Began || (Input.touches[0].phase == TouchPhase.Stationary && !touchingStationary))
            {
                Jump();
                touchingStationary = true; //aqui significa q ainda esta precionando
            }
            else if(Input.touches[0].phase==TouchPhase.Moved && !recarregar && !touchMoving)//!recarregar é o mesmo q recarregar==false
            {                                                                                       //aqui com o TouchPhase.Moved o dedo arrastou mas continuou precionado na tela e continuará spawnando a espada
                SpawnarEspada(distanciaEspadaPlayer);
                
                spawnarEspadaInicial = Time.time;
                recarregar = true;
                touchMoving = true;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) //TouchPhase.Ended é quando o dedo foi levantado da tela e o TouchPhase.Canceled é quando o sistema cancela o rastreamento pelo toque, que pode ser causado por algum problema ou atender uma ligação
            {
                touchMoving = false;
                touchingStationary = false;
            }


         

        }
        
            if(Input.touches.Length == 0)
            {
            touchMoving = false;
            touchingStationary = false;
        }
#endif

#if UNITY_EDITOR



        if (Input.GetKeyDown(KeyCode.Space) && game_Ref.isGameOver() == false && !isDead)
        {
            Jump();
        }

       
        else if (recarregar == false && Input.GetKeyDown(KeyCode.P) && game_Ref.isGameOver() == false && !isDead)
        {
            SpawnarEspada(distanciaEspadaPlayer);
            recarregar = true;
            spawnarEspadaInicial = Time.time;
        }

#endif


        if (Time.time >= spawnarEspadaInicial + spawnarEspadaMax && recarregar == true)
        {
            recarregar = false;
        }


    }

    private void FixedUpdate()
    {
        if (game_Ref.isGameOver() == false)
            Movimento();
    }
    public void Movimento()
    {

        Vector2 input = new Vector2(1, 0);
        Vector2 direction = input.normalized;
        Vector2 velocity = speed * direction;



        //body.AddForce(Vector2.right * acceleration);
        velocity.y = body.velocity.y;  //aqui no movimento o body.velocity.y serve para manter a velocidade do y pois aqui o personagem só andará para frente 
        body.velocity = velocity;
    }

    public void Jump()
    {

      if(IsGrounded == false)
      {
            return;
      }

        Vector2 velocity = new Vector2(body.velocity.x, jumpForce);//aqui no movimento o body.velocity.x serve para manter a velocidade do x
        body.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Inimigo"))
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            if (!invencivel)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                

                Vida--;
            }
         
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
    public void SpawnarEspada(float DistanciaEspadaPlayer)
    {
        Vector3 Inicialpos = transform.position;
        Vector3 position = Inicialpos;
        position.x += DistanciaEspadaPlayer;
        position.z = -1;

        GameObject go = Instantiate(EspadaPrefab, position, Quaternion.identity);
    } 
    public bool isPlayerDead()
    {
        return isDead;
    }
}   
