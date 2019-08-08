using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{


    [SerializeField]
    private float speed;

    [SerializeField]
    Rigidbody2D body;



    // Start is called before the first frame update
    void Start()
    {
        if (!body || body == null)            //não precisa usar o GameObject.findGameObjectwithtag pq o rigidbody ja esta na hierarca da espada;
            body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverEspada();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    public void MoverEspada()
    {
        Vector2 Input = new Vector2(1, 0);
        Vector2 Direction = Input.normalized;
        Vector2 Velocity = speed * Direction;
        Velocity.y = body.velocity.y;  
        body.velocity = Velocity;

    }
}
