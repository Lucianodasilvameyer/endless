using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript: MonoBehaviour
{
   public void PlayGame()
   {
        SceneManager.LoadScene("SampleScene");
   }
   public void quit()
   {
        Debug.Log("saiu");// só diz no console q saiu? //teste
        Application.Quit();
   }     

}
