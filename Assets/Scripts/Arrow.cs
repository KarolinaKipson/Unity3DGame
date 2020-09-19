using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float lifeTime = 2f;
    private float timer;
    private bool hitMade = false;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        if (!hitMade)
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Arrow")
        {
            hitMade = true;
            Debug.Log("Hit: " + collision.gameObject.name);
            if (collision.gameObject.activeSelf)
            {
                stickArrow();
            }
        }

        if (collision.collider.tag != "Enemy")
        {
        }
    }

    private void stickArrow()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}