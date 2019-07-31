using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject BackGroundPrefab;
    public GameObject groundPrefab;

    [SerializeField]
    int maxPoolNumber = 2;
    [SerializeField]
    Vector2 nextPositionBG;
    [SerializeField]
    Vector2 nextPositionGR;

    Queue<GameObject> poolBG = new Queue<GameObject>();
    Queue<GameObject> poolGR = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        
        for(int i = 0; i < maxPoolNumber; i++)
        {
            GameObject go = Instantiate(BackGroundPrefab, nextPositionBG, Quaternion.identity);
            poolBG.Enqueue(go);

            print("borzer z = " + go.GetComponent<SpriteRenderer>().bounds.size.x);
            nextPositionBG.x += go.GetComponent<SpriteRenderer>().bounds.size.x;

            
             
            go = Instantiate(groundPrefab, nextPositionGR, Quaternion.identity);
            poolGR.Enqueue(go);

            nextPositionGR.x += go.GetComponent<SpriteRenderer>().bounds.size.x ;
            


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
