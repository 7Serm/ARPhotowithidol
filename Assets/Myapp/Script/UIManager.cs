using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button toggleMoveButton;
    public Button toggleLookAtButton;

    private bool moveActive = true;
    private bool lookAtActive = true;

    public bool MoveActive => moveActive;
    public bool LookAtActive => lookAtActive;

    void Start()
    {
        toggleMoveButton.onClick.AddListener(ToggleMove);
        toggleLookAtButton.onClick.AddListener(ToggleLookAt);
    }

    public void ToggleMove()
    {
        moveActive = !moveActive;
        Debug.Log("Move Active: " + moveActive);
    }

    public void ToggleLookAt()
    {
        lookAtActive = !lookAtActive;
        Debug.Log("Look At Active: " + lookAtActive);
    }
}
