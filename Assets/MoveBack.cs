using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public int numeroBack = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
    }
    public void OnTriggerEnter2D(Collider2D outro)
    {
        if(outro.transform.tag=="backmover")
        {
            float LarguraBack = GetComponent<BoxCollider2D>().size.x;//aqui usa boxcollider pq ele trabalha com tamanho?
            Vector3 position = GetComponent<BoxCollider2D>().transform.position;

            position.x += LarguraBack * numeroBack;

            GetComponent<BoxCollider2D>().transform.position = position;
        }
    }
}
