using UnityEngine;

public class AbilityMovement: AbilityBase
{
    public override Abilities Ability => Abilities.Movement;

    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _rotationSpeed = 250.0f;

    public Transform playerTransform;

    public override void OnActivated()
    {
        base.OnActivated();

        Debug.Log("Actiae\ted");

        playerTransform = transform.parent.parent.transform;
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
    }

    private void Update()
    {
        Debug.Log("Eye position : " + PlayerEyes.GetEyesWorldLocation());

        CalculateVerticalInput();
        CalculateHorizontalInput();
        //transform.parent.parent.transform.position += transform.forward * Time.deltaTime * _movementSpeed; ;

        //transform.parent.parent.transform.position += transform.forward * Time.deltaTime * 100f;

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("ALLO");
            playerTransform.position += playerTransform.forward * Time.deltaTime * _movementSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerTransform.position += -playerTransform.forward * Time.deltaTime * _movementSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            /*playerTransform.rotation =
                Quaternion.LookRotation(
                    Vector3.RotateTowards(playerTransform.forward, -playerTransform.right, _rotationSpeed, 0.0f));*/

            playerTransform.Rotate(Vector3.up * -1 * _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            /*playerTransform.rotation =
                Quaternion.LookRotation(Vector3.RotateTowards(playerTransform.forward, playerTransform.right, _rotationSpeed,
                    0.0f));*/

            playerTransform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
    }

    private void CalculateVerticalInput()
    {
        Vector3 MousePosition = PlayerEyes.GetEyesWorldLocation();

        Vector3 PlayerCenter = playerTransform.position;
        PlayerCenter.x = MousePosition.x;
        PlayerCenter.z = MousePosition.z;

        float angle = Vector3.Angle(PlayerCenter, MousePosition);

        if (angle > 70.0f)
        {
            playerTransform.position += playerTransform.forward * Time.deltaTime * _movementSpeed;
        }

        if (angle < 40.0f)
        {
            playerTransform.position += -playerTransform.forward * Time.deltaTime * _movementSpeed;
        }
    }

    private void CalculateHorizontalInput()
    {
        Vector3 MousePosition = PlayerEyes.GetEyesWorldLocation();

        Vector3 PlayerCenter = playerTransform.position;
        PlayerCenter.y = MousePosition.y;

        float angle = Vector3.Angle(PlayerCenter, MousePosition);

        if (angle > 70.0f)
        {
            /*playerTransform.rotation =
                Quaternion.LookRotation(
                    Vector3.RotateTowards(playerTransform.forward, -playerTransform.right, _rotationSpeed, 0.0f));*/

            playerTransform.Rotate(Vector3.up  * _rotationSpeed * Time.deltaTime);
        }

        if (angle < 40.0f)
        {
            /*playerTransform.rotation =
                Quaternion.LookRotation(Vector3.RotateTowards(playerTransform.forward, playerTransform.right, _rotationSpeed,
                    0.0f));*/

            playerTransform.Rotate(Vector3.up * -1 * _rotationSpeed * Time.deltaTime);
        }
    }
   
}