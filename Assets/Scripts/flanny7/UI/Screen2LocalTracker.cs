using UnityEngine;

public class Screen2LocalTracker : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform = null;
    [SerializeField]
    private Vector3 offset = Vector3.zero;

    private RectTransform rectTransform = null;
    private RectTransform parentRectTransform = null;
    private Camera uiCamera = null;

    private void Start()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.parentRectTransform = (RectTransform)this.rectTransform.parent;

        var canvasArray = this.GetComponentsInParent<Canvas>();
        for (var i = 0; i < canvasArray.Length; ++i)
        {
            if (canvasArray[i].isRootCanvas)
            {
                uiCamera = canvasArray[i].worldCamera;
            }
        }
    }

    private void Update()
    {
        if (this.targetTransform != null &&
            this.rectTransform != null &&
            this.parentRectTransform != null &&
            this.uiCamera != null)
        {
            UpdateUiLocalPosFromTargetPos();
        }
    }

    private void UpdateUiLocalPosFromTargetPos()
    {
        var screenPos = Camera.main.WorldToScreenPoint(this.targetTransform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.parentRectTransform, screenPos, this.uiCamera, out localPos);
        this.rectTransform.localPosition = localPos;
    }
}