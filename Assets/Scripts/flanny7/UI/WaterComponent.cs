using UnityEngine;
using UnityEngine.UI;

public class WaterComponent : MonoBehaviour
{
    public enum STATE
    {
        Wait,
        Empting,
        Filling,
    }

    [SerializeField]
    private Image _maskImage = null;
    [SerializeField]
    private Image _fillImage = null;
    [SerializeField]
    private float animationTime = 1.0f;

    private STATE state = STATE.Wait;
    private float animElapsedTime = 0.0f;
    
    public float FillImageFillAmount
    {
        get { return this._fillImage.fillAmount; }
        private set { this._fillImage.fillAmount = value; }
    }

    public float AnimTimeRate { get { return this.animElapsedTime / this.animationTime; } }

	private void Start ()
    {
        // null check
        FyUtility.NullCheck(this._maskImage, this.gameObject);
        FyUtility.NullCheck(this._fillImage, this.gameObject);

        // init
        this.state = STATE.Wait;
        this.animationTime = 0.0f;
	}
	
	private void Update ()
    {
        switch (this.state)
        {
            case STATE.Wait: break;

            case STATE.Empting:
                if (this.FillImageFillAmount == 0)
                {
                    this.state = STATE.Wait;
                    this.animElapsedTime = 0.0f;
                    break;
                }
                this.animElapsedTime += Time.deltaTime;
                this.FillImageFillAmount = Mathf.Lerp(1, 0, this.AnimTimeRate);

                break;

            case STATE.Filling:
                if (this.FillImageFillAmount == 1)
                {
                    this.state = STATE.Wait;
                    this.animElapsedTime = 0.0f;
                    break;
                }
                this.animElapsedTime += Time.deltaTime;
                this.FillImageFillAmount = Mathf.Lerp(0, 1, this.AnimTimeRate);

                break;
        }
	}

    public void StartEmpty()
    {
        this.state = STATE.Empting;
    }

    public void StartFill()
    {
        this.state = STATE.Filling;
    }
}
