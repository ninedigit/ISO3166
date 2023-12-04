namespace NineDigit.ISO3166;

/// <summary>
/// https://www.iso.org/glossary-for-iso-3166.html
/// </summary>
public enum CountryCodeType
{
    OfficiallyAssigned,
    /// <summary>
    /// codes that have been reserved for a particular use at special request of a national ISO member body, governments
    /// or international organizations. For example, the code UK has been reserved at the request of the United Kingdom
    /// so that it cannot be used for any other country.
    /// </summary>
    ExceptionallyReserved,
    /// <summary>
    /// codes that are reserved during a transitional period while new code elements that may replace them are taken
    /// into use. This results from changes in the standard. For example, the country codes for the former Yugoslavia
    /// have been reserved when it was removed from ISO 3166-1.
    /// </summary>
    TransitionallyReserved,
    /// <summary>
    /// a code that has been indeterminately reserved for use in a certain way. Usually this is justified by their
    /// presence in other coding systems. For example, several codes have been reserved by the World Intellectual
    /// Property Organization (WIPO) because they have been used in its Standard ST.3.
    /// </summary>
    IndeterminatelyReserved,
    /// <summary>
    /// Codes that used to be part of the standard but that are no longer in use. See alpha-4 codes above.
    /// </summary>
    FormerlyUsed,
    /// <summary>
    /// If users need code elements to represent country names not included in ISO 3166-1, the series of letters AA, QM
    /// to QZ, XA to XZ, and ZZ, and the series AAA to AAZ, QMA to QZZ, XAA to XZZ, and ZZA to ZZZ respectively, and the
    /// series of numbers 900 to 999 are available.
    /// NOTE: Please be advised that the above series of codes are not universal, those code elements are not compatible
    /// between different entities.
    /// </summary>
    UserAssigned
}