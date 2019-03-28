using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private bool isMovingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(isMovingLeft ? -1F : 1F, 0.0F);

        rb.position += movement * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Gotcha! Should reset the game.
            //other.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isMovingLeft = !isMovingLeft;
        }
    }
}
