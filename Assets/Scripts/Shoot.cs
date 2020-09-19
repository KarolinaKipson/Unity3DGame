using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera camera;
    public GameObject arrow;
    public Transform arrowSpawn;
    public float shootForce = 50f;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //draw  animation
            GameObject shooting = Instantiate(arrow, arrowSpawn.position, camera.transform.rotation);
            Rigidbody rigidbody = shooting.GetComponent<Rigidbody>();
            rigidbody.velocity = camera.transform.forward * shootForce;
        }

        //LMB up play release animation
    }
}