using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//################
//# TODO:
//#   - Posible improvement: create a Sibgleton for this class¿
//###

public class DataHandler : MonoBehaviour
{

    private GameObject furniture;
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
      LoadItems();
      CreateButtons();
    }

    void LoadItems(){
        var items_obj = Resources.LoadAll("Items", typeof(Item));
        foreach (Item i in items_obj){
          items.Add(i);
        }
    }


    void CreateButtons()
    {
        foreach(Item i in items){
          //We will create a button for each Item in the list
          ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
          b.ItemId = current_id;
          b.ButtonTexture = i.itemImage;
          current_id++;
        }
    }
    public void SetFurniture(int id){
        furniture = items[id].itemPrefab;
    }

    public GameObject GetFurniture(){
      return furniture;
    }
}
