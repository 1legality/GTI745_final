using System.Collections.Generic;
using UnityEngine;

public class NPCLookAt: MonoBehaviour
{
    [SerializeField] private List<GameObject> _eyes;

    private void Update()
    {
        UpdateEyes();
    }

    private void UpdateEyes()
    {
        _eyes.ForEach(eye =>
        {
            var lookingDirection = (PlayerEyes.GetWorldLocationOnPlane() * Vector3.Distance(Camera.main.transform.position, transform.position) - eye.transform.position);
            Debug.DrawLine(eye.transform.position, PlayerEyes.GetWorldLocationOnPlane(), Color.red);
            eye.transform.rotation =  Quaternion.LookRotation(lookingDirection);
        });
    }
}