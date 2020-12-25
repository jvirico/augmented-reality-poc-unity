using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    [SerializeField] private GameObject furniture;
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> items; //we will store all the 3D objects we need for the App.

    private int current_id = 0; //we will use it to identify each object. We will use the position of the Item in the List items as ID.

    private static DataHandler instance;
    public static DataHandler Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindObjectOfType<DataHandler>();
        }
        return instance;
      }
    }

    private void Start(){
    }

    public void CreateButton(GameObject cloudAsset, Sprite image, int id)
    {

      Item auxItem = ScriptableObject.CreateInstance<Item>();
      
      auxItem.itemPrefab = cloudAsset;
      auxItem.itemImage = image;
      items.Add(auxItem as Item);

      ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
      b.ItemId = id;
      b.ButtonTexture = auxItem.itemImage;
    }
    
    public void SetFurniture(int id){
        furniture = items[id].itemPrefab;
        Debug.Log("Button " + id + " selected");
    }

    public GameObject GetFurniture(){
      return furniture;
    }
}