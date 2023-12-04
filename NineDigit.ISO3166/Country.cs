using System.ComponentModel;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global

namespace NineDigit.ISO3166;

/// <summary>
/// List of ISO 3166 countries
/// </summary>
[TypeConverter(typeof(CountryTypeConverter))]
public readonly struct Country : IEquatable<Country>, IComparable<Country>, IComparable
{
    private static readonly HashSet<Country> _entries = new();
    
    private Country(
        string countryName,
        string twoLetterCode,
        string threeLetterCode,
        string numericCode,
        CountryCodeType codeType = CountryCodeType.OfficiallyAssigned)
    {
        CountryName = countryName;
        TwoLetterCode = twoLetterCode;
        ThreeLetterCode = threeLetterCode;
        NumericCode = numericCode;
        CodeType = codeType;
    }
    
    public string CountryName { get; }
    /// <summary>
    /// A two-letter code that represents a country name, recommended as the general purpose code
    /// </summary>
    public string TwoLetterCode { get; }
    /// <summary>
    /// A three-letter code that represents a country name, which is usually more closely related to the country name
    /// </summary>
    public string ThreeLetterCode { get; }
    public string NumericCode { get; }
    public CountryCodeType CodeType { get; }

    public static IReadOnlyCollection<Country> GetAllOfficiallyAssigned()
    {
        lock (_entries)
            return _entries.ToList();
    }

    public static readonly Country AF = Add("Afghanistan", "AF", "AFG", "004");
    public static readonly Country AL = Add("Albania", "AL", "ALB", "008");
    public static readonly Country DZ = Add("Algeria", "DZ", "DZA", "012");
    public static readonly Country AS = Add("American Samoa", "AS", "ASM", "016");
    public static readonly Country AD = Add("Andorra", "AD", "AND", "020");
    public static readonly Country AO = Add("Angola", "AO", "AGO", "024");
    public static readonly Country AI = Add("Anguilla", "AI", "AIA", "660");
    public static readonly Country AQ = Add("Antarctica", "AQ", "ATA", "010");
    public static readonly Country AG = Add("Antigua and Barbuda", "AG", "ATG", "028");
    public static readonly Country AR = Add("Argentina", "AR", "ARG", "032");
    public static readonly Country AM = Add("Armenia", "AM", "ARM", "051");
    public static readonly Country AW = Add("Aruba", "AW", "ABW", "533");
    public static readonly Country AU = Add("Australia", "AU", "AUS", "036");
    public static readonly Country AT = Add("Austria", "AT", "AUT", "040");
    public static readonly Country AZ = Add("Azerbaijan", "AZ", "AZE", "031");
    public static readonly Country BS = Add("Bahamas (the", "BS", "BHS", "044");
    public static readonly Country BH = Add("Bahrain", "BH", "BHR", "048");
    public static readonly Country BD = Add("Bangladesh", "BD", "BGD", "050");
    public static readonly Country BB = Add("Barbados", "BB", "BRB", "052");
    public static readonly Country BY = Add("Belarus", "BY", "BLR", "112");
    public static readonly Country BE = Add("Belgium", "BE", "BEL", "056");
    public static readonly Country BZ = Add("Belize", "BZ", "BLZ", "084");
    public static readonly Country BJ = Add("Benin", "BJ", "BEN", "204");
    public static readonly Country BM = Add("Bermuda", "BM", "BMU", "060");
    public static readonly Country BT = Add("Bhutan", "BT", "BTN", "064");
    public static readonly Country BO = Add("Bolivia (Plurinational State of", "BO", "BOL", "068");
    public static readonly Country BQ = Add("Bonaire, Sint Eustatius and Saba", "BQ", "BES", "535");
    public static readonly Country BA = Add("Bosnia and Herzegovina", "BA", "BIH", "070");
    public static readonly Country BW = Add("Botswana", "BW", "BWA", "072");
    public static readonly Country BV = Add("Bouvet Island", "BV", "BVT", "074");
    public static readonly Country BR = Add("Brazil", "BR", "BRA", "076");
    public static readonly Country IO = Add("British Indian Ocean Territory (the", "IO", "IOT", "086");
    public static readonly Country BN = Add("Brunei Darussalam", "BN", "BRN", "096");
    public static readonly Country BG = Add("Bulgaria", "BG", "BGR", "100");
    public static readonly Country BF = Add("Burkina Faso", "BF", "BFA", "854");
    public static readonly Country BI = Add("Burundi", "BI", "BDI", "108");
    public static readonly Country CV = Add("Cabo Verde", "CV", "CPV", "132");
    public static readonly Country KH = Add("Cambodia", "KH", "KHM", "116");
    public static readonly Country CM = Add("Cameroon", "CM", "CMR", "120");
    public static readonly Country CA = Add("Canada", "CA", "CAN", "124");
    public static readonly Country KY = Add("Cayman Islands (the", "KY", "CYM", "136");
    public static readonly Country CF = Add("Central African Republic (the", "CF", "CAF", "140");
    public static readonly Country TD = Add("Chad", "TD", "TCD", "148");
    public static readonly Country CL = Add("Chile", "CL", "CHL", "152");
    public static readonly Country CN = Add("China", "CN", "CHN", "156");
    public static readonly Country CX = Add("Christmas Island", "CX", "CXR", "162");
    public static readonly Country CC = Add("Cocos (Keeling) Islands (the", "CC", "CCK", "166");
    public static readonly Country CO = Add("Colombia", "CO", "COL", "170");
    public static readonly Country KM = Add("Comoros (the", "KM", "COM", "174");
    public static readonly Country CD = Add("Congo (the Democratic Republic of the", "CD", "COD", "180");
    public static readonly Country CG = Add("Congo (the", "CG", "COG", "178");
    public static readonly Country CK = Add("Cook Islands (the", "CK", "COK", "184");
    public static readonly Country CR = Add("Costa Rica", "CR", "CRI", "188");
    public static readonly Country HR = Add("Croatia", "HR", "HRV", "191");
    public static readonly Country CU = Add("Cuba", "CU", "CUB", "192");
    public static readonly Country CW = Add("Curaçao", "CW", "CUW", "531");
    public static readonly Country CY = Add("Cyprus", "CY", "CYP", "196");
    public static readonly Country CZ = Add("Czechia", "CZ", "CZE", "203");
    public static readonly Country CI = Add("Côte d'Ivoire", "CI", "CIV", "384");
    public static readonly Country DK = Add("Denmark", "DK", "DNK", "208");
    public static readonly Country DJ = Add("Djibouti", "DJ", "DJI", "262");
    public static readonly Country DM = Add("Dominica", "DM", "DMA", "212");
    public static readonly Country DO = Add("Dominican Republic (the", "DO", "DOM", "214");
    public static readonly Country EC = Add("Ecuador", "EC", "ECU", "218");
    public static readonly Country EG = Add("Egypt", "EG", "EGY", "818");
    public static readonly Country SV = Add("El Salvador", "SV", "SLV", "222");
    public static readonly Country GQ = Add("Equatorial Guinea", "GQ", "GNQ", "226");
    public static readonly Country ER = Add("Eritrea", "ER", "ERI", "232");
    public static readonly Country EE = Add("Estonia", "EE", "EST", "233");
    public static readonly Country SZ = Add("Eswatini", "SZ", "SWZ", "748");
    public static readonly Country ET = Add("Ethiopia", "ET", "ETH", "231");
    public static readonly Country FK = Add("Falkland Islands (the) [Malvinas", "FK", "FLK", "238");
    public static readonly Country FO = Add("Faroe Islands (the", "FO", "FRO", "234");
    public static readonly Country FJ = Add("Fiji", "FJ", "FJI", "242");
    public static readonly Country FI = Add("Finland", "FI", "FIN", "246");
    public static readonly Country FR = Add("France", "FR", "FRA", "250");
    public static readonly Country GF = Add("French Guiana", "GF", "GUF", "254");
    public static readonly Country PF = Add("French Polynesia", "PF", "PYF", "258");
    public static readonly Country TF = Add("French Southern Territories (the", "TF", "ATF", "260");
    public static readonly Country GA = Add("Gabon", "GA", "GAB", "266");
    public static readonly Country GM = Add("Gambia (the", "GM", "GMB", "270");
    public static readonly Country GE = Add("Georgia", "GE", "GEO", "268");
    public static readonly Country DE = Add("Germany", "DE", "DEU", "276");
    public static readonly Country GH = Add("Ghana", "GH", "GHA", "288");
    public static readonly Country GI = Add("Gibraltar", "GI", "GIB", "292");
    public static readonly Country GR = Add("Greece", "GR", "GRC", "300");
    public static readonly Country GL = Add("Greenland", "GL", "GRL", "304");
    public static readonly Country GD = Add("Grenada", "GD", "GRD", "308");
    public static readonly Country GP = Add("Guadeloupe", "GP", "GLP", "312");
    public static readonly Country GU = Add("Guam", "GU", "GUM", "316");
    public static readonly Country GT = Add("Guatemala", "GT", "GTM", "320");
    public static readonly Country GG = Add("Guernsey", "GG", "GGY", "831");
    public static readonly Country GN = Add("Guinea", "GN", "GIN", "324");
    public static readonly Country GW = Add("Guinea-Bissau", "GW", "GNB", "624");
    public static readonly Country GY = Add("Guyana", "GY", "GUY", "328");
    public static readonly Country HT = Add("Haiti", "HT", "HTI", "332");
    public static readonly Country HM = Add("Heard Island and McDonald Islands", "HM", "HMD", "334");
    public static readonly Country VA = Add("Holy See (the", "VA", "VAT", "336");
    public static readonly Country HN = Add("Honduras", "HN", "HND", "340");
    public static readonly Country HK = Add("Hong Kong", "HK", "HKG", "344");
    public static readonly Country HU = Add("Hungary", "HU", "HUN", "348");
    public static readonly Country IS = Add("Iceland", "IS", "ISL", "352");
    public static readonly Country IN = Add("India", "IN", "IND", "356");
    public static readonly Country ID = Add("Indonesia", "ID", "IDN", "360");
    public static readonly Country IR = Add("Iran (Islamic Republic of", "IR", "IRN", "364");
    public static readonly Country IQ = Add("Iraq", "IQ", "IRQ", "368");
    public static readonly Country IE = Add("Ireland", "IE", "IRL", "372");
    public static readonly Country IM = Add("Isle of Man", "IM", "IMN", "833");
    public static readonly Country IL = Add("Israel", "IL", "ISR", "376");
    public static readonly Country IT = Add("Italy", "IT", "ITA", "380");
    public static readonly Country JM = Add("Jamaica", "JM", "JAM", "388");
    public static readonly Country JP = Add("Japan", "JP", "JPN", "392");
    public static readonly Country JE = Add("Jersey", "JE", "JEY", "832");
    public static readonly Country JO = Add("Jordan", "JO", "JOR", "400");
    public static readonly Country KZ = Add("Kazakhstan", "KZ", "KAZ", "398");
    public static readonly Country KE = Add("Kenya", "KE", "KEN", "404");
    public static readonly Country KI = Add("Kiribati", "KI", "KIR", "296");
    public static readonly Country KP = Add("Korea (the Democratic People's Republic of", "KP", "PRK", "408");
    public static readonly Country KR = Add("Korea (the Republic of", "KR", "KOR", "410");
    public static readonly Country KW = Add("Kuwait", "KW", "KWT", "414");
    public static readonly Country KG = Add("Kyrgyzstan", "KG", "KGZ", "417");
    public static readonly Country LA = Add("Lao People's Democratic Republic (the", "LA", "LAO", "418");
    public static readonly Country LV = Add("Latvia", "LV", "LVA", "428");
    public static readonly Country LB = Add("Lebanon", "LB", "LBN", "422");
    public static readonly Country LS = Add("Lesotho", "LS", "LSO", "426");
    public static readonly Country LR = Add("Liberia", "LR", "LBR", "430");
    public static readonly Country LY = Add("Libya", "LY", "LBY", "434");
    public static readonly Country LI = Add("Liechtenstein", "LI", "LIE", "438");
    public static readonly Country LT = Add("Lithuania", "LT", "LTU", "440");
    public static readonly Country LU = Add("Luxembourg", "LU", "LUX", "442");
    public static readonly Country MO = Add("Macao", "MO", "MAC", "446");
    public static readonly Country MG = Add("Madagascar", "MG", "MDG", "450");
    public static readonly Country MW = Add("Malawi", "MW", "MWI", "454");
    public static readonly Country MY = Add("Malaysia", "MY", "MYS", "458");
    public static readonly Country MV = Add("Maldives", "MV", "MDV", "462");
    public static readonly Country ML = Add("Mali", "ML", "MLI", "466");
    public static readonly Country MT = Add("Malta", "MT", "MLT", "470");
    public static readonly Country MH = Add("Marshall Islands (the", "MH", "MHL", "584");
    public static readonly Country MQ = Add("Martinique", "MQ", "MTQ", "474");
    public static readonly Country MR = Add("Mauritania", "MR", "MRT", "478");
    public static readonly Country MU = Add("Mauritius", "MU", "MUS", "480");
    public static readonly Country YT = Add("Mayotte", "YT", "MYT", "175");
    public static readonly Country MX = Add("Mexico", "MX", "MEX", "484");
    public static readonly Country FM = Add("Micronesia (Federated States of", "FM", "FSM", "583");
    public static readonly Country MD = Add("Moldova (the Republic of", "MD", "MDA", "498");
    public static readonly Country MC = Add("Monaco", "MC", "MCO", "492");
    public static readonly Country MN = Add("Mongolia", "MN", "MNG", "496");
    public static readonly Country ME = Add("Montenegro", "ME", "MNE", "499");
    public static readonly Country MS = Add("Montserrat", "MS", "MSR", "500");
    public static readonly Country MA = Add("Morocco", "MA", "MAR", "504");
    public static readonly Country MZ = Add("Mozambique", "MZ", "MOZ", "508");
    public static readonly Country MM = Add("Myanmar", "MM", "MMR", "104");
    public static readonly Country NA = Add("Namibia", "NA", "NAM", "516");
    public static readonly Country NR = Add("Nauru", "NR", "NRU", "520");
    public static readonly Country NP = Add("Nepal", "NP", "NPL", "524");
    public static readonly Country NL = Add("Netherlands (Kingdom of the", "NL", "NLD", "528");
    public static readonly Country NC = Add("New Caledonia", "NC", "NCL", "540");
    public static readonly Country NZ = Add("New Zealand", "NZ", "NZL", "554");
    public static readonly Country NI = Add("Nicaragua", "NI", "NIC", "558");
    public static readonly Country NE = Add("Niger (the", "NE", "NER", "562");
    public static readonly Country NG = Add("Nigeria", "NG", "NGA", "566");
    public static readonly Country NU = Add("Niue", "NU", "NIU", "570");
    public static readonly Country NF = Add("Norfolk Island", "NF", "NFK", "574");
    public static readonly Country MK = Add("North Macedonia", "MK", "MKD", "807");
    public static readonly Country MP = Add("Northern Mariana Islands (the", "MP", "MNP", "580");
    public static readonly Country NO = Add("Norway", "NO", "NOR", "578");
    public static readonly Country OM = Add("Oman", "OM", "OMN", "512");
    public static readonly Country PK = Add("Pakistan", "PK", "PAK", "586");
    public static readonly Country PW = Add("Palau", "PW", "PLW", "585");
    public static readonly Country PS = Add("Palestine, State of", "PS", "PSE", "275");
    public static readonly Country PA = Add("Panama", "PA", "PAN", "591");
    public static readonly Country PG = Add("Papua New Guinea", "PG", "PNG", "598");
    public static readonly Country PY = Add("Paraguay", "PY", "PRY", "600");
    public static readonly Country PE = Add("Peru", "PE", "PER", "604");
    public static readonly Country PH = Add("Philippines (the", "PH", "PHL", "608");
    public static readonly Country PN = Add("Pitcairn", "PN", "PCN", "612");
    public static readonly Country PL = Add("Poland", "PL", "POL", "616");
    public static readonly Country PT = Add("Portugal", "PT", "PRT", "620");
    public static readonly Country PR = Add("Puerto Rico", "PR", "PRI", "630");
    public static readonly Country QA = Add("Qatar", "QA", "QAT", "634");
    public static readonly Country RO = Add("Romania", "RO", "ROU", "642");
    public static readonly Country RU = Add("Russian Federation (the", "RU", "RUS", "643");
    public static readonly Country RW = Add("Rwanda", "RW", "RWA", "646");
    public static readonly Country RE = Add("Réunion", "RE", "REU", "638");
    public static readonly Country BL = Add("Saint Barthélemy", "BL", "BLM", "652");
    public static readonly Country SH = Add("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", "654");
    public static readonly Country KN = Add("Saint Kitts and Nevis", "KN", "KNA", "659");
    public static readonly Country LC = Add("Saint Lucia", "LC", "LCA", "662");
    public static readonly Country MF = Add("Saint Martin (French part", "MF", "MAF", "663");
    public static readonly Country PM = Add("Saint Pierre and Miquelon", "PM", "SPM", "666");
    public static readonly Country VC = Add("Saint Vincent and the Grenadines", "VC", "VCT", "670");
    public static readonly Country WS = Add("Samoa", "WS", "WSM", "882");
    public static readonly Country SM = Add("San Marino", "SM", "SMR", "674");
    public static readonly Country ST = Add("Sao Tome and Principe", "ST", "STP", "678");
    public static readonly Country SA = Add("Saudi Arabia", "SA", "SAU", "682");
    public static readonly Country SN = Add("Senegal", "SN", "SEN", "686");
    public static readonly Country RS = Add("Serbia", "RS", "SRB", "688");
    public static readonly Country SC = Add("Seychelles", "SC", "SYC", "690");
    public static readonly Country SL = Add("Sierra Leone", "SL", "SLE", "694");
    public static readonly Country SG = Add("Singapore", "SG", "SGP", "702");
    public static readonly Country SX = Add("Sint Maarten (Dutch part", "SX", "SXM", "534");
    public static readonly Country SK = Add("Slovakia", "SK", "SVK", "703");
    public static readonly Country SI = Add("Slovenia", "SI", "SVN", "705");
    public static readonly Country SB = Add("Solomon Islands", "SB", "SLB", "090");
    public static readonly Country SO = Add("Somalia", "SO", "SOM", "706");
    public static readonly Country ZA = Add("South Africa", "ZA", "ZAF", "710");
    public static readonly Country GS = Add("South Georgia and the South Sandwich Islands", "GS", "SGS", "239");
    public static readonly Country SS = Add("South Sudan", "SS", "SSD", "728");
    public static readonly Country ES = Add("Spain", "ES", "ESP", "724");
    public static readonly Country LK = Add("Sri Lanka", "LK", "LKA", "144");
    public static readonly Country SD = Add("Sudan (the", "SD", "SDN", "729");
    public static readonly Country SR = Add("Suriname", "SR", "SUR", "740");
    public static readonly Country SJ = Add("Svalbard and Jan Mayen", "SJ", "SJM", "744");
    public static readonly Country SE = Add("Sweden", "SE", "SWE", "752");
    public static readonly Country CH = Add("Switzerland", "CH", "CHE", "756");
    public static readonly Country SY = Add("Syrian Arab Republic (the", "SY", "SYR", "760");
    public static readonly Country TW = Add("Taiwan (Province of China", "TW", "TWN", "158");
    public static readonly Country TJ = Add("Tajikistan", "TJ", "TJK", "762");
    public static readonly Country TZ = Add("Tanzania, the United Republic of", "TZ", "TZA", "834");
    public static readonly Country TH = Add("Thailand", "TH", "THA", "764");
    public static readonly Country TL = Add("Timor-Leste", "TL", "TLS", "626");
    public static readonly Country TG = Add("Togo", "TG", "TGO", "768");
    public static readonly Country TK = Add("Tokelau", "TK", "TKL", "772");
    public static readonly Country TO = Add("Tonga", "TO", "TON", "776");
    public static readonly Country TT = Add("Trinidad and Tobago", "TT", "TTO", "780");
    public static readonly Country TN = Add("Tunisia", "TN", "TUN", "788");
    public static readonly Country TM = Add("Turkmenistan", "TM", "TKM", "795");
    public static readonly Country TC = Add("Turks and Caicos Islands (the", "TC", "TCA", "796");
    public static readonly Country TV = Add("Tuvalu", "TV", "TUV", "798");
    public static readonly Country TR = Add("Türkiye", "TR", "TUR", "792");
    public static readonly Country UG = Add("Uganda", "UG", "UGA", "800");
    public static readonly Country UA = Add("Ukraine", "UA", "UKR", "804");
    public static readonly Country AE = Add("United Arab Emirates (the", "AE", "ARE", "784");
    public static readonly Country GB = Add("United Kingdom of Great Britain and Northern Ireland (the", "GB", "GBR", "826");
    public static readonly Country UM = Add("United States Minor Outlying Islands (the", "UM", "UMI", "581");
    public static readonly Country US = Add("United States of America (the", "US", "USA", "840");
    public static readonly Country UY = Add("Uruguay", "UY", "URY", "858");
    public static readonly Country UZ = Add("Uzbekistan", "UZ", "UZB", "860");
    public static readonly Country VU = Add("Vanuatu", "VU", "VUT", "548");
    public static readonly Country VE = Add("Venezuela (Bolivarian Republic of", "VE", "VEN", "862");
    public static readonly Country VN = Add("Viet Nam", "VN", "VNM", "704");
    public static readonly Country VG = Add("Virgin Islands (British", "VG", "VGB", "092");
    public static readonly Country VI = Add("Virgin Islands (U.S", "VI", "VIR", "850");
    public static readonly Country WF = Add("Wallis and Futuna", "WF", "WLF", "876");
    public static readonly Country EH = Add("Western Sahara", "EH", "ESH", "732");
    public static readonly Country YE = Add("Yemen", "YE", "YEM", "887");
    public static readonly Country ZM = Add("Zambia", "ZM", "ZMB", "894");
    public static readonly Country ZW = Add("Zimbabwe", "ZW", "ZWE", "716");
    public static readonly Country AX = Add("Åland Islands", "AX", "ALA", "248");

    private static Country Add(
        string countryName,
        string twoLetterCode,
        string threeLetterCode,
        string numericCode,
        CountryCodeType codeType = CountryCodeType.OfficiallyAssigned)
    {
        var country = new Country(countryName, twoLetterCode, threeLetterCode, numericCode, codeType);
        
        lock (_entries)
            _entries.Add(country);

        return country;
    }

    private static bool TryFind(Func<Country, bool> predicate, out Country country)
    {
        lock (_entries)
        {
            var entry = _entries.FirstOrDefault(predicate);
            if (entry != default)
            {
                country = entry;
                return true;
            }
        }

        country = default;
        return false;
    }

    public static bool TryGetByCountryName(string countryName, out Country country)
        => TryFind(c => StringComparer.Ordinal.Equals(countryName, c.CountryName), out country);
    
    public static bool TryGetByTwoLetterCode(string twoLetterCode, out Country country)
        => TryFind(c => StringComparer.Ordinal.Equals(twoLetterCode, c.TwoLetterCode), out country);
    
    public static bool TryGetByThreeLetterCode(string threeLetterCode, out Country country)
        => TryFind(c => StringComparer.Ordinal.Equals(threeLetterCode, c.ThreeLetterCode), out country);
    
    public static bool TryGetByNumericCode(string numericCode, out Country country)
        => TryFind(c => StringComparer.Ordinal.Equals(numericCode, c.NumericCode), out country);

    public static bool TryGetByNumericCode(int numericCode, out Country country)
        => TryGetByNumericCode(numericCode.ToString("D4"), out country);

    public static Country GetByNumericCode(int numericCode)
    {
        if (!TryGetByNumericCode(numericCode.ToString("D3"), out var country))
            throw new ArgumentOutOfRangeException(nameof(numericCode), numericCode, null);

        return country;
    }

    public override int GetHashCode()
    {
        var hashCodeBuilder = new HashCode();

        hashCodeBuilder.Add(CountryName, StringComparer.Ordinal);
        hashCodeBuilder.Add(TwoLetterCode, StringComparer.Ordinal);
        hashCodeBuilder.Add(ThreeLetterCode, StringComparer.Ordinal);
        hashCodeBuilder.Add(NumericCode, StringComparer.Ordinal);
        hashCodeBuilder.Add(CodeType);
        
        return hashCodeBuilder.ToHashCode();
    }

    public override bool Equals(object? obj)
        => obj is Country country && Equals(country);

    public bool Equals(Country other)
        => Equals(this, other);

    public static bool Equals(Country left, Country right)
    {
        return StringComparer.Ordinal.Equals(left.CountryName, right.CountryName) &&
               StringComparer.Ordinal.Equals(left.TwoLetterCode, right.TwoLetterCode) &&
               StringComparer.Ordinal.Equals(left.ThreeLetterCode, right.ThreeLetterCode) &&
               StringComparer.Ordinal.Equals(left.NumericCode, right.NumericCode) &&
               left.CodeType == right.CodeType;
    }
    
    public static bool operator ==(Country left, Country right)
        => Equals(left, right);
    
    public static bool operator !=(Country left, Country right)
        => !Equals(left, right);

    public override string ToString()
        => ToString(CountryFormat.ThreeLetterCode);

    public int CompareTo(object? obj)
    {
        if (obj is not Country country)
            throw new ArgumentException($"Object must be of type {nameof(Country)}", nameof(obj));

        return CompareTo(country);
    }
    
    public int CompareTo(Country other)
        => StringComparer.Ordinal.Compare(NumericCode, other.NumericCode);

    public string ToString(CountryFormat format)
    {
        return format switch
        {
            CountryFormat.CountryName => CountryName,
            CountryFormat.TwoLetterCode => TwoLetterCode,
            CountryFormat.ThreeLetterCode => ThreeLetterCode,
            CountryFormat.NumericCode => NumericCode,
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }

    public static Country Parse(string s)
    {
        if (!TryParse(s, out Country country))
            throw new FormatException();

        return country;
    }
    
    public static bool TryParse(string s, out Country country)
        => TryParse(s, null, out country);
    
    public static bool TryParse(string s, CountryFormat? format, out Country country)
    {
        switch (format)
        {
            case null or CountryFormat.CountryName when TryGetByCountryName(s, out country):
            case null or CountryFormat.TwoLetterCode when TryGetByTwoLetterCode(s, out country):
            case null or CountryFormat.ThreeLetterCode when TryGetByThreeLetterCode(s, out country):
            case null or CountryFormat.NumericCode when TryGetByNumericCode(s, out country):
                return true;
            default:
                country = default;
                return false;
        }
    }
}