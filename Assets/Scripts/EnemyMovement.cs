using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour{
    NavMeshAgent agent;
    public StateEnum State;
    protected Target[] potentialTargets;
    protected Target target;
    protected float NextState;

    Transform player;
    EnemyRaycast enemyRaycast;
    float angular;

    public GameObject cam;
    public float deviation = 5f;
    public float healthy;
    public float range;
    public float rageRange = 10f;
    public float defRange = 20f;
    public event System.Action<float> OnSeenPlayer;
    public bool canFollow;

    void Start(){
        canFollow = false;
        player = PlayerManager.instance.player.transform;
        //cam = this.gameObject.transform.GetChild(0).gameObject;
        agent = GetComponent<NavMeshAgent>();
        enemyRaycast = GetComponentInChildren<EnemyRaycast>();
        defRange = range;
        potentialTargets = FindObjectsOfType<Target>(); 
        target = potentialTargets[Random.Range(0, potentialTargets.Length)];
        Vector3 targetDeviation = new Vector3(Random.Range(-deviation, deviation), 0f, Random.Range(-deviation, deviation));
        agent.SetDestination(target.transform.position + targetDeviation);
        State = StateEnum.RUN;

    }

    void FixedUpdate() {
        RaycastHit hitPlayerInfo;
        healthy = GetComponent<Enemy>().CurrentHealth;
        float playerDistance = Vector3.Distance(player.position, cam.transform.position);
        if(playerDistance <= range && canFollow){
            Follow();
        }
        else{
            canFollow = false;
        }
        for(angular = 60f; angular > -60f; angular--) {
            Vector3 direction = cam.transform.forward;
            Quaternion spreadAngle = Quaternion.AngleAxis(angular, new Vector3(0f, 1f, 0f));
            Vector3 shootAngle = spreadAngle * direction;
            //Debug.Log(shootAngle);
            if (Physics.Raycast(cam.transform.position, shootAngle, out hitPlayerInfo, range)) {
                if (hitPlayerInfo.collider.CompareTag("Player")) {
                    canFollow = true;
                }
            }
        }

        NextState -= Time.deltaTime;

        switch(State){
            case StateEnum.RUN:
                if(agent.desiredVelocity.magnitude < 0.1f){
                    State = StateEnum.SHOOT;
                    NextState = Random.Range(1f, 5f);
                }
                break;
            case StateEnum.SHOOT:
                if(NextState < 0){
                    State = StateEnum.RUN;
                    range = defRange;
                    target = potentialTargets[Random.Range(0, potentialTargets.Length)];
                    Vector3 targetDeviation = new Vector3(Random.Range(-deviation, deviation), 0f, Random.Range(-deviation, deviation));
                    agent.SetDestination(target.transform.position + targetDeviation);
                }
                break;
        }

        transform.position += agent.desiredVelocity * Time.deltaTime;
    }

    public enum StateEnum{
        RUN,
        SHOOT
    }

    void Follow(){
        //Debug.Log("Seen player");
        if (OnSeenPlayer != null) {
            OnSeenPlayer(healthy);
        }
        Vector3 direction = (player.position - cam.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        Quaternion lookUpDown = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, lookUpDown, 5f * Time.deltaTime);
        agent.SetDestination(player.position);
        Debug.Log("Going to player");
        range = +rageRange;
        State = StateEnum.SHOOT;
        NextState = Random.Range(1f, 5f);
    }
}
