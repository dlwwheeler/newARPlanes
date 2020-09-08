using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHere : Spawnable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override GameObject spawn(GameObject Player){
        gameObject.SetActive(true);
        return gameObject;
    }
}
