using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetAI : MonoBehaviour
{
    public float floatAmmount;
    public float sideFloatAmmount;
    public float sideTempo;
    public float tempo;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = startPosition + new Vector3(Mathf.Cos(Time.time*sideTempo)*sideFloatAmmount,Mathf.Sin(Time.time*tempo)*floatAmmount,0);
    }
    private void OnTriggerEnter(Collider collider){
        Flyoff flyoff;
        foreach(Transform child in transform){
            flyoff = child.gameObject.AddComponent<Flyoff>() as Flyoff;
            flyoff.howfar = 0.3f;
            flyoff.howrot = 3;
        };
        flyoff = gameObject.AddComponent<Flyoff>() as Flyoff;
        flyoff.howfar = 0.3f;
        flyoff.howrot = 3;
        this.enabled = false;
    }
}
