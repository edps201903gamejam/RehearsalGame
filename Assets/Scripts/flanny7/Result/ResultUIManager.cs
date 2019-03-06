using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    private static readonly float FLOAT_MAX_VALUE = float.MaxValue;

    [SerializeField]
    private CanvasGroup canvasGroup_ = null;
    [SerializeField]
    private CounterComponent counter_ = null;
    [SerializeField]
    private BadgeComponent badge_ = null;
    [SerializeField]
    private float fadeInTime = 1.0f;
    [SerializeField]
    private float fadeOutTime = 1.0f;

    private float elapsedTime = 0.0f;
    private bool isActive = false;

    private float fadeInStartTime = FLOAT_MAX_VALUE;
    private bool isFadeIn { get { return (this.elapsedTime - this.fadeInStartTime) <= this.fadeInTime; } }

    private float fadeOutStartTime = FLOAT_MAX_VALUE;
    private bool isFadeOut { get { return (this.elapsedTime - this.fadeOutStartTime) <= this.fadeOutTime; } }

    private void Start()
    {
        FyUtility.NullCheck(this.canvasGroup_, this.gameObject);
        FyUtility.NullCheck(this.counter_, this.gameObject);
        FyUtility.NullCheck(this.badge_, this.gameObject);

        this.elapsedTime = 0.0f;
        this.isActive = false;
        this.canvasGroup_.alpha = 0;
        this.fadeInStartTime = FLOAT_MAX_VALUE;
        this.fadeOutStartTime = FLOAT_MAX_VALUE;
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;
        
        if (this.isFadeIn)
        {
            this.canvasGroup_.alpha = Easer.EaseIn(this.elapsedTime - this.fadeInStartTime, this.fadeInTime);
            if (1 <= this.canvasGroup_.alpha) { this.canvasGroup_.alpha = 1; this.fadeInStartTime = FLOAT_MAX_VALUE; }
        }

        if (this.isFadeOut)
        {
            this.canvasGroup_.alpha = Easer.EaseOut(this.elapsedTime - this.fadeOutStartTime, this.fadeOutTime);
            if (this.canvasGroup_.alpha <= 0) { this.canvasGroup_.alpha = 0; this.fadeOutStartTime = FLOAT_MAX_VALUE; }
        }
    }

    public void Open(int _flowerCount, int _dieCount, BadgeComponent.BADGE_TYPE _badgeType)
    {
        this.counter_.FlowerCount = _flowerCount;
        this.counter_.DieCount = _dieCount;
        this.badge_.BadgeType = _badgeType;

        this.fadeInStartTime = this.elapsedTime;
    }

    public void Close()
    {
        this.fadeOutStartTime = this.elapsedTime;
    }
}
