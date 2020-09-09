using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BirdAi : MonoBehaviour
{
    // Start is called before the first frame update
    
    delegate void birdBrain(Collider avoid);
    birdBrain thinking;
    public int pushes;
    public int maxPushes;
    public float pushSpeed;
    GameObject startPoint;
    float wanderDistance;
    public float pushUseTime;
    public float pushRegenTime;

    public float friction;

    Vector3 velocity;
    public List<Collider> objectsToAvoid; 
    public GameObject Player;
    
    void Start()
    {
        thinking = idle;
        selfCollider = gameObject.GetComponent<CapsuleCollider>();
    }
    void idle(Collider avoid){
        
    }

    void avoidObst(Collider avoid){

    }
    void avoidPlayer(Collider avoid){

    }
    private void OnTriggerExit(Collider other) {
        objectsToAvoid.Remove(other);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += velocity*Time.fixedDeltaTime;
        transform.rotation.SetLookRotation(velocity);
        velocity -= new Vector3(friction,friction,friction);
        if(objectsToAvoid.Contains()){}
        else if(objectsToAvoid.Count>0){
            Collider closetCollider = objectsToAvoid[0];
            foreach(var collider in objectsToAvoid){
                if(Vector3.Distance(transform.position,))
            }
            thinking=avoidObst(
        }
    }
}
