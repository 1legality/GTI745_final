using System.Collections.Generic;
using UnityEngine;

public class NPCLookAt: MonoBehaviour
{
    [SerializeField] private List<GameObject> _eyes;
    [SerializeField] private Vector2 _yawLimit;
    [SerializeField] private Vector2 _pitchLimit;

    private void Update()
    {
        UpdateEyes();
    }

    private void UpdateEyes()
    {
        _eyes.ForEach(eye =>
        {
            var lookingDirection = PlayerEyes.GetWorldLocation() - transform.position;
            eye.transform.rotation = Quaternion.LookRotation(lookingDirection);

            var euler = eye.transform.localEulerAngles;
            eye.transform.localEulerAngles = new Vector3(
                Mathf.Clamp(euler.x, _pitchLimit.x, _yawLimit.y),
                euler.y > 200
                    ? Mathf.Clamp(euler.y, 360 + _yawLimit.x, 360 +_yawLimit.y)
                    : Mathf.Clamp(euler.y, _yawLimit.x, _yawLimit.y),
                euler.z
            );
        });
    }
}