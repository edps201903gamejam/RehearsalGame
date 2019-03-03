using UnityEngine;

public class Flower : MonoBehaviour
{
    public Material gStage1;
    public Material gStage2;
    public Material gStage3;
    public Material gStage4;
    public Material wither;

    public int GrowStage;
    public float WitherPercentage;
    public bool IsGrowed;
    public bool IsWithered;

    private int witherTime = 1;
    private int growTime = 5;
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

    public void minusWhtherPercentage()
    {
        if (!IsGrowed && !IsWithered && GrowStage < 4){ WitherPercentage -= 10; }
        Debug.Log(WitherPercentage);
    }

    public float PlusWitherPercentage()
    {
        if (!IsWithered){ return WitherPercentage + 1; }
        else return WitherPercentage;
    }

    public void CheckGrowed()
    {
        if (GrowStage >= 3){ IsGrowed = true; }
    }

    public void CheckWhitered()
    {
        if (WitherPercentage >= 100){ IsWithered = true; }
        if(WitherPercentage <= 0){ WitherPercentage = 0; }
    }

    private void WitherEvent()
    {
        timeElapsed1 += Time.deltaTime;
        if (timeElapsed1 >= witherTime && WitherPercentage < 10)
        {
            WitherPercentage += 2;
            timeElapsed1 = 0.0f;
        }
    }

    private void GrowEvent()
    {
        timeElapsed2 += Time.deltaTime;
        if (timeElapsed2 >= growTime && GrowStage < 3 && !IsWithered)
        {
            GrowStage += 1;
            timeElapsed2 = 0.0f;
        }
    }

    void Grow()
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

    void Wither()
    {
        render.material = wither;
    }

    void Update()
    {
        CheckGrowed();
        CheckWhitered();
        WitherEvent();
        GrowEvent();
        if (!IsWithered){ Grow(); }
        else { Wither(); }
    }

}
