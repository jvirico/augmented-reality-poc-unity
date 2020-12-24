using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    public GameObject furniture;
    [SerializeField] private RawImage buttonImage;
    private Camera MainCamera;

    private int _itemId;
    private Sprite _buttonTexture;

    public int ItemId {set { _itemId = value;}}
    public Sprite ButtonTexture 
    {
      set 
      { 
        _buttonTexture = value;
        buttonImage.texture = _buttonTexture.texture;  
      }
    }
    
    // Start is called before the first frame update
    void Start()
    {
      MainCamera = Camera.main;
      btn = GetComponent<Button>(); // assign Button
      btn.onClick.AddListener(SelectObject);
      btn.onClick.AddListener(InstantiateObject);

    }

    // Update is called once per frame
    void Update()
    {
      if (UIManager.Instance.onEntered(gameObject))
      {
        //transform.localScale = Vector3.one * 2;
        transform.DOScale(Vector3.one * 2, 0.2f); // second gives the times 
      }
      else
      {
        //transform.localScale = Vector3.one;
        transform.DOScale(Vector3.one, 0.2f); 
      }

    }

    void InstantiateObject(){
      //FurnitureHandler is in AR Camera (MainCamera)
      GameObject furniture = DataHandler.Instance.GetFurniture();
      MainCamera.GetComponent<FurnitureHandler>().PlaceFurniture(furniture);
      Debug.Log("Instantiate button");
    }

    void SelectObject()
    {
      DataHandler.Instance.SetFurniture(_itemId);
    }
}
