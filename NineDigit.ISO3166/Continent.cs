using System.Runtime.Serialization;

public enum Continent
{
    [EnumMember(Value = "AF")]
    Africa,
    
    [EnumMember(Value = "AN")]
    Antarctica,
    
    [EnumMember(Value = "AS")]
    Asia,
    
    [EnumMember(Value = "EU")]
    Europe,
    
    [EnumMember(Value = "NA")]
    NorthAmerica,
    
    [EnumMember(Value = "OC")]
    Oceania,
    
    [EnumMember(Value = "SA")]
    SouthAmerica
}