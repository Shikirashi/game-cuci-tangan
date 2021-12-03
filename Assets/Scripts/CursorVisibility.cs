using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorVisibility : MonoBehaviour{
    public GameObject playerObject;
    public GameObject winText;
    public GameObject loseText;

    private Raycast Raycast;
    private float counter = 0f;

    [SerializeField]
    private List<GameObject> cures = new List<GameObject>();
    
    void Start(){
        Raycast = playerObject.GetComponent<Raycast>();
        winText.SetActive(false);
        loseText.SetActive(false);
        Cursor.visible = false;

        cures.AddRange(GameObject.FindGameObjectsWithTag("Item"));
    }

    private void Update() {
        if(Raycast.cureCount == 10){
            StartCoroutine(WinGame());
        }
        if(playerObject.transform.position.y < -3f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
		if (Input.GetKeyDown(KeyCode.K)) {
            Raycast.cureCount = 10;
		}
        if((cures.Count + Raycast.cureCount) < 10) {
            StartCoroutine(LoseGame());
        }
        counter += Time.deltaTime * 1f;
        if(counter >= .5f) {
            counter = 0f;
            cures.Clear();
            cures.AddRange(GameObject.FindGameObjectsWithTag("Item"));
        }
    }
 
    IEnumerator WinGame() {
        winText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator LoseGame() {
        loseText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
