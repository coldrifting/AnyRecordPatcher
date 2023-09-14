namespace AnyRecordData;

public static partial class DataUtils
{
    public static float? GetDataNumber(float newFloat, float oldFloat)
    {
        return System.Math.Abs(newFloat - oldFloat) < 0.01 
            ? null
            : newFloat;
    }

    public static byte? GetDataNumber(byte newNum, byte oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static ushort? GetDataNumber(ushort newNum, ushort oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static uint? GetDataNumber(uint newNum, uint oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static int? GetDataNumber(int newNum, int oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }
}