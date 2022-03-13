using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GameObject currency;
    GroundTileSpawner groundTileSpawner;
    private GameObject[] GroundTiles = new GameObject[5];
    GameObject curr;

    private void Awake()
    {
        GroundTiles[0] = (GameObject)Resources.Load("Prefabs/Ground_1", typeof(GameObject));
        GroundTiles[1] = (GameObject)Resources.Load("Prefabs/Ground_2", typeof(GameObject));
        GroundTiles[2] = (GameObject)Resources.Load("Prefabs/Ground_3", typeof(GameObject));
        GroundTiles[3] = (GameObject)Resources.Load("Prefabs/Ground_4", typeof(GameObject));
        GroundTiles[4] = (GameObject)Resources.Load("Prefabs/Ground_5", typeof(GameObject));
        currency = (GameObject)Resources.Load("Prefabs/Currency", typeof(GameObject));
    }

    void Start()
    {
        groundTileSpawner = GameObject.FindObjectOfType<GroundTileSpawner>();
        if (Random.value > 0.2) 
            curr = Instantiate(currency, new Vector3(gameObject.transform.position.x, 1.5f ,gameObject.transform.position.z), Quaternion.identity);
    }

    private void OnTriggerExit(Collider other)
    {
        int randomTile = Random.Range(0, GroundTiles.Length);
        groundTileSpawner.SpawnTile(GroundTiles[randomTile]);
        Destroy(gameObject, 5);
    }



}
