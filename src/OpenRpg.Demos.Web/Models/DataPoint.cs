namespace OpenRpg.Demos.Web.Models;

public class DataPoint
{
    public decimal InputValue;
    public decimal OutputValue;

    public DataPoint(decimal inputValue, decimal outputValue)
    {
        InputValue = inputValue;
        OutputValue = outputValue;
    }
}