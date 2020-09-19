using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubPool : MonoBehaviour
{
    private static int numShrubs = 100;
    public GameObject shrub;
    private static GameObject[] shrubs;

    // Start is called before the first frame update
    private void Awake()
    {
        shrubs = new GameObject[numShrubs];
        for (int i = 0; i < numShrubs; i++)
        {
            shrubs[i] = Instantiate(shrub, Vector3.zero, Quaternion.identity);
            shrubs[i].SetActive(false);
        }
    }

    static public GameObject getShrub()
    {
        for (int i = 0; i < numShrubs; i++)
        {
            if (!shrubs[i].activeSelf)
            {
                return shrubs[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}