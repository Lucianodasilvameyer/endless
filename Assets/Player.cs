using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
     float speed;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float jumpForce;
    Rigidbody2D body;
    bool isGrounded;

    [SerializeField]
   bool invencivel;

    [SerializeField]
    Game game_Ref;

    // Start is called before the first frame update
    void Start()
    {
        if (!game_Ref || game_Ref == null)   //aqui verifica se o rigdbody tem uma referencia ou não(apontando para uma classe e ganhando dados)
            game_Ref = GameObject.FindGameObjectWithTag("Game"). GetComponent<Game>();

        if (!body || body == null)   //aqui verifica se o rigdbody tem uma referencia ou não(apontando para uma classe e ganhando dados)
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touches.Length == 1)
        {
            if(Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)
            {
                Jump();
            }
        }



#if UNITY_ANDROID

        
#else
        if (Input.GetKeyDown(KeyCode.Space) && game_Ref.isGameOver() == false && isGrounded)
        {
            Jump();
        }

#endif

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
        Vector2 velocity = new Vector2(body.velocity.x, jumpForce);//aqui no movimento o body.velocity.x serve para manter a velocidade do x
        body.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Inimigo"))
        {
            if(!invencivel)
            game_Ref.GameOver();
           
        }
    }

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

}   
