namespace AnyRecordData;

// Helper functions for dealing with numbers and booleans
public static partial class DataUtils
{
    public static bool? GetBool(bool? newBool, bool? oldBool)
    {
        return newBool == oldBool
            ? null
            : newBool;
    }
    
    public static byte? GetNumber(byte? newNum, byte? oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static ushort? GetNumber(ushort? newNum, ushort? oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static uint? GetNumber(uint? newNum, uint? oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static short? GetNumber(short? newNum, short? oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }

    public static int? GetNumber(int? newNum, int? oldNum)
    {
        return newNum == oldNum
            ? null
            : newNum;
    }
    
    public static float? GetNumber(float? newNum, float? oldNum)
    {
        if (newNum is null)
            return null;

        if (oldNum is null)
            return newNum;

        return Math.Abs((float)newNum - (float)oldNum) < 0.01 
            ? null
            : newNum;
    }


    public static bool PatchBool(bool recBool, bool? dataBool)
    {
        return dataBool ?? recBool;
    }

    public static byte PatchNumber(byte recNum, byte? dataNum)
    {
        return dataNum ?? recNum;
    }

    public static ushort PatchNumber(ushort recNum, ushort? dataNum)
    {
        return dataNum ?? recNum;
    }

    public static uint PatchNumber(uint recNum, uint? dataNum)
    {
        return dataNum ?? recNum;
    }

    public static short PatchNumber(short recNum, short? dataNum)
    {
        return dataNum ?? recNum;
    }

    public static int PatchNumber(int recNum, int? dataNum)
    {
        return dataNum ?? recNum;
    }

    public static float PatchNumber(float recNum, float? dataNum)
    {
        return dataNum ?? recNum;
    }
}