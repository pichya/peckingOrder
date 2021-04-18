using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ToggleTrackingMode : MonoBehaviour
{
    public GameObject planeManagerObj;
    public GameObject imageManagerObj;
    
    void Start(){
        planeManagerObj.GetComponent<ARTrackedImageManager>().enabled = false;
        imageManagerObj.GetComponent<ARPlaneManager>().enabled = false;
    }
    public void ToggleImageManagerActive(){
        planeManagerObj.GetComponent<ARTrackedImageManager>().enabled = false;
        imageManagerObj.GetComponent<ARPlaneManager>().enabled = true;
    }
    public void TogglePlaneManagerActive(){
        planeManagerObj.GetComponent<ARTrackedImageManager>().enabled = true;
        imageManagerObj.GetComponent<ARPlaneManager>().enabled = false;
    }
}
