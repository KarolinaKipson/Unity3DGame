using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // EnemyHealth + EnemyMovement
    private GameObject playerFollowed;

    private Animator animator;
    private Rigidbody rigidbody;
    private Collider collider;

    public float searchRadius; //distance to start moving
    public float speed; //speed to move at
    public float minDistance = 5f;

    public float health;
    public float damage;

    public float playerDamage;
    private bool canAttack = true;

    // public int score;

    // Start is called before the first frame update
    private void Start()
    {
        playerFollowed = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        speed = 1f;
        searchRadius = 10f;
        playerDamage = 10f;
        health = 30f;
        damage = 30f;
    }

    // Update is called once per frame
    private void Update()
    {
        playerDamage = CalculatePlayerDamage();
        Debug.Log("Player damage " + CalculatePlayerDamage());
        searchRadius = CalculatePlayerDamage();
        float dist_ = Vector3.Distance(playerFollowed.transform.position, transform.position); //find distance
        if (dist_ <= searchRadius && dist_ > minDistance && gameObject.activeSelf)
        {
            Vector3 direction = playerFollowed.transform.position - this.transform.position;
            direction.y = 0;
            // Turn toward player.
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            ChasePlayer();
        }

        if (dist_ < minDistance && health > 0 && gameObject.activeSelf)
        {
            AttackPlayer();
        }

        if (dist_ <= 2f)
        {
            speed = 0;
        }
    }

    public float CalculatePlayerDamage()
    {
        if (GameManager.instance.score <= 10)
        {
            return 10f;
        }
        else if (GameManager.instance.score <= 20)
        {
            return 20f;
        }
        else
        {
            return 30f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Arrow" && gameObject.activeSelf)
        {
            takeDamage(damage);
        }
    }

    public void AttackPlayer()
    {
        speed = 0f;
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        StartCoroutine(AttackTime());
    }

    private IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(5f);
        PlayerHealth.instance.DamagePlayer(playerDamage);
        yield return new WaitForSeconds(5f);
        canAttack = true;
    }

    public void ChasePlayer()
    {
        animator.SetBool("isStanding", false);
        animator.SetBool("isWalking", true);
        rigidbody.transform.position = Vector3.MoveTowards(transform.position, playerFollowed.transform.position, speed * Time.deltaTime); //move towards player
    }

    private IEnumerator DieAnimation()
    {
        animator.SetBool("isDead", true);
        GameManager.instance.score += 10;
        yield return new WaitForSeconds(40f);
        gameObject.SetActive(false);
    }

    public void Die()
    {
        StartCoroutine(DieAnimation());
    }

    public void DisableEnemy()
    {
        canAttack = false;
        animator.SetBool("isStanding", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    public void takeDamage(float damage)
    {
        canAttack = false;
        if (health <= 0)
        {
            health = 0;
            speed = 0;
            collider.enabled = false;
            Die();
        }
        else
        {
            health -= damage;
        }
    }
}