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
            var lookingDirection = PlayerEyes.GetWorldLocation() - transform.position;
            eye.transform.rotation =  Quaternion.LookRotation(lookingDirection);
            //eye.transform.localEulerAngles = new Vector3(
            //    Mathf.Clamp(eye.transform.localEulerAngles.x, -50,50),
            //    Mathf.Clamp(eye.transform.localEulerAngles.y, -50, 50),
            //    0
            //);

        });
    }
}