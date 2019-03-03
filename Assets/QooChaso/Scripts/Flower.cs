
public class Flower
{
    public int GrowStage;
    public int WitherPercentage;
    public bool IsGrowed;
    public bool IsWithered;

    public Flower()
    {
        GrowStage = 0;
        WitherPercentage = 0;
        IsGrowed = false;
        IsWithered = false;
    }

    public int PlusGrowStage()
    {
        if (!IsGrowed && !IsWithered && GrowStage < 4)
        {
            return GrowStage + 1;
        }
        else return GrowStage;
    }

    public int PlusWitherPercentage()
    {
        if (!IsWithered)
        {
            return WitherPercentage + 1;
        }
        else return WitherPercentage;
    }

    public void CheckGrowed()
    {
        if(GrowStage >= 4) IsGrowed = true;
    }

    public void CheckWhitered()
    {
        if (WitherPercentage >= 100) IsWithered = true;
    }

}
