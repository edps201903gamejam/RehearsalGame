using UnityEngine;
using UnityEngine.UI;

public class CounterView : MonoBehaviour
{
    // Serialize cache
    [SerializeField]
    private Text flowerText_ = null;
    [SerializeField]
    private Text dieText_ = null;
    [Space(8)]
    [SerializeField]
    private int flowerCount = 0;
    [SerializeField]
    private int dieCount = 0;
    
    public void Set(int _flowerCount, int _dieCount)
    {
        this.flowerCount = _flowerCount;
        this.dieCount = _dieCount;
        this.flowerText_.text = string.Format("{0} こ", this.flowerCount);
        this.dieText_.text = string.Format("{0} こ", this.dieCount);
    }

    private void Start()
    {
        FyUtility.NullCheck(this.dieText_, this.gameObject);
        FyUtility.NullCheck(this.flowerText_, this.gameObject);

        this.flowerText_.text = string.Empty;
        this.dieText_.text = string.Empty;
	}

    private void Update()
    {

    }
}
