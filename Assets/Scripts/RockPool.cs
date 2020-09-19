using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    private static int numRocks = 100;
    public GameObject rock;
    private static GameObject[] rocks;

    // Start is called before the first frame update
    private void Start()
    {
        rocks = new GameObject[numRocks];
        for (int i = 0; i < numRocks; i++)
        {
            rocks[i] = Instantiate(rock, Vector3.zero, Quaternion.identity);
            rocks[i].SetActive(false);
        }
    }

    static public GameObject getRock()
    {
        for (int i = 0; i < numRocks; i++)
        {
            if (!rocks[i].activeSelf)
            {
                return rocks[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}