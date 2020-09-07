using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyoff : MonoBehaviour
{
    // Start is called before the first frame update
    public float howfar=1;
    public float howlong = 5;
    public float howrot;
    private float timeStart;
    private Vector3 velocity;
    private Vector3 rotation;
    void Start()
    {
        transform.parent = null;
        velocity = new Vector3(Random.Range(-howfar,howfar),Random.Range(-howfar,howfar),Random.Range(-howfar,howfar));
        rotation = new Vector3(Random.Range(-howrot,howrot),Random.Range(-howrot,howrot),Random.Range(-howrot,howrot));
        timeStart = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity;
        transform.eulerAngles += rotation;
        if(Time.time - timeStart >5)
            Destroy(this.gameObject);
    }
}
