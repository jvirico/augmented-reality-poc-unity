﻿/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    //[SerializeField] private GameObject arObj;
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager raycastManager;
    //[SerializeField] private GameObject crosshair;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //private Touch touch; // for mobile
    //private Pose pose;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
          Debug.Log("Pressed primary button.");
          // when we touch any part of the screen Input.mousePosition is raised
          Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
          if(raycastManager.Raycast(ray,hits))
          {
            Pose pose = hits[0].pose;
            Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);
          }
        }
    }

/*     void Update()
    {
      CrosshairCalculation();

      if(Input.touchCount<1 || touch.phase!=TouchPhase.Began)
        return;
      else{
        touch = Input.GetTouch(0);
        if(IsPointerOverUI(touch)) return;

        Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);
      }

    } */

/*     bool IsPointerOverUI(Touch touch)
    {
      PointerEventData eventData = new PointerEventData(EventSystem.current);
      eventData.position = new Vector2(touch.position.x, touch.position.y);
      List<RaycastResult> results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventData, results);
      return results.Count > 0;
    }

    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0)); //Vector3(0.5f, 0.5f, 0) gives us the center location of the screen!

        Ray ray = arCam.ScreenPointToRay(origin);
        if(_raycastManager.Raycast(ray, _hits)) // returns true if we hit anything
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90,0,0);
        }
    } */
}
 */