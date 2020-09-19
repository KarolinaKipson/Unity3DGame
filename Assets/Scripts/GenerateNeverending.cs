using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNeverending : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;

    private int planeSize = 10;
    private int halfTilesX = 5;
    private int halfTilesZ = 5;

    private Vector3 startPosition;

    private Hashtable tiles = new Hashtable();

    // Start is called before the first frame update
    private void Start()
    {
        this.gameObject.transform.position = Vector3.zero;
        startPosition = Vector3.zero;
        float updateTime = Time.realtimeSinceStartup;

        for (int x = -halfTilesX; x < halfTilesX; x++)
        {
            for (int z = -halfTilesZ; z < halfTilesZ; z++)
            {
                Vector3 position = new Vector3((x * planeSize + startPosition.x), 0,
                                               (z * planeSize + startPosition.z));
                GameObject tileRaw = Instantiate(plane, position, Quaternion.identity);
                string tileName = "Tile_" + ((int)position.x).ToString() + "_" + ((int)position.z).ToString();
                tileRaw.name = tileName;

                Tile tile = new Tile(tileRaw, updateTime);
                tiles.Add(tileName, tile);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Player movement.
        int xMove = (int)(player.transform.position.x - startPosition.x);
        int zMove = (int)(player.transform.position.z - startPosition.z);

        if (Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize)
        {
            float updateTime = Time.realtimeSinceStartup;

            int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

            for (int x = -halfTilesX; x < halfTilesX; x++)
            {
                for (int z = -halfTilesZ; z < halfTilesZ; z++)
                {
                    Vector3 position = new Vector3((x * planeSize + playerX), 0,
                                                                  (z * planeSize + playerZ));

                    string tileName = "Tile_" + ((int)position.x).ToString() + "_" + ((int)position.z).ToString();

                    if (!tiles.ContainsKey(tileName))
                    {
                        GameObject tileRaw = Instantiate(plane, position, Quaternion.identity);
                        tileRaw.name = tileName;

                        Tile tile = new Tile(tileRaw, updateTime);
                        tiles.Add(tileName, tile);
                    }
                    else
                    {
                        (tiles[tileName] as Tile).creationTime = updateTime;
                    }
                }
            }

            // Destroy updated tiles and put new tiles in hashtable
            Hashtable newTerrain = new Hashtable();
            foreach (Tile item in tiles.Values)
            {
                if (item.creationTime != updateTime)
                {
                    Destroy(item.tileGameObject);
                }
                else
                {
                    newTerrain.Add(item.tileGameObject, item);
                }
            }
            // copy to tiles
            tiles = newTerrain;

            startPosition = player.transform.position;
        }
    }
}