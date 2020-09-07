using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameModeManager : MonoBehaviour
{
    public GameObject Plane;
    public List<GameObject> activeObjects;
    public Level[] levels;
    public GameObject Player;
    private int levelCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnLevel(levelCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if(activeObjects.Count==0){
            levelCounter++;
            spawnLevel(levelCounter);
        }
        if(Vector3.Distance(Player.transform.position,Plane.transform.position)>50f){
            var rigid = Plane.GetComponent<Rigidbody>();
            rigid.velocity = new Vector3(0,0,0);
            rigid.angularVelocity = new Vector3(0,0,0);
            Plane.SetActive(false);
            Plane.transform.position = Player.transform.position;
            Plane.transform.rotation = Player.transform.rotation;
        }
        foreach(Touch touch in Input.touches){
            if(touch.phase == TouchPhase.Began){
                Plane.SetActive(true);
                Plane.transform.position = Player.transform.position;
                Plane.transform.rotation = Player.transform.rotation;
                Plane.GetComponent<Engine>().throttle=1;
                Plane.GetComponent<Shooter>().Launch();
            }
        }
    }
    void spawnLevel(int levelToSpawn){
        foreach(var spawnable in levels[levelToSpawn].objectsToLoad){
            activeObjects.Add(spawnable.spawn(Player));
        }
    }
}
