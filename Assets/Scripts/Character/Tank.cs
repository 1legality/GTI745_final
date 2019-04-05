using UnityEngine;

public class Tank: MonoBehaviour
{
    [SerializeField] private GameObject _fireSocket = null;
    [SerializeField] private GameObject _tower = null;
    [SerializeField] private GameObject _barrel = null;

    private void Update()
    {
        if (PlayerEyes.GetWorldLocation() == Vector3.zero) return;

        var dir = PlayerEyes.GetWorldLocation() - _fireSocket.transform.position;
        var rot = Quaternion.LookRotation(dir).eulerAngles;

        var towerRot = _tower.transform.rotation.eulerAngles;
        towerRot.y = rot.y + 90.0f;
        _tower.transform.rotation = Quaternion.Euler(towerRot);

        var barrelRot = _barrel.transform.rotation.eulerAngles;
        barrelRot.x = rot.x - 90.0f;
        _barrel.transform.rotation = Quaternion.Euler(barrelRot);
    }
}