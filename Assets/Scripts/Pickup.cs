using UnityEngine;

public class Pickup : MonoBehaviour{
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject item;
    public GameObject tempParent;

    float throwForce = 600f;
    float distance;
    Vector3 objectPos;

    void Update() {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if(distance >= 2f){
            isHolding = false;
            canHold = true;
        }
        //check if isHolding
        if(isHolding){
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
            if(Input.GetMouseButtonDown(1)){
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
                canHold = false;
            }
        }
        else{
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    void OnMouseOver() {
        if(Input.GetKey("e") && canHold){
            if (distance <= 2f) {
                isHolding = true;
                item.GetComponent<Rigidbody>().useGravity = false;
                item.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }
        else{
            isHolding = false;
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    
}
