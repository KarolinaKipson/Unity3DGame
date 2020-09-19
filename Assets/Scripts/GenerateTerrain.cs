using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GenerateTerrain : MonoBehaviour
{
    private GameObject player;
    private MeshCollider meshCollider;
    private Mesh mesh;
    private int height = 2;
    private float detail = 6.0f;
    private List<GameObject> terrainTrees = new List<GameObject>();
    private List<GameObject> terrainShrubs = new List<GameObject>();
    private List<GameObject> terrainRocks = new List<GameObject>();

    private List<GameObject> terrainKnights = new List<GameObject>();
    private List<GameObject> terrainMutants = new List<GameObject>();
    private List<GameObject> terrainCossacks = new List<GameObject>();

    private Vector3 cossackPosition;
    private Vector3 knightPosition;
    private Vector3 mutantPosition;

    private int posMin;
    private int posMax;
    private float percentage;
    private float startTime;
    private float repeatRate;

    // Start is called before the first frame update
    private void Awake()
    {
        posMin = 20;
        posMax = 40;
        percentage = 4.7f;
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = 10f;

        int xMove = (int)(player.transform.position.x - this.transform.position.x);
        int zMove = (int)(player.transform.position.z - this.transform.position.z);

        mesh = this.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Mathf.PerlinNoise((vertices[i].x + this.transform.position.x) / detail,
                (vertices[i].z + this.transform.position.z) / detail) * height;

            if (vertices[i].y > 1.0 && vertices[i].y < 1.1
                && Mathf.PerlinNoise((vertices[i].x + 5) / 10, (vertices[i].z + 5) / 10) * 10 > 4.5)
            {
                GameObject newTree = TreePool.getTree();
                if (newTree != null)
                {
                    Vector3 treePosition = new Vector3(vertices[i].x + this.transform.position.x,
                        vertices[i].y - 0.3f,
                                                  vertices[i].z + this.transform.position.z);
                    newTree.transform.position = treePosition;
                    string treeName = "Tree_" + ((int)treePosition.x).ToString()
                        + "_" + ((int)treePosition.z).ToString();
                    newTree.name = treeName;
                    newTree.SetActive(true);
                    // Debug.Log("Tree " + newTree.name + " active");
                    newTree.GetComponent<Collider>().enabled = true;
                    terrainTrees.Add(newTree);
                }
            }

            if (vertices[i].y < 1.3 && vertices[i].y > 1.28
               && Mathf.PerlinNoise((vertices[i].x + 5) / 10, (vertices[i].z + 5) / 10) * 10 > 4.8)
            {
                GameObject newShrub = ShrubPool.getShrub();
                if (newShrub != null)
                {
                    Vector3 shrubPosition = new Vector3(vertices[i].x + this.transform.position.x,
                        vertices[i].y - 0.1f,
                                                  vertices[i].z + this.transform.position.z);
                    newShrub.transform.position = shrubPosition;
                    string deadTreeName = "DeadTree_" + ((int)shrubPosition.x).ToString()
                      + "_" + ((int)shrubPosition.z).ToString();
                    newShrub.name = deadTreeName;
                    newShrub.SetActive(true);
                    terrainShrubs.Add(newShrub);
                }
            }

            if (vertices[i].y > 1.9 && vertices[i].y < 1.8
            && Mathf.PerlinNoise((vertices[i].x + 5) / 10, (vertices[i].z + 5) / 10) * 10 > 4.7)
            {
                GameObject newRock = RockPool.getRock();
                if (newRock != null)
                {
                    Vector3 rockPosition = new Vector3(vertices[i].x + this.transform.position.x,
                        vertices[i].y - 0.3f,
                                                  vertices[i].z + this.transform.position.z);
                    newRock.transform.position = rockPosition;
                    newRock.SetActive(true);
                    terrainRocks.Add(newRock);
                }
            }
            if (vertices[i].y > 1.4 && vertices[i].y < 1.45 && UnityEngine.Random.value > 0.9
                && zMove > 10 && xMove > 10)
            {
                cossackPosition = new Vector3(vertices[i].x + this.transform.position.x,
                 vertices[i].y,
                                           vertices[i].z + this.transform.position.z);

                if (GameManager.instance.score >= 0)
                {
                    CancelInvoke("ActivateCossack");
                    InvokeRepeating("ActivateCossack", 1f, 3f);
                }
                if (GameManager.instance.score >= 10)
                {
                    CancelInvoke("ActivateCossack");
                    InvokeRepeating("ActivateCossack", 5f, 2f);
                }
                if (GameManager.instance.score >= 20)
                {
                    CancelInvoke("ActivateCossack");
                    InvokeRepeating("ActivateCossack", 3f, 1f);
                }
            }

            if (vertices[i].y > 1.5 && vertices[i].y < 1.55 && UnityEngine.Random.value > 0.9
                && zMove > 20 && xMove > 20)
            {
                mutantPosition = new Vector3(vertices[i].x + this.transform.position.x,
                           vertices[i].y, vertices[i].z + this.transform.position.z);
                if (GameManager.instance.score >= 0)
                {
                    CancelInvoke("ActivateMutant");
                    InvokeRepeating("ActivateMutant", 1f, 3f);
                }
                if (GameManager.instance.score >= 10)
                {
                    CancelInvoke("ActivateMutant");
                    InvokeRepeating("ActivateMutant", 5f, 2f);
                }
                if (GameManager.instance.score >= 20)
                {
                    CancelInvoke("ActivateMutant");
                    InvokeRepeating("ActivateMutant", 3f, 1f);
                }
            }

            if (vertices[i].y > 1.3 && vertices[i].y < 1.4 && UnityEngine.Random.value > 0.9
                && zMove > 10 && xMove > 10)
            {
                knightPosition =
                     new Vector3(vertices[i].x + this.transform.position.x,
                     vertices[i].y,
                                               vertices[i].z + this.transform.position.z);
                if (GameManager.instance.score >= 0)
                {
                    CancelInvoke("ActivateKnight");
                    InvokeRepeating("ActivateKnight", 3f, 5f);
                }
                if (GameManager.instance.score >= 10)
                {
                    CancelInvoke("ActivateKnight");
                    InvokeRepeating("ActivateKnight", 2f, 4f);
                }
                if (GameManager.instance.score >= 20)
                {
                    CancelInvoke("ActivateKnight");
                    InvokeRepeating("ActivateKnight", 1f, 2f);
                }
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.meshCollider = this.gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < terrainTrees.Count; i++)
        {
            if (terrainTrees[i] != null)
            {
                terrainTrees[i].SetActive(false);
            }
        }

        for (int j = 0; j < terrainShrubs.Count; j++)
        {
            if (terrainShrubs[j] != null)
            {
                terrainShrubs[j].SetActive(false);
            }
        }

        for (int k = 0; k < terrainRocks.Count; k++)
        {
            if (terrainRocks[k] != null)
            {
                terrainRocks[k].SetActive(false);
            }
        }
        for (int l = 0; l < terrainCossacks.Count; l++)
        {
            if (terrainCossacks[l] != null)
            {
                terrainCossacks[l].SetActive(false);
            }
        }

        for (int m = 0; m < terrainMutants.Count; m++)
        {
            if (terrainMutants[m] != null)
            {
                terrainMutants[m].SetActive(false);
            }
        }
        for (int n = 0; n < terrainKnights.Count; n++)
        {
            if (terrainKnights[n] != null)
            {
                terrainKnights[n].SetActive(false);
            }
        }
    }

    public void ActivateCossack()
    {
        GameObject newCossack = EnemyPool.getCossack();
        if (newCossack != null)
        {
            foreach (GameObject obj in terrainCossacks)
            {
                float distanceX = obj.transform.position.x - cossackPosition.x;
                float distanceZ = obj.transform.position.z - cossackPosition.z;
                if (Mathf.Abs(distanceX) > 3 && Mathf.Abs(distanceZ) > 3)
                {
                    newCossack.transform.position = cossackPosition;
                }
                else
                {
                    continue;
                }
            }

            newCossack.SetActive(true);
            terrainCossacks.Add(newCossack);
        }
    }

    public void ActivateKnight()
    {
        GameObject newKnight = EnemyPool.getKnight();
        if (newKnight != null)
        {
            foreach (GameObject obj in terrainKnights)
            {
                float distanceX = obj.transform.position.x - knightPosition.x;
                float distanceZ = obj.transform.position.z - knightPosition.z;
                if (Mathf.Abs(distanceX) > 3 && Mathf.Abs(distanceZ) > 3)
                {
                    newKnight.transform.position = knightPosition;
                }
                else
                {
                    continue;
                }
            }

            newKnight.transform.position = knightPosition;
            newKnight.SetActive(true);
            terrainKnights.Add(newKnight);
        }
    }

    public void ActivateMutant()
    {
        GameObject newMutant = EnemyPool.getMutant();
        if (newMutant != null)
        {
            foreach (GameObject obj in terrainMutants)
            {
                float distanceX = obj.transform.position.x - mutantPosition.x;
                float distanceZ = obj.transform.position.z - mutantPosition.z;
                if (Mathf.Abs(distanceX) > 3 && Mathf.Abs(distanceZ) > 3)
                {
                    newMutant.transform.position = mutantPosition;
                }
                else
                {
                    continue;
                }
            }

            newMutant.transform.position = mutantPosition;
            newMutant.SetActive(true);
            terrainMutants.Add(newMutant);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        this.meshCollider.sharedMesh = mesh;
    }
}