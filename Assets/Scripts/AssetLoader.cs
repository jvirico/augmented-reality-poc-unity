using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> loadedAssets = new List<GameObject>();
    private int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Addressables.LoadAssetAsync<GameObject>("Modules").Completed += OnLoadDone; //The keys here are the Labels assignmed to addresable in Addressables Groups window
        //Addressables.LoadAssetAsync<GameObject>("Furnitures").Completed += OnLoadDone;
        Addressables.LoadAssetsAsync<GameObject>("Furnitures",OnLoadDone); //We are checking for addressables labeled as Furnitires and calling OnLoadDone method for each of them.
        //DataHandler.Instance.CreateButtons();
    }

    private void OnLoadDone(GameObject obj){
        loadedAssets.Add(obj);
        DataHandler.Instance.LoadItem(obj);
        DataHandler.Instance.CreateButton(obj,id);
        id++;
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj){
        loadedAssets.Add(obj.Result);
    }

    public List<GameObject> GetCloudAssets(){
        return loadedAssets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
