using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    private static PlayerEyes _playerEyes = null;

    private void Awake()
    {
        if (!_playerEyes)
            _playerEyes = this;
    }

    // TODO Replace with TOBI LOCATION
    private static Vector2 GetEyesLocation()
    {
        return Input.mousePosition;
    }

    public static Vector3 GetWorldLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetEyesLocation());

        RaycastHit hit;
        return Physics.Raycast(ray, out hit)
            ? hit.point
            : Vector3.zero;
    }

    public static Vector3 GetWorldLocationOnPlane()
    {
        var value = Camera.main.ScreenToWorldPoint(new Vector3(GetEyesLocation().x,  GetEyesLocation().y , Camera.main.nearClipPlane));
        Debug.Log(value);
        return value;
    }
}
