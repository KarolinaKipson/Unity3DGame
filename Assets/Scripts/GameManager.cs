using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public GameObject winGame;
    public GameObject player;

    // Singleton.
    private void manageGameManager()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        manageGameManager();
    }

    private void Start()
    {
        score = 0;
        ScoreCounter();
    }

    private void Update()
    {
        ScoreCounter();
        if (player != null)
        {
            Debug.Log("Player position " + player.transform.position.x.ToString() + player.transform.position.y.ToString() + player.transform.position.z.ToString());
            Vector3 position = new Vector3(player.transform.position.x + 1f, player.transform.position.y, player.transform.position.z + 1f);

            if (score > 40)
            {
                EnemyPool.deactivateCossacks();
                EnemyPool.deactivateKnights();
                EnemyPool.deactivateMutants();
                Instantiate(winGame, position, Quaternion.identity);
            }
        }
    }

    public void ScoreCounter()
    {
        score.ToString();
    }
}