using System;
using System.Collections.Generic;

namespace FeatureToggle.Domain.Entity.BusinessSchema;

/// <summary>
/// The table holds the information about the business
/// </summary>
public partial class Business
{
    public int BusinessId { get; set; }

    public string BusinessName { get; set; } = null!;

    public string? DomainName { get; set; }

    /// <summary>
    /// The enum column denotes the current status of the business
    /// </summary>
    public byte StatusId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTimeOffset? ModifiedOn { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public int CityId { get; set; }

    public int CountryId { get; set; }

    public string DialingCode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string Phone { get; set; } = null!;

    public int RepresentativeId { get; set; }

    public string RepresentativeRelationshipTitle { get; set; } = null!;

    public byte RepresentativeRelationshipType { get; set; }

    public string? RepresentativeSsn { get; set; }

    public int StateId { get; set; }

    public string? TaxId { get; set; }

    public string Website { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public int NameStartsWith { get; set; }

    public int? BusinessIdentifier { get; set; }

    public string? BusinessOnboardingId { get; set; }

    public virtual ICollection<BusinessFeatureFlag> BusinessFeatureFlags { get; set; } = new List<BusinessFeatureFlag>();
}
