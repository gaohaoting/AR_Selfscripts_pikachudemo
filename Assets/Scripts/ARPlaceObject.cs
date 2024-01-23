using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlaceObject : MonoBehaviour
{
    // Start is called before the first frame update
    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;

    private bool isObjectPlaced = false;

    void Start()
    {
        // Get AR components from AR Session Origin
        ARSessionOrigin arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        arRaycastManager = arSessionOrigin.GetComponent<ARRaycastManager>();
        arPlaneManager = arSessionOrigin.GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        // Check if the object is not yet placed
        if (!isObjectPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Raycast to the AR plane
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                // Place the object at the hit point
                transform.position = hits[0].pose.position;

                // Disable all AR planes
                DisableAllARPlanes();

                // Set the flag to indicate that the object is placed
                isObjectPlaced = true;
            }
        }
    }

    // Function to disable all AR planes
    private void DisableAllARPlanes()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            // Disable the GameObject associated with each AR plane
            plane.gameObject.SetActive(false);
        }
    }
}
