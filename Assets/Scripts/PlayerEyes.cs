using UnityEngine;
using Tobii.Gaming;

public class PlayerEyes : MonoBehaviour
{
    private static PlayerEyes _playerEyes = null;

    private static Vector3 _historicPoint = new Vector3();

    //private static float FilterSmoothingFactor = 0.15f;
    //private static float VisualizationDistance = 10f;

    public static bool UseMouseAsInput { get; set; }

    private void Awake()
    {
        if (!_playerEyes)
            _playerEyes = this;
    }

    // TODO Replace with TOBI LOCATION
    public static Vector2 GetEyesLocation()
    {
        if (UseMouseAsInput)
            return Input.mousePosition;
        /*GazePoint gazePoint = TobiiAPI.GetGazePoint();

        Vector3 gazeOnScreen = gazePoint.Screen;

        // gazeOnScreen += (oldPostion * VisualizationDistance);
        Vector3 gazePointInWorld = Camera.main.ScreenToWorldPoint(gazeOnScreen);

        _historicPoint = gazePointInWorld;

        var smoothedPoint = new Vector3(
            gazePointInWorld.x * (1.0f - FilterSmoothingFactor) + _historicPoint.x * FilterSmoothingFactor,
            gazePointInWorld.y * (1.0f - FilterSmoothingFactor) + _historicPoint.y * FilterSmoothingFactor,
            gazePointInWorld.z * (1.0f - FilterSmoothingFactor) + _historicPoint.z * FilterSmoothingFactor);

        oldPostion = smoothedPoint;

        Vector2 position = smoothedPoint;

        return position;*/

        //transform.position = Smoothify(gazePointInWorld);

        //return Input.mousePosition;

        if(GameObject.Find("GazePlot"))
        {
            GameObject gazePlot = GameObject.Find("GazePlot");
            GazePlotter gaszePlotter = gazePlot.GetComponent<GazePlotter>();

            return gaszePlotter._lastGazePoint.Screen;
        }

        if(GameObject.Find("PointCloudSprite0"))
        {
            return GameObject.Find("PointCloudSprite0").transform.position;
        }

        return _historicPoint;
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
