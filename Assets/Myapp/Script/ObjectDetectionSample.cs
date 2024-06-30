using Niantic.Lightship.AR.ObjectDetection;
using UnityEngine;

public class ObjectDetectionSample : MonoBehaviour
{
    [SerializeField]
    private float _probabilityThreshold = 0.5f;

    [SerializeField]
    private ARObjectDetectionManager _objectDetectionManager;

    private Color[] _colors = new Color[]
    {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            Color.magenta,
            Color.cyan,
            Color.white,
            Color.black
    };

    [SerializeField]
    private DrawRect _drawRect;

    private Canvas _canvas;

    [SerializeField]
    private string _categoryName;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
    }

    public void Start()
    {
        _objectDetectionManager.enabled = true;
        _objectDetectionManager.MetadataInitialized += OnMetadataInitialized;
    }

    private void OnDestroy()
    {
        _objectDetectionManager.MetadataInitialized -= OnMetadataInitialized;
        _objectDetectionManager.ObjectDetectionsUpdated -= ObjectDetectionsUpdated;
    }

    private void OnMetadataInitialized(ARObjectDetectionModelEventArgs args)
    {
        _objectDetectionManager.ObjectDetectionsUpdated += ObjectDetectionsUpdated;
    }

    private void ObjectDetectionsUpdated(ARObjectDetectionsUpdatedEventArgs args)
    {
        string resultString = "";
        float _confidence = 0;
        string _name = "";
        var result = args.Results;
        if (result == null)
        {
            return;
        }

        _drawRect.ClearRects();

        for (int i = 0; i < result.Count; i++)
        {
            var detection = result[i];
            var categorizations = detection.GetConfidentCategorizations(_probabilityThreshold);
            if (categorizations.Count <= 0)
            {
                break;
            }

            /*categorizations.Sort((a, b) => b.Confidence.CompareTo(a.Confidence));
            var categoryToDisplay = categorizations[0];
            _confidence = categoryToDisplay.Confidence;
            _name = categoryToDisplay.CategoryName;
*/
            
            //Get name and confidence of the detected object in a given category.
            _confidence = result[i].GetConfidence(_categoryName);

            //filter out the objects with confidence less than the threshold 
            if (_confidence < _probabilityThreshold)
            {
                break;
            }
            _name = _categoryName;

            int h = Mathf.FloorToInt(_canvas.GetComponent<RectTransform>().rect.height);
            int w = Mathf.FloorToInt(_canvas.GetComponent<RectTransform>().rect.width);

            // Get the rect around the detected object
            var _rect = result[i].CalculateRect(w, h, Screen.orientation);

            resultString = $"{_name}: {_confidence}\n";
            // Draw the rect
            _drawRect.CreateRect(_rect, _colors[i % _colors.Length], resultString);
        }
    }
}