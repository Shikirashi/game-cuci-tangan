using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }

    public void Options(){
        Debug.Log("OPTIONS");
    }
    
    public void Tutorial(){
        Debug.Log("TUTORIAL");
        SceneManager.LoadScene(2);
    }
    
    public void QuitGame(){
        Debug.Log("QUIT");
        Application.Quit();
    }
}
