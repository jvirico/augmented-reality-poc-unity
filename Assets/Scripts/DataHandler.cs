using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public GameObject furniture;
    private static DataHandler instance;
    // need to create an instance of this class : Singleton pattern

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
}
