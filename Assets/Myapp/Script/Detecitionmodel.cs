using Niantic.Lightship.AR.ObjectDetection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecitionmodel : MonoBehaviour
{
    [SerializeField] ARObjectDetectionManager _objectDetectionManager;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _objectDetectionManager.enabled = true;
        _objectDetectionManager.MetadataInitialized += OnMetadataInitialized;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMetadataInitialized(ARObjectDetectionModelEventArgs args)
    {
        _objectDetectionManager.ObjectDetectionsUpdated += ObjectDetectionsUpdated;
    }

    private void ObjectDetectionsUpdated(ARObjectDetectionsUpdatedEventArgs args)
    {
        var result = args.Results;
        if (result != null) { }


    }

    private void OnDestroy()
    {
        _objectDetectionManager.MetadataInitialized -= OnMetadataInitialized;
        _objectDetectionManager.ObjectDetectionsUpdated -= ObjectDetectionsUpdated;
    }
}
