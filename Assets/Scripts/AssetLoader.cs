﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> loadedAssets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Addressables.LoadAssetAsync<GameObject>("Modules").Completed += OnLoadDone; //The keys here are the Labels assignmed to addresable in Addressables Groups window
        Addressables.LoadAssetAsync<GameObject>("Furnitures").Completed += OnLoadDone;
    }

private void OnLoadDone(AsyncOperationHandle<GameObject> obj){
    loadedAssets.Add(obj.Result);
}


    // Update is called once per frame
    void Update()
    {
        
    }
}
