using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    Vector3 Startplace;

    public Game[] GamesInScene;
    public GameObject GameToActivate;
    public float floatScale;
    // Start is called before the first frame update
    void Start()
    {
        Startplace = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Startplace + new Vector3(0,Mathf.Sin(Time.time)*floatScale,0);

    }
    private void OnTriggerEnter(Collider other){
        GameToActivate.SetActive(true);
        foreach(var game in GamesInScene){
            Destroy(game.gameObject);
        }
    }
}
