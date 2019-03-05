// Writer：QooChaso

using UnityEngine;

public class Flower : MonoBehaviour
{
    public int GrowStage;
    public int WitherPercentage;
    public bool IsGrowed;
    public bool IsWithered;

    public Material gStage1;
    public Material gStage2;
    public Material gStage3;
    public Material gStage4;
    public Material wither;

    private int witherTime = 1;     //[1秒ごと]にパーセンテージを+1する
    private int growTime = 10;      //1段階成長までの時間
    private int toWitherPer = 30;   //枯れるまでの時間
    private float timeElapsed1;
    private float timeElapsed2;
    private MeshRenderer render;

    public Flower()
    {
        GrowStage = 0;
        WitherPercentage = 0;
        IsGrowed = false;
        IsWithered = false;
    }

    public void MinusWhtherPercentage()
    {
        if (!IsGrowed && !IsWithered && GrowStage < 4) WitherPercentage -= 10;
    }

    private void PlusWitherPercentage()
    {
        if (!IsWithered) WitherPercentage++;
    }

    private void CheckGrowed()
    {
        if (GrowStage >= 3) IsGrowed = true;
    }

    private void CheckWhitered()
    {
        if (WitherPercentage >= toWitherPer) IsWithered = true;
        if (WitherPercentage <= 0) WitherPercentage = 0;
    }

    private void WitherEvent()
    {
        timeElapsed1 += Time.deltaTime;
        if (timeElapsed1 >= witherTime && !IsWithered)
        {
            PlusWitherPercentage();
            timeElapsed1 = 0.0f;
        }
    }

    private void GrowEvent()
    {
        timeElapsed2 += Time.deltaTime;
        if (timeElapsed2 >= growTime && !IsGrowed && !IsWithered)
        {
            GrowStage++;
            timeElapsed2 = 0.0f;
        }
    }

    private void Grow()
    {
        render = this.GetComponent<MeshRenderer>();
        switch(GrowStage)
        {
            case 0:
                render.material = gStage1;
                break;
            case 1:
                render.material = gStage2;
                break;
            case 2:
                render.material = gStage3;
                break;
            case 3:
                render.material = gStage4;
                break;
        }
    }

    private void Wither()
    {
        render.material = wither;
    }

    void Update()
    {
        CheckGrowed();
        CheckWhitered();
        WitherEvent();
        GrowEvent();
        if (!IsWithered) Grow();
        else Wither();
    }

}
