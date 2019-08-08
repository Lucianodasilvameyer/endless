using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [SerializeField]
    Game game_ref;

    

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    float speed;


    // Start is called before the first frame update
    void Start()
    {
        game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        DefinirAlvo();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();

        



    }
    public void Mover()
    {
        transform.Translate(speed * direction * Time.deltaTime);
    }

    public virtual void DefinirAlvo()
    {
        if (!target || target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;


            direction = target.position - transform.position;
            direction = direction.normalized;
        }    



    }



  



}

