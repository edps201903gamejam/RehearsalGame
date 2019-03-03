
public class WateringCan
{

    public bool isWaterd;

    public WateringCan(){
        isWaterd = false;
    }

    public void ToMaxWaterGase()
    {
        isWaterd = true;
    }

    public bool ValidWatering()
    {
        if (isWaterd)
        {
            toRemovedWater();
            return true;
        }
        else return false;
    }

    private void toRemovedWater()
    {
        if (isWaterd) isWaterd = false;
    }
}
