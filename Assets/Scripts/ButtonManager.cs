using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    public GameObject furniture;
    // Start is called before the first frame update
    void Start()
    {
      btn = GetComponent<Button>(); // assign Button
      btn.onClick.AddListener(SelectObject);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void SelectObject() // where we change arObj in InputManager
    {
      DataHandler.Instance.furniture = furniture;
    }
}
