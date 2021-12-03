using UnityEngine;

public class Crouch : MonoBehaviour{
    CharacterController characterCollider;
    public GameObject GFX;

    void Start(){
        characterCollider = gameObject.GetComponent<CharacterController>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.C)){
            characterCollider.height = 0.5f;
            GFX.transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
        }
        else{
            characterCollider.height = 1.0f;
            GFX.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
