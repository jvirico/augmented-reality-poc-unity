using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    //[SerializeField] private GameObject arObj;
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0)) // touch in mobile
      {
        Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
        if(_raycastManager.Raycast(ray, _hits)) // returns true if we hit anything
        {
          Pose pose = _hits[0].pose;
          Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation);
        }
      }

    }
}
