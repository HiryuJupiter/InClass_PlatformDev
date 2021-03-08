using System;

public class Temperature : IComparable
{
    protected float temperature;

    //If thi = 5, obj = 89, return -1, meaning this object is before the 
    public int CompareTo(object obj) //<0 then this object is before the thing in the list. 
    {
        if (obj == null)
            return 1;

        Temperature otherTemp = obj as Temperature;

        if (otherTemp != null)
        {
            if (this.temperature > otherTemp.temperature)
                return 1;
        }
        else if (this.temperature < otherTemp.temperature)
        {
            return -1;
        }
        return 0;
    }


    public Temperature(float _temp)
    {
        temperature = _temp;
    }


    public override string ToString()
    {
        return temperature.ToString("0.0");
    }
}
