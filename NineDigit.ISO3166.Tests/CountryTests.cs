// ReSharper disable StringLiteralTypo
namespace NineDigit.ISO3166.Tests;

public class CountryTests
{
    [Fact]
    public void GetAll_ReturnsAllCountries()
    {
        var all = Country.GetAllOfficiallyAssigned();
        
        Assert.Equal(250, all.Count);
    }

    [Fact]
    public void TryGetByName_ReturnsTrueForExistingCountry()
    {
        var exists = Country.TryGetByName("Slovakia", out var country);
        
        Assert.True(exists);
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void TryGetByName_ReturnsFalseForNonExistingCountry()
    {
        var exists = Country.TryGetByName("Slavicia", out var country);
        
        Assert.False(exists);
        Assert.Equal(default, country);
    }
    
    [Fact]
    public void TryGetByTwoLetterCode_ReturnsTrueForExistingCountry()
    {
        var exists = Country.TryGetByTwoLetterCode("SK", out var country);
        
        Assert.True(exists);
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void TryGetByTwoLetterCode_ReturnsFalseForNonExistingCountry()
    {
        var exists = Country.TryGetByTwoLetterCode("XX", out var country);
        
        Assert.False(exists);
        Assert.Equal(default, country);
    }
    
    [Fact]
    public void TryGetByThreeLetterCode_ReturnsTrueForExistingCountry()
    {
        var exists = Country.TryGetByThreeLetterCode("SVK", out var country);
        
        Assert.True(exists);
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void TryGetByThreeLetterCode_ReturnsFalseForNonExistingCountry()
    {
        var exists = Country.TryGetByThreeLetterCode("XXX", out var country);
        
        Assert.False(exists);
        Assert.Equal(default, country);
    }
    
    [Fact]
    public void TryGetByNumericCode_ReturnsTrueForExistingCountry()
    {
        var exists = Country.TryGetByNumericCode("703", out var country);
        
        Assert.True(exists);
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void TryGetByNumericCode_ReturnsFalseForNonExistingCountry()
    {
        var exists = Country.TryGetByNumericCode("000", out var country);
        
        Assert.False(exists);
        Assert.Equal(default, country);
    }
    
    [Fact]
    public void Parse_ReturnsCountryByCountryName()
    {
        var country = Country.Parse("Slovakia");
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void Parse_ReturnsCountryByTwoLetterCode()
    {
        var country = Country.Parse("SK");
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void Parse_ReturnsCountryByThreeLetterCode()
    {
        var country = Country.Parse("SVK");
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void Parse_ReturnsCountryByNumericCode()
    {
        var country = Country.Parse("703");
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void GetByNumericCode_ReturnsCountryByIntegerNumericCode()
    {
        var country = Country.GetByNumericCode(703);
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void GetByNumericCode_ReturnsCountryByIntegerNumericCode2()
    {
        var country = Country.GetByNumericCode(4);
        Assert.Equal(Country.AF, country);
    }
    
    [Fact]
    public void Parse_ThrowsFormatExceptionForNonExistingCountry()
    {
        Assert.Throws<FormatException>(() => Country.Parse("?"));
    }

    [Fact]
    public void TryGetByCode_ReturnsCountryByTwoLetterCode()
    {
        var hasCountry = Country.TryGetByCode("SK", out var country);
        
        Assert.True(hasCountry);
        Assert.Equal(Country.SK, country);
    }
    
    [Fact]
    public void TryGetByCode_ReturnsCountryByThreeLetterCode()
    {
        var hasCountry = Country.TryGetByCode("SVK", out var country);
        
        Assert.True(hasCountry);
        Assert.Equal(Country.SK, country);
    }
}