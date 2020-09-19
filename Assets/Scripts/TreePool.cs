using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePool : MonoBehaviour
{
    private static int numTrees = 1000;
    public GameObject tree;
    private static GameObject[] trees;

    // Start is called before the first frame update
    private void Awake()
    {
        trees = new GameObject[numTrees];
        for (int i = 0; i < numTrees; i++)
        {
            trees[i] = Instantiate(tree, Vector3.zero, Quaternion.identity);
            trees[i].SetActive(false);
            trees[i].GetComponent<Collider>().enabled = false;
            // Debug.Log("Collider " + trees[i].GetComponent<Collider>().name + " " +
            //      trees[i].GetComponent<Collider>().enabled);
        }
    }

    static public GameObject getTree()
    {
        for (int i = 0; i < numTrees; i++)
        {
            if (!trees[i].activeSelf)
            {
                //trees[i].GetComponent<Collider>().enabled = true;
                return trees[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}