namespace TireService.Core.Domain;

public static class AppSettingConstantKeys
{
    public const string ShiftWorkDuration = "ShiftWorkDuration";

    public static HashSet<string> ReservedKeys = new HashSet<string>
    {
        ShiftWorkDuration
    };
}