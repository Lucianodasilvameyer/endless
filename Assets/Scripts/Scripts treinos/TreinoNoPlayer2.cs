using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TreinoNoPlayer2 : MonoBehaviour
{
    [SerializeField]
    private float distanciaEspadaPlayer;

    public GameObject espadaPrefab;
    bool isgrounded;

    [SerializeField]
    private int vida;

    [SerializeField]
    private float speed;

    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    private float jumpForce;

    bool invencivel;

    [SerializeField]
    Game game_ref;

    bool isdead = false;
    bool recarregar;

    [SerializeField]
    private float spawnarEspadaInicial;

    [SerializeField]
    private float spawnarEspadaMax;

    [SerializeField]
    bool jaSpawnou;



    // Start is called before the first frame update
    void Start()
    {
        if (!game_ref || game_ref == false)
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

             
    }

    // Update is called once per frame
    void Update()
    {
        if (isgrounded == true && game_ref.isGameOver() == false && !isdead && Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        else if(Input.GetKeyDown(KeyCode.P) && recarregar==false && game_ref.isGameOver()==false && !isdead)
        {
            spawnarEspada(distanciaEspadaPlayer);
            recarregar = true;
            spawnarEspadaInicial = Time.time;
        }
        
        if(Time.time>=spawnarEspadaInicial+spawnarEspadaMax && recarregar==true)
        {
            recarregar = false;
        }

#if UNITY_ANDROID

        if(Input.touches.Length==1 && !isdead)
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)
            {
                jump();
            }
            else if (Input.touches[].phase == TouchPhase.Moved && jaSpawnou == false)
            {
                spawnarEspada(distanciaEspadaPlayer);
                jaSpawnou = true;
            }
            else (Input.touches[].phase == TouchPhase.Ended || Input.touches[].phase == TouchPhase.Canceled);
            {
                jaSpawnou = false;
            }
        }


    }
    public void movimento()
    {
        Vector2 input = new Vector2(1,0);
        Vector2 direction = input.normalized;
        Vector2 velocity = speed * direction;
        velocity.y = body.velocity.y;
        body.velocity = velocity;
    }
    public void jump()
    {
        if(isgrounded==false)
        {
            return;
        }
        Vector2 velocity = new Vector2(body.velocity.x, jumpForce);
        body.velocity = velocity;
    }
    public void spawnarEspada(float distanciaEspadaPlayer)
    {
        Vector2 inicialPos = transform.position;
        Vector3 position = inicialPos;
        position.x += distanciaEspadaPlayer;
        position.z = -1;
        GameObject go = Instantiate(espadaPrefab, position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            if(!invencivel)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                vida--;
            }
            
        }
     
    }
}
