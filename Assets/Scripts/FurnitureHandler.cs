using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnitureHandler : MonoBehaviour
{
    public Transform placementIndicator;
    //public AudioSource buttonSuccess;
    //public AudioSource selectObjectSuccess;
    public GameObject selectionUI;

    private List<GameObject> m_Furnitures;
    private GameObject m_CurrentSelectedObject;
    private Camera m_MainCamera;
    private GameObject m_MaterialSelections;

    private void Start()
    {
        m_MainCamera = Camera.main;
        m_Furnitures = new List<GameObject>();
        m_MaterialSelections = selectionUI.transform.Find("MaterialGroup").transform.gameObject;
        selectionUI.SetActive(false);
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
                // Check if the user is clicking any virtual items in the world
                // Also check if the virtual object is a furniture
                if (hit.collider.gameObject != null && m_Furnitures.Contains(hit.collider.gameObject))
                {
                    GameObject userSelected = hit.collider.gameObject;
                    if (m_CurrentSelectedObject != null && userSelected != m_CurrentSelectedObject)
                    {
                        SelectFurniture(userSelected);
                    }
                    else if (m_CurrentSelectedObject == null)
                    {
                        SelectFurniture(userSelected);
                    }
                }
            }

            else
            {
                DeselectAllFurnitures();
            }
        }

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
        //selectObjectSuccess.Play();
        if (m_CurrentSelectedObject != null)
        {
            ToggleSelectionVisual(m_CurrentSelectedObject, false);
        }

        m_CurrentSelectedObject = selected;
        ToggleSelectionVisual(m_CurrentSelectedObject, true);
        //selectionUI.SetActive(true);

        if (selected.CompareTag("table"))
        {
            m_MaterialSelections.SetActive(false);
        }
        else
        {
            m_MaterialSelections.SetActive(true);
        }
    }

    private void DeselectAllFurnitures()
    {
        if (m_CurrentSelectedObject != null)
        {
            ToggleSelectionVisual(m_CurrentSelectedObject, false);
            m_CurrentSelectedObject = null;
        }
        selectionUI.SetActive(false);
    }

    private void ToggleSelectionVisual(GameObject obj, bool toggle)
    {
        obj.transform.Find("selected").gameObject.SetActive(toggle);
    }

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

    public void PlaceFurniture (GameObject prefab)
    {
        //buttonSuccess.Play();
        GameObject obj = Instantiate(prefab, placementIndicator.position, Quaternion.identity);
        m_Furnitures.Add(obj);
        SelectFurniture(obj);
    }


    public void ScaleSelected (float rate)
    {
        //buttonSuccess.Play();
        m_CurrentSelectedObject.transform.localScale += Vector3.one * rate;
        if (m_CurrentSelectedObject.transform.localScale.x <= 0
            || m_CurrentSelectedObject.transform.localScale.y <= 0
            || m_CurrentSelectedObject.transform.localScale.z <= 0)
        {
            m_CurrentSelectedObject.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }

        if (m_CurrentSelectedObject.transform.localScale.x > 0.8
            || m_CurrentSelectedObject.transform.localScale.y > 0.8
            || m_CurrentSelectedObject.transform.localScale.z > 0.8)
        {
            m_CurrentSelectedObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    public void RotateSelected (float rate)
    {
        //buttonSuccess.Play();

        // Rotate the object about the y-axis by rate
        m_CurrentSelectedObject.transform.eulerAngles += Vector3.up * rate;
    }

    public void DeleteSelectedFurniture()
    {
        //buttonSuccess.Play();

        // Delete furniture from furnitures list
        m_Furnitures.Remove(m_CurrentSelectedObject);
        Destroy(m_CurrentSelectedObject);
        DeselectAllFurnitures();
    }

    // Switch default cloth selection
    public void SetClothMaterial(Image buttonImage)
    {
        //buttonSuccess.Play();
        MeshRenderer[] meshRenderers = m_CurrentSelectedObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mr in meshRenderers)
        {
            if (mr.gameObject.CompareTag("cloth"))
            {
                mr.material = buttonImage.material;
            }
        }
    }
}
