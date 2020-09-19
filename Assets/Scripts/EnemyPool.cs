using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static int numEnemies = 30;
    public GameObject enemyCossack;
    public GameObject enemyMutant;
    public GameObject enemyKnight;

    private static GameObject[] cossacks;
    private static GameObject[] knights;
    private static GameObject[] mutants;

    // Start is called before the first frame update
    private void Awake()
    {
        cossacks = new GameObject[numEnemies];

        for (int i = 0; i < numEnemies; i++)
        {
            cossacks[i] = Instantiate(enemyCossack, Vector3.zero, Quaternion.identity);
            cossacks[i].SetActive(false);
        }

        mutants = new GameObject[numEnemies];
        for (int i = 0; i < numEnemies; i++)
        {
            mutants[i] = Instantiate(enemyMutant, Vector3.zero, Quaternion.identity);
            mutants[i].SetActive(false);
        }

        knights = new GameObject[numEnemies];
        for (int i = 0; i < numEnemies; i++)
        {
            knights[i] = Instantiate(enemyKnight, Vector3.zero, Quaternion.identity);
            knights[i].SetActive(false);
        }
    }

    static public GameObject getCossack()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (!cossacks[i].activeSelf)
            {
                return cossacks[i];
            }
        }
        return null;
    }

    static public GameObject getMutant()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (!mutants[i].activeSelf)
            {
                return mutants[i];
            }
        }
        return null;
    }

    static public GameObject getKnight()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (!knights[i].activeSelf)
            {
                return knights[i];
            }
        }
        return null;
    }

    static public void deactivateKnights()
    {
        for (int i = 0; i < knights.Length; i++)
        {
            if (knights[i] != null)
            {
                knights[i].SetActive(false);
            }
        }
    }

    static public void deactivateCossacks()
    {
        for (int i = 0; i < cossacks.Length; i++)
        {
            if (cossacks[i] != null)
            {
                cossacks[i].SetActive(false);
            }
        }
    }

    static public void deactivateMutants()
    {
        for (int i = 0; i < mutants.Length; i++)
        {
            if (mutants[i] != null)
            {
                mutants[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Rigidbody rigidbody = knights[i].AddComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        for (int i = 0; i < numEnemies; i++)
        {
            Rigidbody rigidbody = mutants[i].AddComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        for (int i = 0; i < numEnemies; i++)
        {
            Rigidbody rigidbody = cossacks[i].AddComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}