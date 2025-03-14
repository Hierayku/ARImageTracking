using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject helicopterPrefab;
    
    private ARTrackedImageManager _trackedImageManager;

    private void Awake()
    {
        _trackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable() => _trackedImageManager.trackablesChanged.AddListener(OnChanged);
    private void OnDisable() => _trackedImageManager.trackablesChanged.RemoveListener(OnChanged);

    public void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            Vector3 spawnPosition = trackedImage.transform.position;
            Quaternion spawnRotation = Quaternion.identity;
            GameObject helicopter = Instantiate(helicopterPrefab, spawnPosition, spawnRotation, trackedImage.transform);
            helicopter.transform.LookAt(Camera.main.transform.position);
        }
    }

}
