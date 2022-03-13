using UnityEngine;

public class GroundTileSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public GameObject Grounds;
    Vector3 nextSpawnPoint;

    public void SpawnTile(GameObject gameObject)
    {
        GameObject temp = Instantiate(gameObject, nextSpawnPoint, Quaternion.identity);
        temp.transform.SetParent(Grounds.transform);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnTile(groundTile);
        }
    }

    

}
