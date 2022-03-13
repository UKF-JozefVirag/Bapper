using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{ 
    void Update()
    {
        transform.Rotate(0, PlayerPrefs.GetFloat("PlayerSpeed") * 10 * Time.deltaTime, 0);
    }
}
