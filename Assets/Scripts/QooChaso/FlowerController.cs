// Writer：QooChaso

using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject flower1;
    private GameObject flower2;
    private GameObject flower3;
    private GameObject flower4;
    private GameObject[] flowerList;
    private int finCount = 0;

    void Start()
    {
        flower2 = Instantiate(flower1, new Vector3(-4.5f, 0.35f, 4.5f), Quaternion.Euler(0, 0, 0));
        flower3 = Instantiate(flower1, new Vector3(4.5f, 0.35f, -4.5f), Quaternion.Euler(0, 0, 0));
        flower4 = Instantiate(flower1, new Vector3(-4.5f, 0.35f, -4.5f), Quaternion.Euler(0, 0, 0));
    }

    void Update()
    {
        flower1.transform.GetChild(0).gameObject.GetComponent<Flower>().OnUpdate();
        flower2.transform.GetChild(0).gameObject.GetComponent<Flower>().OnUpdate();
        flower3.transform.GetChild(0).gameObject.GetComponent<Flower>().OnUpdate();
        flower4.transform.GetChild(0).gameObject.GetComponent<Flower>().OnUpdate();
    }

    private void CheckFin()
    {
        //flowerList = GameObject.FindGameObjectsWithTag("plant");
        //foreach (GameObject flower in flowerList)
        //{
        //    Flower flo = flower.GetComponent<Flower>();
        //    if (flo.IsGrowed || flo.IsWithered) { finCount++; }
        //}
        //if (finCount == flowerList.Length) { toResult(); }
        //else finCount = 0;
    }

    private void toResult(){ Debug.Log("おわり"); }

}
