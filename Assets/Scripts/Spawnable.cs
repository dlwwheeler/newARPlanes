﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public float Distance;
    public float Height;
    public float Variance;
    public float rotVariance;
    public virtual GameObject spawn(GameObject Player){
        float y =Player.transform.eulerAngles.y;
        rotVariance=Random.Range(-rotVariance,rotVariance);
        gameObject.transform.position = Player.transform.position;
        gameObject.transform.position += new Vector3(Mathf.Sin(y+rotVariance)*Distance + Random.Range(-Variance,Variance),Height, Mathf.Cos(y+rotVariance)*Distance + Random.Range(-Variance,Variance));
        transform.LookAt(Player.transform);
        gameObject.SetActive(true);
        return gameObject;
    }
}
