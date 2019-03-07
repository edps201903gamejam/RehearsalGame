// Writer：QooChaso

using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject flower1;
    private GameObject[] flowerList;
    private int finCount = 0;

    void Start()
    {
        flowerList = new GameObject[] 
        {
            Instantiate(flower1, new Vector3(4.5f, 0.35f, 4.5f), Quaternion.Euler(0, 0, 0)),
            Instantiate(flower1, new Vector3(-4.5f, 0.35f, 4.5f), Quaternion.Euler(0, 0, 0)),
            Instantiate(flower1, new Vector3(4.5f, 0.35f, -4.5f), Quaternion.Euler(0, 0, 0)),
            Instantiate(flower1, new Vector3(-4.5f, 0.35f, -4.5f), Quaternion.Euler(0, 0, 0))
        };
    }

    void Update()
    {
        foreach(GameObject i in flowerList)
        {
            i.transform.GetChild(0).gameObject.GetComponent<Flower>().OnUpdate();
        }
    }

    public GameObject[] GetObjects()
    {
        return flowerList;
    }

    private void toResult(){ Debug.Log("おわり"); }

}
