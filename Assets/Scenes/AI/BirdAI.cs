using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public delegate void birdBrain();
public class BirdAi : MonoBehaviour
{
    // Start is called before the first frame update
    
    public birdBrain thinking;
    public int pushes;
    public int maxPushes;
    public float pushSpeed;
    GameObject startPoint;
    float wanderDistance;
    public float pushUseTime;
    public float pushRegenTime;
    private float lastPush;
    private float lastRegen;

    public float friction;
    public Vector3 positionToAvoid;

    Vector3 velocity;
    public List<Collider> objectsToAvoid; 
    public GameObject Player;
    
    void Start()
    {
        thinking = idle;
        var selfCollider = gameObject.GetComponent<CapsuleCollider>();
    }
    void idle(){
        
    }

    void avoidObst(){
        
    }
    void avoidPlayer(){
        
    }
    private void OnTriggerExit(Collider other) {
        objectsToAvoid.Remove(other);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += velocity*Time.fixedDeltaTime;
        transform.rotation.SetLookRotation(velocity);
        if(velocity.magnitude<0.1f){
            velocity=new Vector3(0,0,0);
            friction=0;
        }
        velocity = Vector3.Normalize(velocity) * (velocity.magnitude-friction/Time.fixedDeltaTime);
        bool playerSpotted = false;
        foreach(var obst in objectsToAvoid){
            if(obst.gameObject == Player){
                 playerSpotted = true;
            }
        }
        if(playerSpotted){
            thinking = avoidPlayer;
        }
        else if(objectsToAvoid.Count>0){
            Collider closetCollider = objectsToAvoid[0];
            foreach(var collider in objectsToAvoid){
                if(Vector3.Distance(transform.position,collider.ClosestPoint(transform.position))
                <Vector3.Distance(transform.position,closetCollider.ClosestPoint(transform.position))){
                    closetCollider = collider;
                }
            }
            positionToAvoid = closetCollider.ClosestPoint(transform.position);
            thinking=avoidObst;
        }
        else {
            thinking = idle;
        }
        thinking();
    }
}
