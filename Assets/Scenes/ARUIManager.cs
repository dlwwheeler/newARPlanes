using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARUIManager : MonoBehaviour
{

    // Start is called before the first frame update
    //public pair<Button,GameObject>[] Spawners;
    private List<ARRaycastHit> hits;
    public GameObject planeObject;
    public Button planeSpawner;
    public ARRaycastManager raycastManager;
    public ARPlaneManager aRPlaneManager;
    public Slider throttle;
    public Joystick joystick;
    
    void Start()
    {
        MultiWingAirplane multiWingPlane = planeObject.GetComponent<MultiWingAirplane>();

        hits = new List<ARRaycastHit>();
        if(multiWingPlane!=null){
        multiWingPlane.variableJoystick = joystick;
        multiWingPlane.UIthrottle = throttle;
        
        }
        else{
            Debug.LogError("object does not have a plane");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(throttle.enabled)return;
        //Debug.Log("looking for touch");
        foreach(
        Touch touch in Input.touches){
            raycastManager.Raycast(touch.position,hits);
            if(touch.phase == TouchPhase.Began){
            Pose pose = hits[0].pose;
            planeObject.SetActive(false);
            planeObject.transform.position = pose.position;
            planeObject.GetComponent<Rigidbody>().velocity =new  Vector3(0,0,0);
            planeObject.transform.eulerAngles =new Vector3(0,0,0);
            planeObject.SetActive(true);
            throttle.gameObject.SetActive(true);
            throttle.enabled = true;
            joystick.gameObject.SetActive(true);
            joystick.enabled = true;
            planeObject.SetActive(true);
        }
        }
    }
    public void ResetAircraft(){
        planeObject.SetActive(false);
        throttle.gameObject.SetActive(false);
        throttle.enabled =false;
        throttle.value =0;
        joystick.gameObject.SetActive(false);
        joystick.enabled = false;
    }
}
