using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    private static PlayerEyes _playerEyes = null;

    private void Awake()
    {
        if (!_playerEyes)
            _playerEyes = this;
    }

    public static Vector3 GetWorldLocation()
    {
        // TODO Replace with TOBI LOCATION
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        return Physics.Raycast(ray, out hit)
            ? hit.point
            : Vector3.zero;
    }
}
