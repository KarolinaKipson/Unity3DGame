using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;
    public Slider slider;
    public TextMeshProUGUI textMesh;
    public LevelManager levelManager;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        currentHealth = maxHealth;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        slider.value = 1;
        textMesh.text = "Score: " + GameManager.instance.score;
    }

    // Update is called once per frame
    private void Update()
    {
        textMesh.text = "Score: " + GameManager.instance.score;
        slider.value = currentHealth / 100;
        Debug.Log("Health: " + currentHealth.ToString());
        Debug.Log("Score:" + GameManager.instance.score.ToString());
    }

    public void DamagePlayer(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        currentHealth = 0;
        isDead = true;
        slider.value = 0;
        levelManager.LoadGameOverScene();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            DamagePlayer(5f);
        }

        if (collision.gameObject.tag == "MagicTree")
        {
            levelManager.LoadWinScene();
        }
    }
}