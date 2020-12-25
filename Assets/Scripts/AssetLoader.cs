using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> loadedAssets = new List<GameObject>();
    List<Sprite> loadedImages = new List<Sprite>();
    private int id = 0;

    // Start is called before the first frame update
    void Start()
    {

        Addressables.LoadAssetsAsync<GameObject>("Furnitures",LoadFurnitures);
        Addressables.LoadAssetsAsync<Sprite>("Images",LoadImages);
    }

    private void LoadFurnitures(GameObject obj){
        loadedAssets.Add(obj);
    }

    private void LoadImages(Sprite obj){

        if(obj!=null){
            loadedImages.Add(obj);
            string img_name = obj.name;

            foreach (var item in loadedAssets) //Find corresponding GameObject
            {
                if(item.name == img_name){
                    //We moved the Button creation here because of the Asyncronicity of the Addressable Load.
                    DataHandler.Instance.CreateButton(item,obj,id);
                    id++;
                }
            }
        }
    }


    private void OnLoadDone(GameObject obj){
        //loadedAssets.Add(obj);
        //DataHandler.Instance.LoadItem(obj);
        //DataHandler.Instance.CreateButton(obj,id);
        //id++;
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
