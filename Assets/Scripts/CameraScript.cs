using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 
            Player.transform.position.z - 4);
    }
}

