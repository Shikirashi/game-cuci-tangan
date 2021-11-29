using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorVisibility : MonoBehaviour{
    public GameObject playerObject;
    public GameObject winText;
    public Raycast Raycast;
    
    
    void Start(){
        Raycast = playerObject.GetComponent<Raycast>();
        winText.SetActive(false);
        Cursor.visible = false;
    }

    private void Update() {
        if(Raycast.cureCount == 10){
            //game won
            StartCoroutine(WinGame());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(playerObject.transform.position.y < -3f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    IEnumerator WinGame() {
        winText.SetActive(true);
        yield return new WaitForSeconds(3f);
    }
}
