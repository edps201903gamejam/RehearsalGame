// Writer：QooChaso

using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public GameObject flower;
    private GameObject[] flowerList;
    private int finCount = 0;

    void Start()
    {
        Instantiate(flower, new Vector3(-4.2f, 1.0f, 4.2f), Quaternion.Euler(35f, 0, 0));
        Instantiate(flower, new Vector3(4.4f, 1.0f, -4.0f), Quaternion.Euler(55f, 0, 0));
        Instantiate(flower, new Vector3(-4.2f, 1.0f, -4.0f), Quaternion.Euler(55f, 0, 0));
    }

    void Update(){ CheckFin(); }

    private void CheckFin()
    {
        flowerList = GameObject.FindGameObjectsWithTag("plant");
        foreach (GameObject flower in flowerList)
        {
            Flower flo = flower.GetComponent<Flower>();
            if (flo.IsGrowed || flo.IsWithered) { finCount++; }
        }
        if (finCount == flowerList.Length) { toResult(); }
        else finCount = 0;
    }

    private void toResult(){ Debug.Log("おわり"); }

}
