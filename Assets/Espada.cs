using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    [SerializeField]
    Inimigo inimigo;

    [SerializeField]
    private float speed;

    [SerializeField]
    Rigidbody2D body;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoverEspada();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("inimigo"))
        {
            Destroy(inimigo);
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
