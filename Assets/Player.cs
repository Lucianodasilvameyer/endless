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

    // Start is called before the first frame update
    void Start()
    {
        if(!body || body == null)   //aqui verifica se o rigdbody tem uma referencia ou não(apontando para uma classe e ganhando dados)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

#endif

    }

    private void FixedUpdate()
    {
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
}   
