using UnityEngine;

public class FlowerGrowingView : MonoBehaviour
{
    [SerializeField, Range(0,1)]
    private float fillAmount = 0.0f;

    private MeshRenderer meshRender = null;

    private void Start()
    {
        this.meshRender = this.GetComponent<MeshRenderer>();
    }

    public void Set(float _fillAmount)
    {
        this.fillAmount = _fillAmount;
        this.meshRender.material.SetFloat("_Cutoff", this.fillAmount);
    }
}
