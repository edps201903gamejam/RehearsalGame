using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Serialize
    [SerializeField]
    private float speed = .01f;
    [SerializeField]
    private string gameSceneName = "";
    [SerializeField]
    private KeyCode decisionKey = KeyCode.Space;
    [SerializeField]
    private float startTimeOffset = 5.0f;
    [SerializeField]
    private float fadeSpeed = .01f;
    // Serialize Cache
    [SerializeField]
    private CanvasGroup canvasGroup_ = null;
    [SerializeField]
    private Image panelImage_ = null;

    private float elapsedTime = 0;
    private bool sceneLoadTrigger = false;

	private void Start ()
    {
        // init
        this.elapsedTime = 0;
        this.sceneLoadTrigger = false;
        this.canvasGroup_.alpha = 0;
	}
	
	private void Update ()
    {
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime < this.startTimeOffset) { return; }

        ChangePanelColor(this.panelImage_);
        if (this.canvasGroup_.alpha < 1 && !this.sceneLoadTrigger)
        {
            this.canvasGroup_.alpha += this.fadeSpeed * Time.deltaTime;
            return;
        }
        
        if (this.sceneLoadTrigger)
        {
            if (this.canvasGroup_.alpha == 0)
            {
                SceneManager.LoadScene(this.gameSceneName);
            }

            this.canvasGroup_.alpha -= this.fadeSpeed * Time.deltaTime;
        }

        if (Input.GetKey(this.decisionKey))
        {
            this.sceneLoadTrigger = true;
        }
    }

    private void ChangePanelColor(Image _image)
    {
        var c = _image.color;
        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);
        h = (h + this.speed) % 360.0f;
        _image.color = Color.HSVToRGB(h, s, v);
    }
}
