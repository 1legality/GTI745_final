using UnityEngine;

public class AbilityMovement: AbilityBase
{
    public override Abilities Ability => Abilities.Movement;

    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _rotationSpeed = 250.0f;


    public Transform playerTransform;
    [SerializeField] private float _minThreshold = 50.0f;
    [SerializeField] private float _maxThreshold = 50.0f;

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
        //Vector3 MousePosition = PlayerEyes.GetEyesWorldLocation();
        Vector2 MousePosition = PlayerEyes.GetEyesWorldLocation();

        if(MousePosition.x > Screen.width - _maxThreshold && MousePosition.y > Screen.height - _maxThreshold)
        {
            return;
        }
        else if (MousePosition.x < _maxThreshold && MousePosition.y > Screen.height - _maxThreshold)
        {
            return;
        }
        else if (MousePosition.x > Screen.width - _maxThreshold && MousePosition.y < _maxThreshold)
        {
            return;
        }
        else if (MousePosition.x < _maxThreshold && MousePosition.y < _maxThreshold)
        {
            return;
        }

        if (MousePosition.x > Screen.width / 2 + _minThreshold)
        {
            playerTransform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        else if (MousePosition.x < Screen.width / 2 - _minThreshold)
        {
            playerTransform.Rotate(Vector3.up * -_rotationSpeed * Time.deltaTime);
        }
        else if (MousePosition.y > Screen.height/2 + _minThreshold)
        {
            playerTransform.position += playerTransform.forward * Time.deltaTime * _movementSpeed;
        }
        else if (MousePosition.y < Screen.height/2 - _minThreshold)
        {
            playerTransform.position += -playerTransform.forward * Time.deltaTime * _movementSpeed;
        }

        /*if (distance > _yThresholdUp)
        {
            playerTransform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        else if (distance < _yThresholdBottom)
        {
            playerTransform.Rotate(Vector3.up * -_rotationSpeed * Time.deltaTime);
        }

        if (angle > 30.0f)
        {
            playerTransform.position += playerTransform.forward * Time.deltaTime * _movementSpeed;
        }

        if (angle < 20.0f)
        {
            playerTransform.position += -playerTransform.forward * Time.deltaTime * _movementSpeed;
        }*/
    }

    private void CalculateHorizontalInput()
    {
        Vector3 MousePosition = PlayerEyes.GetEyesWorldLocation();

        Vector3 PlayerCenter = playerTransform.position;
        PlayerCenter.y = MousePosition.y;

        float distance = Vector3.Distance(PlayerCenter, MousePosition);
        float angle = Vector3.Angle(PlayerCenter, MousePosition);

        Camera cam = Camera.main;
        Vector2 screen = cam.WorldToScreenPoint(MousePosition);

        /*if (distance > _xThresholdRight)
        {
            playerTransform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        else if (distance < _xThresholdLeft)
        {
            playerTransform.Rotate(Vector3.up * -_rotationSpeed * Time.deltaTime);
        }

        if (angle > 70.0f)
        {
            playerTransform.Rotate(Vector3.up  * _rotationSpeed * Time.deltaTime);
        }

        if (angle < 40.0f)
        {
            playerTransform.Rotate(Vector3.up * -1 * _rotationSpeed * Time.deltaTime);
        }*/
    }
   
}