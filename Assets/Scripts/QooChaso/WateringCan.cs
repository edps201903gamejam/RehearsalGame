// Writer：QooChaso

public class WateringCan
{

    public bool isWaterd;

    public WateringCan()
    {
        isWaterd = false;
    }

    public void ToMaxWaterGase()
    {
        isWaterd = true;
    }

    public void RemovedWater()
    {
        isWaterd = false;
    }
}
