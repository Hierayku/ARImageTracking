using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace MobileARTemplateAssets.Scripts
{
    public class PrefabCreator : MonoBehaviour
    {
        [SerializeField] private GameObject gameObjectPrefab;
    
        void OnTrackedImageChanged(ARTrackedImage trackedImage)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                Vector3 spawnPosition = trackedImage.transform.position;
                Quaternion spawnRotation = trackedImage.transform.rotation;
                Instantiate(gameObjectPrefab, spawnPosition, spawnRotation);
            }
        }

    }
}
