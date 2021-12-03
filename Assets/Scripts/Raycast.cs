using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour{
    [SerializeField] private GameObject raycastedObject;
    [SerializeField] private GameObject raycastedObjectHeld;
    private bool crosshairNormal = true;

    [SerializeField] private float rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;

    public Camera cam;
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject tempParent;
    public int cureCount;
    public static int curesCount;
    public float grabRange;
    public AudioClip bottle;

    float throwForce = 600f;
    float distance;
    Vector3 objectPos;

    private void Awake() {
        cureCount = 0;
        curesCount = 0;
    }

    private void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayLength, layerMaskInteract.value)){
            if(hit.collider.CompareTag("Item")){
                raycastedObject = hit.collider.gameObject;
                CrosshairActive();
                if(Input.GetMouseButton(1)) {
                    Debug.Log("Picked up " + raycastedObject.name);
                    if(cureCount < 10){
                        cureCount++;
                        curesCount = cureCount;
                        AudioSource.PlayClipAtPoint(bottle, tempParent.transform.position);
                    }
                    raycastedObject.SetActive(false);
                }
            }
            if (hit.collider.CompareTag("Object")) {
                if (raycastedObjectHeld == null) {
                    raycastedObject = hit.collider.gameObject;
                }
                CrosshairActive();
                if (Input.GetMouseButton(1) && canHold) {
                    Debug.Log("Interacted with " + raycastedObject.name);
                    isHolding = true;
                    Debug.Log("Is holding " + raycastedObject.name);
                    raycastedObject.GetComponent<Rigidbody>().useGravity = false;
                    raycastedObject.GetComponent<Rigidbody>().detectCollisions = true;
                    distance = Vector3.Distance(raycastedObject.transform.position, transform.position);
                    //check if isHolding
                    if (isHolding) {
                        raycastedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        raycastedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        raycastedObject.transform.SetParent(tempParent.transform);
                        raycastedObjectHeld = raycastedObject;
                        if (Input.GetKeyDown("e")) {
                            raycastedObject.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                            isHolding = false;
                            canHold = false;
                        }
                    }
                    else {
                        objectPos = raycastedObject.transform.position;
                        raycastedObject.transform.SetParent(null);
                        raycastedObject.GetComponent<Rigidbody>().useGravity = true;
                        raycastedObject.transform.position = objectPos;
                    }
                    if (distance >= grabRange) {
                        isHolding = false;
                        canHold = true;
                        raycastedObject.transform.parent = null;
                        raycastedObject.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
                else{
                    isHolding = false;
                    raycastedObject.transform.parent = null;
                    raycastedObject.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
        else{
            if(!crosshairNormal) {
                CrosshairNormal();
            }
            raycastedObjectHeld = null;
            isHolding = false;
            if(raycastedObject != null){
                /*if(!hit.collider.CompareTag("Item")){
                    raycastedObject.transform.parent = null;
                }*/
                if(raycastedObject.GetComponent<Rigidbody>()){
                    raycastedObject.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            canHold = true;
        }
    }

    void CrosshairActive(){
        uiCrosshair.color = Color.red;
        crosshairNormal = false;
    }

    void CrosshairNormal(){
        uiCrosshair.color = Color.white;
        crosshairNormal = true;
    }

    /*void OnMouseOver() {
        if (Input.GetKey("e") && canHold) {
            if (distance <= 2f) {
                isHolding = true;
                Debug.Log("Is holding " + raycastedObject.name);
                raycastedObject.GetComponent<Rigidbody>().useGravity = false;
                raycastedObject.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }
        else {
            isHolding = false;
            raycastedObject.transform.parent = null;
            raycastedObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }*/
}
