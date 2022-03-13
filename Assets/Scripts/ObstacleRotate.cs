using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotate : MonoBehaviour
{
    private float degreesPerSec;
    private float dir;

    private void Start()
    {
        if (Random.Range(0, 2) == 0) dir = -1;
        else dir = 1;
    }

    void Update()
    {
        degreesPerSec = PlayerPrefs.GetFloat("PlayerSpeed") * 50;
        transform.Rotate(new Vector3(0,0,(degreesPerSec) * dir) * Time.deltaTime);
    }
}

