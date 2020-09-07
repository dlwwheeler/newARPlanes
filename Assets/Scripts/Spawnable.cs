using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public float Distance;
    public float Height;
    public float Variance;
    public float rotVariance;
    public GameObject spawn(GameObject Player){
        float y =Player.transform.eulerAngles.y;
        gameObject.SetActive(true);
        rotVariance=Random.Range(-rotVariance,rotVariance);
        gameObject.transform.position = Player.transform.position;
        gameObject.transform.position += new Vector3(Mathf.Cos(y+rotVariance)*Distance + Random.Range(-Variance,Variance),Height, Mathf.Sin(y+rotVariance)*Distance + Random.Range(-Variance,Variance));

        gameObject.transform.eulerAngles = new Vector3(0,y+rotVariance,0);
        return gameObject;
    }
}
