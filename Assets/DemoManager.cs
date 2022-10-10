using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class DemoManager : MonoBehaviour
{
    public AssetReference localno;
    private List<IResourceLocation> remoteNos;
    public AssetLabelReference number;
    void Start()
    {
        DisplayNos();
        Addressables.LoadResourceLocationsAsync(number.labelString).Completed += LocationLoaded;
    }

    private void DisplayNos()
    {
        localno.InstantiateAsync(Vector3.zero, Quaternion.identity);
        Debug.Log(number.labelString);
    }
    private void LocationLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj)
    {
        remoteNos = new List<IResourceLocation>(obj.Result);
        StartCoroutine(SpawnRemoteNos());
    }
    private IEnumerator SpawnRemoteNos()
    {
        yield return new WaitForSeconds(1f);
        float xoff = -4.0f;
        for (int i = 0; i < remoteNos.Count; i++)
        {
            Vector3 spawnposition = new Vector3(xoff, 3, 0);
            Addressables.InstantiateAsync(remoteNos[i], spawnposition, Quaternion.identity);
            xoff = xoff + 2.5f;
            yield return new WaitForSeconds(1f);
        }
    }
}