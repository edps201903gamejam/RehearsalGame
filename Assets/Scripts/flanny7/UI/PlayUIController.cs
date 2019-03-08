using UnityEngine;

public class PlayUIController : SingletonMonoBehaviour<PlayUIController>
{
    public WateringCanView WateringCan { get { return this.wateringCan; } set { this.wateringCan = value; } }

    [SerializeField]
    private CanvasGroup canvasGroup = null;
    [SerializeField]
    private WateringCanView wateringCan = null;

    private void Start()
    {
        FyUtility.NullCheck(this.wateringCan, this.gameObject);
        FyUtility.NullCheck(this.canvasGroup, this.gameObject);
    }

    public void Opne() { this.canvasGroup.alpha = 1; }
    public void Close() { this.canvasGroup.alpha = 0; }

    public void OnUpdate()
    {

    }
}
