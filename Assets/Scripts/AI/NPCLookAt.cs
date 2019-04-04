using System.Collections.Generic;
using UnityEngine;

public class NPCLookAt: MonoBehaviour
{
    [SerializeField] private List<GameObject> _eyes = null;
    [SerializeField] private Vector2 _yawLimit = Vector2.zero;
    [SerializeField] private Vector2 _pitchLimit = Vector2.zero;

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
                //euler.x > 200
                //    ? Mathf.Clamp(euler.x, 360 + _pitchLimit.x, 360 + _pitchLimit.x)
                //    : Mathf.Clamp(euler.x, _pitchLimit.x, _pitchLimit.x),
                Mathf.Clamp(euler.x, _pitchLimit.x, _pitchLimit.y),
                euler.y > 200
                    ? Mathf.Clamp(euler.y, 360 + _yawLimit.x, 360 +_yawLimit.y)
                    : Mathf.Clamp(euler.y, _yawLimit.x, _yawLimit.y),
                euler.z
            );
        });
    }
}