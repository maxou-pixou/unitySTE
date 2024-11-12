using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    private ARTrackedImageManager _trackedImagesManager;
    public GameObject[] ArPrefabs;
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    private readonly HashSet<string> _scannedImages = new HashSet<string>(); // Suivi des images scannées
    // les variables en dessous sont à suppr, en attendant celles d'elise en booleen à récupérer dans son script
    public bool carte3 = true;
    public bool carte4 = true;
    public bool carte5 = true;
    public bool carte6 = true;




    private void Start()
    {
        // Vérifiez si la variable est vraie
        if (carte3)
        {
            Debug.Log("La variable est vraie1 !");
        }
        if (carte4)
        {
            Debug.Log("La variable est vraie2 !");
        }
        if (carte5)
        {
            Debug.Log("La variable est vraie3 !");
        }
        if (carte6)
        {
            Debug.Log("La variable est vraie4 !");
        }
    }
    void Awake()
    {
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
        
    }

    void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    
    
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            
            
            if (!_scannedImages.Contains(imageName))
            {
                foreach (var curPrefab in ArPrefabs)
                {
                    if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                        && !_instantiatedPrefabs.ContainsKey(imageName))
                    {
                        var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                        _instantiatedPrefabs[imageName] = newPrefab;

                        
                        _scannedImages.Add(imageName);

                        
                        Debug.Log($"Image scannée ajoutée : {imageName}");
                        

                        Debug.Log($"Images scannées : {string.Join(", ", _scannedImages)}");
                    }
                }
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (_instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
            {
                prefab.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (_instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
            {
                Destroy(prefab);
                _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            }
        }
    }
}
