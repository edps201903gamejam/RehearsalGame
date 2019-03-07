using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    private static readonly float FLOAT_MAX_VALUE = float.MaxValue;

    [SerializeField]
    private CanvasGroup canvasGroup_ = null;
    [SerializeField]
    private CounterView counter_ = null;
    [SerializeField]
    private BadgeView badge_ = null;
    [Space(8)]
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
        this.canvasGroup_.alpha = 0.0f;
        this.fadeInStartTime = FLOAT_MAX_VALUE;
        this.fadeOutStartTime = FLOAT_MAX_VALUE;
    }

    public void OnUpdate()
    {
        this.elapsedTime += Time.deltaTime;
        
        if (this.isFadeIn)
        {
            this.canvasGroup_.alpha = Easer.EaseIn(this.elapsedTime - this.fadeInStartTime, this.fadeInTime);
            if (0.99f <= this.canvasGroup_.alpha) { this.canvasGroup_.alpha = 1; this.fadeInStartTime = FLOAT_MAX_VALUE; }
        }

        if (this.isFadeOut)
        {
            this.canvasGroup_.alpha = Easer.EaseOut(this.elapsedTime - this.fadeOutStartTime, this.fadeOutTime);
            if (this.canvasGroup_.alpha <= 0.01f) { this.canvasGroup_.alpha = 0; this.fadeOutStartTime = FLOAT_MAX_VALUE; }
        }
    }

    public void Open(int _flowerCount, int _dieCount, BadgeView.BADGE_TYPE _badgeType)
    {
        this.counter_.Set(_flowerCount ,_dieCount);
        this.badge_.Set(_badgeType);

        this.fadeInStartTime = this.elapsedTime;
    }

    public void Close()
    {
        this.fadeOutStartTime = this.elapsedTime;
    }
}
