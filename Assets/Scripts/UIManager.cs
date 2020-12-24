using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private GraphicRaycaster raycaster;
    private PointerEventData pData;
    private EventSystem eventSystem;
    [SerializeField] public Transform selectionPoint; // center of the button

    public static UIManager instance;

    public static UIManager Instance // could make a singleton class 
    {
      get
      {
        if (instance == null)
        {
          instance = FindObjectOfType<UIManager>();
        }
        return instance;
      }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        pData = new PointerEventData(eventSystem);
        pData.position = selectionPoint.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool onEntered(GameObject button)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pData, results);

        foreach (var result in results)
        {
            if (result.gameObject == button)
            {
                return true;
            }
        }
        return false;
    }
}
