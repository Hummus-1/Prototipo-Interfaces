using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDetector : MonoBehaviour
{
    Camera slimeCamera;
    Plane[] cameraFrustum;
    Collider slimeCollider;

    public bool slimeBehind;

    // Start is called before the first frame update
    void Start()
    {
        slimeCamera = Camera.main;
        slimeCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        var bounds = slimeCollider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(slimeCamera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds)) {
            slimeBehind = false;
        } else {
            slimeBehind = true;
        }
    }
}
