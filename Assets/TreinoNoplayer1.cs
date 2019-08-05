using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoplayer1 : MonoBehaviour
{
    [SerializeField]
    Game game_ref;]

    bool estaNoChao; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touches.Length==1)//Input.touches.Length=atualização q um dedo executou num quadro mais recente
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)//TouchPhase.Began = um dedo tocou a tela
            {                                                                                                 //TouchPhase.Stationary= Um dedo está tocando na tela, mas não se moveu.
                Pular();

            }
                                                             
        }

#if UNITY_ANDROID

#else
        if(Input.GetKeyDown(KeyCode.Space) && game_ref.isGameOver()==false && estaNoChao)//sem false ou true o estaNoChao é true?
        {
            Pular();
        }



    }
}
