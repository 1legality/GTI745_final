using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _rotationSpeed = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation =
                Quaternion.LookRotation(
                    Vector3.RotateTowards(transform.forward, -transform.right, _rotationSpeed, 0.0f));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation =
                Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, transform.right, _rotationSpeed,
                    0.0f));
        }
    }
}
