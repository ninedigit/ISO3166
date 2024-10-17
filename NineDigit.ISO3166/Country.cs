using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global

namespace NineDigit.ISO3166;

/// <summary>
/// List of ISO 3166 countries
/// </summary>
[TypeConverter(typeof(CountryTypeConverter))]
[DebuggerDisplay("{Name} / {TwoLetterCode} / {ThreeLetterCode} / {NumericCode} ({CodeType})")]
public readonly struct Country : IEquatable<Country>, IComparable<Country>, IComparable
{
    private static readonly StringComparer NameComparer = StringComparer.Ordinal;
    private static readonly StringComparer TwoLetterCodeComparer = StringComparer.OrdinalIgnoreCase;
    private static readonly StringComparer ThreeLetterCodeComparer = StringComparer.OrdinalIgnoreCase;
    private static readonly StringComparer NumericCodeComparer = StringComparer.Ordinal;
    
    private static readonly IList<Country> Items = new List<Country>();
    private static readonly IDictionary<string, int> TwoLetterCodeCountryIndexMap =
        new Dictionary<string, int>(TwoLetterCodeComparer);
    private static readonly IDictionary<string, int> ThreeLetterCodeCountryIndexMap =
        new Dictionary<string, int>(ThreeLetterCodeComparer);

    private Country(
        string name,
        string twoLetterCode,
        string threeLetterCode,
        string numericCode,
        Continent continent,
        CountryCodeType codeType = CountryCodeType.OfficiallyAssigned)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Value can not be null nor empty.", nameof(name));

        if (twoLetterCode is null || !Regex.IsMatch(twoLetterCode, "^[A-Z]{2}$"))
            throw new ArgumentException("Value must be a two-letter code.", nameof(twoLetterCode));

        if (threeLetterCode is null || !Regex.IsMatch(threeLetterCode, "^[A-Z]{3}$"))
            throw new ArgumentException("Value must be a two-letter code.", nameof(twoLetterCode));

        if (numericCode is null || !Regex.IsMatch(numericCode, "^[0-9]{3}$"))
            throw new ArgumentException("Value must be a three-digit code.", nameof(twoLetterCode));
        
        Name = name;
        TwoLetterCode = twoLetterCode;
        ThreeLetterCode = threeLetterCode;
        NumericCode = numericCode;
        Continent = continent;
        CodeType = codeType;
    }
    
    public string Name { get; }
    /// <summary>
    /// A two-letter code that represents a country name, recommended as the general purpose code (ISO 3166-1 alpha2)
    /// </summary>
    public string TwoLetterCode { get; }
    /// <summary>
    /// A three-letter code that represents a country name, which is usually more closely related to the country name
    /// (ISO 3166-1 alpha3)
    /// </summary>
    public string ThreeLetterCode { get; }
    /// <summary>
    /// ISO 3166-1 numeric
    /// </summary>
    public string NumericCode { get; }
    public Continent Continent { get; }
    public CountryCodeType CodeType { get; }

    public Country this[string twoOrThreeLetterCode]
    {
        get
        {
            if (!TryGetByCode(twoOrThreeLetterCode, out var country))
                throw new InvalidOperationException("No country found with the specified code.");

            return country;
        }
    }

    public static bool TryGetByCode(string twoOrThreeLetterCode, out Country country)
    {
        if (twoOrThreeLetterCode is null)
            throw new ArgumentNullException(nameof(twoOrThreeLetterCode));
        
        lock (Items)
        {
            if (twoOrThreeLetterCode.Length == 2)
            {
                if (TwoLetterCodeCountryIndexMap.TryGetValue(twoOrThreeLetterCode, out var index))
                {
                    country = Items[index];
                    return true;
                }
            }
            else if (twoOrThreeLetterCode.Length == 3)
            {
                if (ThreeLetterCodeCountryIndexMap.TryGetValue(twoOrThreeLetterCode, out var index))
                {
                    country = Items[index];
                    return true;
                }
            }
            else
            {
                throw new FormatException();
            }
        }

        country = default;
        return false;
    }

    public static IReadOnlyCollection<Country> GetAllOfficiallyAssigned()
    {
        lock (Items)
            return Items.ToList();
    }

    public static readonly Country AF = GetOrAdd("Afghanistan", "AF", "AFG", "004", Continent.Asia);
    public static readonly Country AX = GetOrAdd("Åland Islands", "AX", "ALA", "248", Continent.Europe);
    public static readonly Country AI = GetOrAdd("Anguilla", "AI", "AIA", "660", Continent.NorthAmerica);
    public static readonly Country AL = GetOrAdd("Albania", "AL", "ALB", "008", Continent.Europe);
    public static readonly Country DZ = GetOrAdd("Algeria", "DZ", "DZA", "012", Continent.Africa);
    public static readonly Country AS = GetOrAdd("American Samoa", "AS", "ASM", "016", Continent.Oceania);
    public static readonly Country AD = GetOrAdd("Andorra", "AD", "AND", "020", Continent.Europe);
    public static readonly Country AQ = GetOrAdd("Antarctica", "AQ", "ATA", "010", Continent.Antarctica);
    public static readonly Country AT = GetOrAdd("Austria", "AT", "AUT", "040", Continent.Europe);
    public static readonly Country AG = GetOrAdd("Antigua and Barbuda", "AG", "ATG", "028", Continent.NorthAmerica);
    public static readonly Country AW = GetOrAdd("Aruba", "AW", "ABW", "533", Continent.NorthAmerica);
    public static readonly Country AU = GetOrAdd("Australia", "AU", "AUS", "036", Continent.Oceania);
    public static readonly Country AZ = GetOrAdd("Azerbaijan", "AZ", "AZE", "031", Continent.Asia);
    public static readonly Country BS = GetOrAdd("Bahamas", "BS", "BHS", "044", Continent.NorthAmerica);
    public static readonly Country BH = GetOrAdd("Bahrain", "BH", "BHR", "048", Continent.Asia);
    public static readonly Country AR = GetOrAdd("Argentina", "AR", "ARG", "032", Continent.SouthAmerica);
    public static readonly Country BD = GetOrAdd("Bangladesh", "BD", "BGD", "050", Continent.Asia);
    public static readonly Country BY = GetOrAdd("Belarus", "BY", "BLR", "112", Continent.Europe);
    public static readonly Country BJ = GetOrAdd("Benin", "BJ", "BEN", "204", Continent.Africa);
    public static readonly Country BM = GetOrAdd("Bermuda", "BM", "BMU", "060", Continent.NorthAmerica);
    public static readonly Country BT = GetOrAdd("Bhutan", "BT", "BTN", "064", Continent.Asia);
    public static readonly Country BO = GetOrAdd("Bolivia", "BO", "BOL", "068", Continent.SouthAmerica);
    public static readonly Country BA = GetOrAdd("Bosnia and Herzegovina", "BA", "BIH", "070", Continent.Europe);
    public static readonly Country BW = GetOrAdd("Botswana", "BW", "BWA", "072", Continent.Africa);
    public static readonly Country BV = GetOrAdd("Bouvet Island", "BV", "BVT", "074", Continent.Antarctica);
    public static readonly Country BE = GetOrAdd("Belgium", "BE", "BEL", "056", Continent.Europe);
    public static readonly Country BR = GetOrAdd("Brazil", "BR", "BRA", "076", Continent.SouthAmerica);
    public static readonly Country SI = GetOrAdd("Slovenia", "SI", "SVN", "705", Continent.Europe);
    public static readonly Country IO = GetOrAdd("British Indian Ocean Territory", "IO", "IOT", "086", Continent.Asia);
    public static readonly Country VG = GetOrAdd("British Virgin Islands", "VG", "VGB", "092", Continent.NorthAmerica);
    public static readonly Country BN = GetOrAdd("Brunei", "BN", "BRN", "096", Continent.Asia);
    public static readonly Country MZ = GetOrAdd("Mozambique", "MZ", "MOZ", "508", Continent.Africa);
    public static readonly Country BF = GetOrAdd("Burkina Faso", "BF", "BFA", "854", Continent.Africa);
    public static readonly Country BB = GetOrAdd("Barbados", "BB", "BRB", "052", Continent.NorthAmerica);
    public static readonly Country BI = GetOrAdd("Burundi", "BI", "BDI", "108", Continent.Africa);
    public static readonly Country KH = GetOrAdd("Cambodia", "KH", "KHM", "116", Continent.Asia);
    public static readonly Country CM = GetOrAdd("Cameroon", "CM", "CMR", "120", Continent.Africa);
    public static readonly Country CA = GetOrAdd("Canada", "CA", "CAN", "124", Continent.NorthAmerica);
    public static readonly Country BQ = GetOrAdd("Caribbean Netherlands", "BQ", "BES", "535", Continent.NorthAmerica);
    public static readonly Country KY = GetOrAdd("Cayman Islands", "KY", "CYM", "136", Continent.NorthAmerica);
    public static readonly Country ES = GetOrAdd("Spain", "ES", "ESP", "724", Continent.Europe);
    public static readonly Country CF = GetOrAdd("Central African Republic", "CF", "CAF", "140", Continent.Africa);
    public static readonly Country TD = GetOrAdd("Chad", "TD", "TCD", "148", Continent.Africa);
    public static readonly Country CL = GetOrAdd("Chile", "CL", "CHL", "152", Continent.SouthAmerica);
    public static readonly Country SY = GetOrAdd("Syria", "SY", "SYR", "760", Continent.Asia);
    public static readonly Country CN = GetOrAdd("China", "CN", "CHN", "156", Continent.Asia);
    public static readonly Country CX = GetOrAdd("Christmas Island", "CX", "CXR", "162", Continent.Oceania);
    public static readonly Country CC = GetOrAdd("Cocos (Keeling) Islands", "CC", "CCK", "166", Continent.Asia);
    public static readonly Country CO = GetOrAdd("Colombia", "CO", "COL", "170", Continent.SouthAmerica);
    public static readonly Country MU = GetOrAdd("Mauritius", "MU", "MUS", "480", Continent.Africa);
    public static readonly Country QA = GetOrAdd("Qatar", "QA", "QAT", "634", Continent.Asia);
    public static readonly Country CZ = GetOrAdd("Czechia", "CZ", "CZE", "203", Continent.Europe);
    public static readonly Country KM = GetOrAdd("Comoros", "KM", "COM", "174", Continent.Africa);
    public static readonly Country CK = GetOrAdd("Cook Islands", "CK", "COK", "184", Continent.Oceania);
    public static readonly Country CR = GetOrAdd("Costa Rica", "CR", "CRI", "188", Continent.NorthAmerica);
    public static readonly Country HR = GetOrAdd("Croatia", "HR", "HRV", "191", Continent.Europe);
    public static readonly Country CW = GetOrAdd("Curacao", "CW", "CUW", "531", Continent.NorthAmerica);
    public static readonly Country CY = GetOrAdd("Cyprus", "CY", "CYP", "196", Continent.Europe);
    public static readonly Country BZ = GetOrAdd("Belize", "BZ", "BLZ", "084", Continent.NorthAmerica);
    public static readonly Country CD = GetOrAdd("Democratic Republic of the Congo", "CD", "COD", "180", Continent.Africa);
    public static readonly Country DJ = GetOrAdd("Djibouti", "DJ", "DJI", "262", Continent.Africa);
    public static readonly Country DM = GetOrAdd("Dominica", "DM", "DMA", "212", Continent.NorthAmerica);
    public static readonly Country TL = GetOrAdd("East Timor", "TL", "TLS", "626", Continent.Oceania);
    public static readonly Country EG = GetOrAdd("Egypt", "EG", "EGY", "818", Continent.Africa);
    public static readonly Country SV = GetOrAdd("El Salvador", "SV", "SLV", "222", Continent.NorthAmerica);
    public static readonly Country SZ = GetOrAdd("Eswatini", "SZ", "SWZ", "748", Continent.Africa);
    public static readonly Country GQ = GetOrAdd("Equatorial Guinea", "GQ", "GNQ", "226", Continent.Africa);
    public static readonly Country ER = GetOrAdd("Eritrea", "ER", "ERI", "232", Continent.Africa);
    public static readonly Country FK = GetOrAdd("Falkland Islands", "FK", "FLK", "238", Continent.SouthAmerica);
    public static readonly Country ET = GetOrAdd("Ethiopia", "ET", "ETH", "231", Continent.Africa);
    public static readonly Country ZA = GetOrAdd("South Africa", "ZA", "ZAF", "710", Continent.Africa);
    public static readonly Country FM = GetOrAdd("Federated States of Micronesia", "FM", "FSM", "583", Continent.Oceania);
    public static readonly Country FJ = GetOrAdd("Fiji", "FJ", "FJI", "242", Continent.Oceania);
    public static readonly Country ZW = GetOrAdd("Zimbabwe", "ZW", "ZWE", "716", Continent.Africa);
    public static readonly Country FI = GetOrAdd("Finland", "FI", "FIN", "246", Continent.Europe);
    public static readonly Country FR = GetOrAdd("France", "FR", "FRA", "250", Continent.Europe);
    public static readonly Country GF = GetOrAdd("French Guiana", "GF", "GUF", "254", Continent.SouthAmerica);
    public static readonly Country PF = GetOrAdd("French Polynesia", "PF", "PYF", "258", Continent.Oceania);
    public static readonly Country DK = GetOrAdd("Denmark", "DK", "DNK", "208", Continent.Europe);
    public static readonly Country TF = GetOrAdd("French Southern and Antarctic Lands", "TF", "ATF", "260", Continent.Antarctica);
    public static readonly Country GM = GetOrAdd("Gambia", "GM", "GMB", "270", Continent.Africa);
    public static readonly Country GA = GetOrAdd("Gabon", "GA", "GAB", "266", Continent.Africa);
    public static readonly Country PW = GetOrAdd("Palau", "PW", "PLW", "585", Continent.Oceania);
    public static readonly Country GE = GetOrAdd("Georgia", "GE", "GEO", "268", Continent.Asia);
    public static readonly Country DE = GetOrAdd("Germany", "DE", "DEU", "276", Continent.Europe);
    public static readonly Country GH = GetOrAdd("Ghana", "GH", "GHA", "288", Continent.Africa);
    public static readonly Country KE = GetOrAdd("Kenya", "KE", "KEN", "404", Continent.Africa);
    public static readonly Country GI = GetOrAdd("Gibraltar", "GI", "GIB", "292", Continent.Europe);
    public static readonly Country GR = GetOrAdd("Greece", "GR", "GRC", "300", Continent.Europe);
    public static readonly Country GL = GetOrAdd("Greenland", "GL", "GRL", "304", Continent.NorthAmerica);
    public static readonly Country GD = GetOrAdd("Grenada", "GD", "GRD", "308", Continent.NorthAmerica);
    public static readonly Country GP = GetOrAdd("Guadeloupe", "GP", "GLP", "312", Continent.NorthAmerica);
    public static readonly Country GU = GetOrAdd("Guam", "GU", "GUM", "316", Continent.Oceania);
    public static readonly Country GG = GetOrAdd("Guernsey", "GG", "GGY", "831", Continent.Europe);
    public static readonly Country GW = GetOrAdd("Guinea-Bissau", "GW", "GNB", "624", Continent.Africa);
    public static readonly Country GY = GetOrAdd("Guyana", "GY", "GUY", "328", Continent.SouthAmerica);
    public static readonly Country HT = GetOrAdd("Haiti", "HT", "HTI", "332", Continent.NorthAmerica);
    public static readonly Country VU = GetOrAdd("Vanuatu", "VU", "VUT", "548", Continent.Oceania);
    public static readonly Country HM = GetOrAdd("Heard Island and McDonald Islands", "HM", "HMD", "334", Continent.Antarctica);
    public static readonly Country HN = GetOrAdd("Honduras", "HN", "HND", "340", Continent.NorthAmerica);
    public static readonly Country HK = GetOrAdd("Hong Kong", "HK", "HKG", "344", Continent.Asia);
    public static readonly Country HU = GetOrAdd("Hungary", "HU", "HUN", "348", Continent.Europe);
    public static readonly Country IS = GetOrAdd("Iceland", "IS", "ISL", "352", Continent.Europe);
    public static readonly Country IN = GetOrAdd("India", "IN", "IND", "356", Continent.Asia);
    public static readonly Country ID = GetOrAdd("Indonesia", "ID", "IDN", "360", Continent.Asia);
    public static readonly Country IR = GetOrAdd("Iran", "IR", "IRN", "364", Continent.Asia);
    public static readonly Country IQ = GetOrAdd("Iraq", "IQ", "IRQ", "368", Continent.Asia);
    public static readonly Country IM = GetOrAdd("Isle of Man", "IM", "IMN", "833", Continent.Europe);
    public static readonly Country IL = GetOrAdd("Israel", "IL", "ISR", "376", Continent.Asia);
    public static readonly Country IT = GetOrAdd("Italy", "IT", "ITA", "380", Continent.Europe);
    public static readonly Country CI = GetOrAdd("Ivory Coast", "CI", "CIV", "384", Continent.Africa);
    public static readonly Country JM = GetOrAdd("Jamaica", "JM", "JAM", "388", Continent.NorthAmerica);
    public static readonly Country JO = GetOrAdd("Jordan", "JO", "JOR", "400", Continent.Asia);
    public static readonly Country JE = GetOrAdd("Jersey", "JE", "JEY", "832", Continent.Europe);
    public static readonly Country KZ = GetOrAdd("Kazakhstan", "KZ", "KAZ", "398", Continent.Asia);
    public static readonly Country NF = GetOrAdd("Norfolk Island", "NF", "NFK", "574", Continent.Oceania);
    public static readonly Country GS = GetOrAdd("South Georgia and South Sandwich Islands", "GS", "SGS", "239", Continent.Antarctica);
    public static readonly Country KI = GetOrAdd("Kiribati", "KI", "KIR", "296", Continent.Oceania);
    public static readonly Country XK = GetOrAdd("Kosovo", "XK", "XXK", "412", Continent.Europe);
    public static readonly Country KW = GetOrAdd("Kuwait", "KW", "KWT", "414", Continent.Asia);
    public static readonly Country KG = GetOrAdd("Kyrgyzstan", "KG", "KGZ", "417", Continent.Asia);
    public static readonly Country LV = GetOrAdd("Latvia", "LV", "LVA", "428", Continent.Europe);
    public static readonly Country LB = GetOrAdd("Lebanon", "LB", "LBN", "422", Continent.Asia);
    public static readonly Country LA = GetOrAdd("Laos", "LA", "LAO", "418", Continent.Asia);
    public static readonly Country LS = GetOrAdd("Lesotho", "LS", "LSO", "426", Continent.Africa);
    public static readonly Country LR = GetOrAdd("Liberia", "LR", "LBR", "430", Continent.Africa);
    public static readonly Country LY = GetOrAdd("Libya", "LY", "LBY", "434", Continent.Africa);
    public static readonly Country LI = GetOrAdd("Liechtenstein", "LI", "LIE", "438", Continent.Europe);
    public static readonly Country LT = GetOrAdd("Lithuania", "LT", "LTU", "440", Continent.Europe);
    public static readonly Country LU = GetOrAdd("Luxembourg", "LU", "LUX", "442", Continent.Europe);
    public static readonly Country MO = GetOrAdd("Macao", "MO", "MAC", "446", Continent.Asia);
    public static readonly Country MG = GetOrAdd("Madagascar", "MG", "MDG", "450", Continent.Africa);
    public static readonly Country MW = GetOrAdd("Malawi", "MW", "MWI", "454", Continent.Africa);
    public static readonly Country MY = GetOrAdd("Malaysia", "MY", "MYS", "458", Continent.Asia);
    public static readonly Country MV = GetOrAdd("Maldives", "MV", "MDV", "462", Continent.Asia);
    public static readonly Country ML = GetOrAdd("Mali", "ML", "MLI", "466", Continent.Africa);
    public static readonly Country MH = GetOrAdd("Marshall Islands", "MH", "MHL", "584", Continent.Oceania);
    public static readonly Country MQ = GetOrAdd("Martinique", "MQ", "MTQ", "474", Continent.NorthAmerica);
    public static readonly Country MR = GetOrAdd("Mauritania", "MR", "MRT", "478", Continent.Africa);
    public static readonly Country YT = GetOrAdd("Mayotte", "YT", "MYT", "175", Continent.Africa);
    public static readonly Country PT = GetOrAdd("Portugal", "PT", "PRT", "620", Continent.Europe);
    public static readonly Country MD = GetOrAdd("Moldova", "MD", "MDA", "498", Continent.Europe);
    public static readonly Country MX = GetOrAdd("Mexico", "MX", "MEX", "484", Continent.NorthAmerica);
    public static readonly Country MN = GetOrAdd("Mongolia", "MN", "MNG", "496", Continent.Asia);
    public static readonly Country ME = GetOrAdd("Montenegro", "ME", "MNE", "499", Continent.Europe);
    public static readonly Country MS = GetOrAdd("Montserrat", "MS", "MSR", "500", Continent.NorthAmerica);
    public static readonly Country MA = GetOrAdd("Morocco", "MA", "MAR", "504", Continent.Africa);
    public static readonly Country NO = GetOrAdd("Norway", "NO", "NOR", "578", Continent.Europe);
    public static readonly Country MM = GetOrAdd("Myanmar", "MM", "MMR", "104", Continent.Asia);
    public static readonly Country NA = GetOrAdd("Namibia", "NA", "NAM", "516", Continent.Africa);
    public static readonly Country NR = GetOrAdd("Nauru", "NR", "NRU", "520", Continent.Oceania);
    public static readonly Country NP = GetOrAdd("Nepal", "NP", "NPL", "524", Continent.Asia);
    public static readonly Country CU = GetOrAdd("Cuba", "CU", "CUB", "192", Continent.NorthAmerica);
    public static readonly Country AM = GetOrAdd("Armenia", "AM", "ARM", "051", Continent.Asia);
    public static readonly Country SG = GetOrAdd("Singapore", "SG", "SGP", "702", Continent.Asia);
    public static readonly Country CV = GetOrAdd("Cape Verde", "CV", "CPV", "132", Continent.Africa);
    public static readonly Country NL = GetOrAdd("Netherlands", "NL", "NLD", "528", Continent.Europe);
    public static readonly Country UZ = GetOrAdd("Uzbekistan", "UZ", "UZB", "860", Continent.Asia);
    public static readonly Country NC = GetOrAdd("GetOrAdd Caledonia", "NC", "NCL", "540", Continent.Oceania);
    public static readonly Country NZ = GetOrAdd("GetOrAdd Zealand", "NZ", "NZL", "554", Continent.Oceania);
    public static readonly Country NI = GetOrAdd("Nicaragua", "NI", "NIC", "558", Continent.NorthAmerica);
    public static readonly Country NE = GetOrAdd("Niger", "NE", "NER", "562", Continent.Africa);
    public static readonly Country NG = GetOrAdd("Nigeria", "NG", "NGA", "566", Continent.Africa);
    public static readonly Country NU = GetOrAdd("Niue", "NU", "NIU", "570", Continent.Oceania);
    public static readonly Country MP = GetOrAdd("Northern Mariana Islands", "MP", "MNP", "580", Continent.Oceania);
    public static readonly Country MK = GetOrAdd("North Macedonia", "MK", "MKD", "807", Continent.Europe);
    public static readonly Country GB = GetOrAdd("United Kingdom", "GB", "GBR", "826", Continent.Europe);
    public static readonly Country FO = GetOrAdd("Faroe Islands", "FO", "FRO", "234", Continent.Europe);
    public static readonly Country MT = GetOrAdd("Malta", "MT", "MLT", "470", Continent.Europe);
    public static readonly Country PK = GetOrAdd("Pakistan", "PK", "PAK", "586", Continent.Asia);
    public static readonly Country OM = GetOrAdd("Oman", "OM", "OMN", "512", Continent.Asia);
    public static readonly Country PS = GetOrAdd("Palestine", "PS", "PSE", "275", Continent.Asia);
    public static readonly Country PA = GetOrAdd("Panama", "PA", "PAN", "591", Continent.NorthAmerica);
    public static readonly Country PG = GetOrAdd("Papua GetOrAdd Guinea", "PG", "PNG", "598", Continent.Oceania);
    public static readonly Country PY = GetOrAdd("Paraguay", "PY", "PRY", "600", Continent.SouthAmerica);
    public static readonly Country PE = GetOrAdd("Peru", "PE", "PER", "604", Continent.SouthAmerica);
    public static readonly Country PH = GetOrAdd("Philippines", "PH", "PHL", "608", Continent.Asia);
    public static readonly Country PN = GetOrAdd("Pitcairn Islands", "PN", "PCN", "612", Continent.Oceania);
    public static readonly Country PL = GetOrAdd("Poland", "PL", "POL", "616", Continent.Europe);
    public static readonly Country GN = GetOrAdd("Guinea", "GN", "GIN", "324", Continent.Africa);
    public static readonly Country DO = GetOrAdd("Dominican Republic", "DO", "DOM", "214", Continent.NorthAmerica);
    public static readonly Country MC = GetOrAdd("Principality of Monaco", "MC", "MCO", "492", Continent.Europe);
    public static readonly Country PR = GetOrAdd("Puerto Rico", "PR", "PRI", "630", Continent.NorthAmerica);
    public static readonly Country CG = GetOrAdd("Republic of the Congo", "CG", "COG", "178", Continent.Africa);
    public static readonly Country RE = GetOrAdd("Reunion", "RE", "REU", "638", Continent.Africa);
    public static readonly Country RO = GetOrAdd("Romania", "RO", "ROU", "642", Continent.Europe);
    public static readonly Country RU = GetOrAdd("Russia", "RU", "RUS", "643", Continent.Europe);
    public static readonly Country RW = GetOrAdd("Rwanda", "RW", "RWA", "646", Continent.Africa);
    public static readonly Country BL = GetOrAdd("Saint Barthelemy", "BL", "BLM", "652", Continent.NorthAmerica);
    public static readonly Country SH = GetOrAdd("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", "654", Continent.Africa);
    public static readonly Country AO = GetOrAdd("Angola", "AO", "AGO", "024", Continent.Africa);
    public static readonly Country KN = GetOrAdd("Saint Kitts and Nevis", "KN", "KNA", "659", Continent.NorthAmerica);
    public static readonly Country LC = GetOrAdd("Saint Lucia", "LC", "LCA", "662", Continent.NorthAmerica);
    public static readonly Country MF = GetOrAdd("Saint Martin", "MF", "MAF", "663", Continent.NorthAmerica);
    public static readonly Country PM = GetOrAdd("Saint Pierre and Miquelon", "PM", "SPM", "666", Continent.NorthAmerica);
    public static readonly Country WS = GetOrAdd("Samoa", "WS", "WSM", "882", Continent.Oceania);
    public static readonly Country SM = GetOrAdd("San Marino", "SM", "SMR", "674", Continent.Europe);
    public static readonly Country ST = GetOrAdd("Sao Tome and Principe", "ST", "STP", "678", Continent.Africa);
    public static readonly Country SA = GetOrAdd("Saudi Arabia", "SA", "SAU", "682", Continent.Asia);
    public static readonly Country SN = GetOrAdd("Senegal", "SN", "SEN", "686", Continent.Africa);
    public static readonly Country RS = GetOrAdd("Serbia", "RS", "SRB", "688", Continent.Europe);
    public static readonly Country SC = GetOrAdd("Seychelles", "SC", "SYC", "690", Continent.Africa);
    public static readonly Country SL = GetOrAdd("Sierra Leone", "SL", "SLE", "694", Continent.Africa);
    public static readonly Country SX = GetOrAdd("Sint Maarten", "SX", "SXM", "534", Continent.NorthAmerica);
    public static readonly Country VE = GetOrAdd("Venezuela", "VE", "VEN", "862", Continent.SouthAmerica);
    public static readonly Country SK = GetOrAdd("Slovakia", "SK", "SVK", "703", Continent.Europe);
    public static readonly Country SB = GetOrAdd("Solomon Islands", "SB", "SLB", "090", Continent.Oceania);
    public static readonly Country SO = GetOrAdd("Somalia", "SO", "SOM", "706", Continent.Africa);
    public static readonly Country ZM = GetOrAdd("Zambia", "ZM", "ZMB", "894", Continent.Africa);
    public static readonly Country SS = GetOrAdd("South Sudan", "SS", "SSD", "728", Continent.Africa);
    public static readonly Country KR = GetOrAdd("South Korea", "KR", "KOR", "410", Continent.Asia);
    public static readonly Country LK = GetOrAdd("Sri Lanka", "LK", "LKA", "144", Continent.Asia);
    public static readonly Country WF = GetOrAdd("Wallis and Futuna", "WF", "WLF", "876", Continent.Oceania);
    public static readonly Country SD = GetOrAdd("Sudan", "SD", "SDN", "729", Continent.Africa);
    public static readonly Country SR = GetOrAdd("Suriname", "SR", "SUR", "740", Continent.SouthAmerica);
    public static readonly Country EE = GetOrAdd("Estonia", "EE", "EST", "233", Continent.Europe);
    public static readonly Country SJ = GetOrAdd("Svalbard", "SJ", "SJM", "744", Continent.Europe);
    public static readonly Country SE = GetOrAdd("Sweden", "SE", "SWE", "752", Continent.Europe);
    public static readonly Country CH = GetOrAdd("Switzerland", "CH", "CHE", "756", Continent.Europe);
    public static readonly Country TW = GetOrAdd("Taiwan", "TW", "TWN", "158", Continent.Asia);
    public static readonly Country TZ = GetOrAdd("Tanzania", "TZ", "TZA", "834", Continent.Africa);
    public static readonly Country TH = GetOrAdd("Thailand", "TH", "THA", "764", Continent.Asia);
    public static readonly Country TG = GetOrAdd("Togo", "TG", "TGO", "768", Continent.Africa);
    public static readonly Country TK = GetOrAdd("Tokelau", "TK", "TKL", "772", Continent.Oceania);
    public static readonly Country TO = GetOrAdd("Tonga", "TO", "TON", "776", Continent.Oceania);
    public static readonly Country UA = GetOrAdd("Ukraine", "UA", "UKR", "804", Continent.Europe);
    public static readonly Country TT = GetOrAdd("Trinidad and Tobago", "TT", "TTO", "780", Continent.NorthAmerica);
    public static readonly Country TN = GetOrAdd("Tunisia", "TN", "TUN", "788", Continent.Africa);
    public static readonly Country TR = GetOrAdd("Turkey", "TR", "TUR", "792", Continent.Asia);
    public static readonly Country TM = GetOrAdd("Turkmenistan", "TM", "TKM", "795", Continent.Asia);
    public static readonly Country TC = GetOrAdd("Turks and Caicos Islands", "TC", "TCA", "796", Continent.NorthAmerica);
    public static readonly Country TV = GetOrAdd("Tuvalu", "TV", "TUV", "798", Continent.Oceania);
    public static readonly Country UG = GetOrAdd("Uganda", "UG", "UGA", "800", Continent.Africa);
    public static readonly Country KP = GetOrAdd("North Korea", "KP", "PRK", "408", Continent.Asia);
    public static readonly Country EC = GetOrAdd("Ecuador", "EC", "ECU", "218", Continent.SouthAmerica);
    public static readonly Country AE = GetOrAdd("United Arab Emirates", "AE", "ARE", "784", Continent.Asia);
    public static readonly Country GT = GetOrAdd("Guatemala", "GT", "GTM", "320", Continent.NorthAmerica);
    public static readonly Country JP = GetOrAdd("Japan", "JP", "JPN", "392", Continent.Asia);
    public static readonly Country IE = GetOrAdd("Ireland", "IE", "IRL", "372", Continent.Europe);
    public static readonly Country TJ = GetOrAdd("Tajikistan", "TJ", "TJK", "762", Continent.Asia);
    public static readonly Country UM = GetOrAdd("United States Minor Outlying Islands", "UM", "UMI", "581", Continent.Oceania);
    public static readonly Country VN = GetOrAdd("Vietnam", "VN", "VNM", "704", Continent.Asia);
    public static readonly Country BG = GetOrAdd("Bulgaria", "BG", "BGR", "100", Continent.Europe);
    public static readonly Country US = GetOrAdd("United States of America", "US", "USA", "840", Continent.NorthAmerica);
    public static readonly Country UY = GetOrAdd("Uruguay", "UY", "URY", "858", Continent.SouthAmerica);
    public static readonly Country VA = GetOrAdd("Vatican City", "VA", "VAT", "336", Continent.Europe);
    public static readonly Country VI = GetOrAdd("Virgin Islands", "VI", "VIR", "850", Continent.NorthAmerica);
    public static readonly Country VC = GetOrAdd("Saint Vincent and the Grenadines", "VC", "VCT", "670", Continent.NorthAmerica);
    public static readonly Country EH = GetOrAdd("Western Sahara", "EH", "ESH", "732", Continent.Africa);
    public static readonly Country YE = GetOrAdd("Yemen", "YE", "YEM", "887", Continent.Asia);

    private static bool TryFind(Func<Country, bool> predicate, out Country country)
    {
        lock (Items)
        {
            var entry = Items.FirstOrDefault(predicate);
            if (entry != default)
            {
                country = entry;
                return true;
            }
        }

        country = default;
        return false;
    }

    public static bool TryGetByName(string countryName, out Country country)
        => TryFind(c => NameComparer.Equals(countryName, c.Name), out country);
    
    public static bool TryGetByTwoLetterCode(string twoLetterCode, out Country country)
        => TryFind(c => TwoLetterCodeComparer.Equals(twoLetterCode, c.TwoLetterCode), out country);
    
    public static bool TryGetByThreeLetterCode(string threeLetterCode, out Country country)
        => TryFind(c => ThreeLetterCodeComparer.Equals(threeLetterCode, c.ThreeLetterCode), out country);
    
    public static bool TryGetByNumericCode(string numericCode, out Country country)
        => TryFind(c => NumericCodeComparer.Equals(numericCode, c.NumericCode), out country);

    public static bool TryGetByNumericCode(int numericCode, out Country country)
        => TryGetByNumericCode(numericCode.ToString("D3"), out country);

    public static Country GetByNumericCode(int numericCode)
    {
        if (!TryGetByNumericCode(numericCode, out var country))
            throw new ArgumentOutOfRangeException(nameof(numericCode), numericCode, null);

        return country;
    }

    public override bool Equals(object? obj)
        => obj is Country country && Equals(country);

    public bool Equals(Country other)
        => Equals(this, other);
    
    public static bool Equals(Country left, Country right)
        => NumericCodeComparer.Equals(left.NumericCode, right.NumericCode);
    
    public static bool operator ==(Country left, Country right)
        => Equals(left, right);
    
    public static bool operator !=(Country left, Country right)
        => !Equals(left, right);

    public override int GetHashCode()
    {
        var hashCodeBuilder = new HashCode();
        
        hashCodeBuilder.Add(NumericCode, NumericCodeComparer);
        
        var hashCode = hashCodeBuilder.ToHashCode();
        return hashCode;
    }

    public override string ToString()
        => ToString(CountryFormat.ThreeLetterCode);

    public int CompareTo(object? obj)
    {
        if (obj is not Country country)
            throw new ArgumentException($"Object must be of type {nameof(Country)}", nameof(obj));

        return CompareTo(country);
    }
    
    public int CompareTo(Country other)
        => TwoLetterCodeComparer.Compare(TwoLetterCode, other.TwoLetterCode);

    public string ToString(CountryFormat format)
    {
        return format switch
        {
            CountryFormat.Name => Name,
            CountryFormat.TwoLetterCode => TwoLetterCode,
            CountryFormat.ThreeLetterCode => ThreeLetterCode,
            CountryFormat.NumericCode => NumericCode,
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }

    public static Country Parse(string s)
        => Parse(s, format: null);
    
    public static Country Parse(string s, CountryFormat? format)
    {
        if (!TryParse(s, format, out var country))
            throw new FormatException();

        return country;
    }
    
    public static bool TryParse(string s, out Country country)
        => TryParse(s, null, out country);
    
    public static bool TryParse(string s, CountryFormat? format, out Country country)
    {
        switch (format)
        {
            case null or CountryFormat.Name when TryGetByName(s, out country):
            case null or CountryFormat.TwoLetterCode when TryGetByTwoLetterCode(s, out country):
            case null or CountryFormat.ThreeLetterCode when TryGetByThreeLetterCode(s, out country):
            case null or CountryFormat.NumericCode when TryGetByNumericCode(s, out country):
                return true;
            default:
                country = default;
                return false;
        }
    }
    
    private static Country GetOrAdd(string name, string twoLetterCode, string threeLetterCode, string numericCode,
        Continent continent)
    {
        lock (Items)
        {
            var item = new Country(name, twoLetterCode, threeLetterCode, numericCode, continent);
            if (Items.Contains(item))
                return item;

            if (TryGetByNumericCode(numericCode, out var existingItem) && existingItem != item)
                throw new InvalidOperationException("Country with same ISO 3166-1 numeric already exists.");
            
            if (TryGetByThreeLetterCode(threeLetterCode, out existingItem) && existingItem != item)
                throw new InvalidOperationException("Country with same ISO 3166-1 alpha3 already exists.");
            
            if (TryGetByTwoLetterCode(twoLetterCode, out existingItem) && existingItem != item)
                throw new InvalidOperationException("Country with same ISO 3166-1 alpha2 already exists.");
            
            if (TryGetByName(name, out existingItem) && existingItem != item)
                throw new InvalidOperationException("Country with same name already exists.");

            var index = Items.Count;
            
            Items.Add(item);
            TwoLetterCodeCountryIndexMap[twoLetterCode] = index;
            ThreeLetterCodeCountryIndexMap[threeLetterCode] = index;
            
            return item;
        }
    }
}