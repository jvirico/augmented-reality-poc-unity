﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnitureHandler : MonoBehaviour
{
    public Transform placementIndicator;
    private List<GameObject> m_Furnitures;
    private GameObject m_CurrentSelectedObject;
    private Camera m_MainCamera;

    //Trying to make this sound
    //public AudioClip clip;
    //public float volume=1;
    
    private void Start()
    {
        //AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        m_MainCamera = Camera.main;
        m_Furnitures = new List<GameObject>();
    }

    // Update to check touches by user
    private void Update()
    {   
        if (Input.touchCount > 0
            && Input.touches[0].phase == TouchPhase.Began
            && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) // Checks if currently touching UI element
        {
            // Instantiate ray from camera to point touched by user
            Ray ray = m_MainCamera.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the user is clicking any virtual items
                // Also check if the virtual object is a furniture
                if (hit.collider.gameObject != null && m_Furnitures.Contains(hit.collider.gameObject))
                {
                    //If an object is selected through touch
                    GameObject userSelected = hit.collider.gameObject;
                    
                    // If and only if a different virtual object is selected in the environment.
                    if (m_CurrentSelectedObject != null && userSelected != m_CurrentSelectedObject)
                    {   
                        SelectFurniture(userSelected);
                    }
                    // Or if there's none selected
                    else if (m_CurrentSelectedObject == null)
                    {
                        SelectFurniture(userSelected);
                    }
                }
            }
            // If no virtual object is touch deselect them all
            else
            {
                DeselectAllFurnitures();
            }
        }

        // If a virtual object is selected it may be moved
        if (m_CurrentSelectedObject != null
            && Input.touchCount > 0
            && Input.touches[0].phase == TouchPhase.Moved)
        {
            MoveSelectedFurniture();
        }
    }


    // Selects current furniture tapped by user
    private void SelectFurniture(GameObject selected)
    {   
        if (m_CurrentSelectedObject != null)
        {
            ToggleSelectionVisual(m_CurrentSelectedObject, false);
        }

        m_CurrentSelectedObject = selected;
        ToggleSelectionVisual(m_CurrentSelectedObject, true);
    }

    // Deselects all furnitures
    private void DeselectAllFurnitures()
    {
        if (m_CurrentSelectedObject != null)
        {
            ToggleSelectionVisual(m_CurrentSelectedObject, false);
            m_CurrentSelectedObject = null;
        }
    }

    // Helper function - Allows changing from one virtual object to another
    private void ToggleSelectionVisual(GameObject obj, bool toggle)
    {
        obj.transform.Find("selected").gameObject.SetActive(toggle);
    }

    // Helper function - Allows moving virtual objects in the environment
    private void MoveSelectedFurniture()
    {
        // Get current position of finger
        Vector3 currentPosition = m_MainCamera.ScreenToViewportPoint(Input.touches[0].position);

        // Get last position of finger on screen
        Vector3 lastPosition = m_MainCamera.ScreenToViewportPoint(Input.touches[0].position - Input.touches[0].deltaPosition);

        // Should allow movement furniture in x-y direction only
        Vector3 touchDirection = currentPosition - lastPosition;

        Vector3 camRight = m_MainCamera.transform.right;

        // Prevent furnitures from being dragged above/under the plane
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = m_MainCamera.transform.forward;

        // Prevent furnitures from being dragged above/under the plane
        camForward.y = 0;
        camForward.Normalize();

        // Move the object relative to main camera current position
        m_CurrentSelectedObject.transform.position += (camRight * touchDirection.x + camForward * touchDirection.y);
    }

    // External function - Used to instantiate the virtual object
    public void PlaceFurniture (GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, placementIndicator.position, Quaternion.identity);
        m_Furnitures.Add(obj);
        SelectFurniture(obj);
    }


    // Helper function - Allows scaling the virtual objects in the environment
    public void ScaleSelected (float rate)
    {
        m_CurrentSelectedObject.transform.localScale += Vector3.one * rate;
        if (m_CurrentSelectedObject.transform.localScale.x <= 0
            || m_CurrentSelectedObject.transform.localScale.y <= 0
            || m_CurrentSelectedObject.transform.localScale.z <= 0)
        {
            m_CurrentSelectedObject.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }

    }

    // Helper function - Allows rotating the virtual objects in the environment
    public void RotateSelected (float rate)
    {
        // Rotate the object about the y-axis by rate
        m_CurrentSelectedObject.transform.eulerAngles += Vector3.up * rate;
    }

    // Helper function - Allows deleting a specific virtual object from the environment
    public void DeleteSelectedFurniture()
    {
        // Delete furniture from furnitures list
        m_Furnitures.Remove(m_CurrentSelectedObject);
        Destroy(m_CurrentSelectedObject);
        DeselectAllFurnitures();
    }

}
