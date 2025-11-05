using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Remp.Model.Enums;

namespace Remp.Model.Entities;

/// <summary>
/// Property listing case created by photography company
/// </summary>
public class ListingCase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>Property listing title</summary>
    [Required]
    public string Title { get; set; } = string.Empty;
    /// <summary>Property description</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Street address</summary>
    public string Street { get; set; } = string.Empty;
    /// <summary>City</summary>
    public string City { get; set; } = string.Empty;
    /// <summary>Australian state</summary>
    public AustralianState State { get; set; }
    /// <summary>Postal code</summary>
    public string PostCode { get; set; } = string.Empty;
    /// <summary>Property price</summary>
    public double Price { get; set; }
    /// <summary>Geographic longitude</summary>
    [Column(TypeName = "decimal(18, 6)")]
    public decimal Longitude { get; set; }
    /// <summary>Geographic latitude</summary>
    [Column(TypeName = "decimal(18,6)")]
    public decimal Latitude { get; set; }

    /// <summary>Number of bedrooms</summary>
    public int Bedrooms { get; set; }
    /// <summary>Number of bathrooms</summary>
    public int Bathrooms { get; set; }
    /// <summary>Number of garages</summary>
    public int Garages { get; set; }
    /// <summary>Floor area in square meters</summary>
    public double FloorArea { get; set; }

    /// <summary>Property type (House/Unit/Townhouse/Villa/Others)</summary>
    public PropertyType PropertyType { get; set; }
    /// <summary>Sale category (ForSale/ForRent/Auction)</summary>
    public SaleCategory SaleCategory { get; set; }
    /// <summary>Listing case status (Created/Pending/Delivered)</summary>
    public ListingCaseStatus ListCaseStatus { get; set; }

    /// <summary>Case creation timestamp</summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>Soft delete flag</summary>
    public bool IsDeleted { get; set; }

    /// <summary>Creator user ID</summary>
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    /// <summary>Agents assigned to this listing case</summary>
    public ICollection<Agent> Agents { get; set; } = new List<Agent>();
    /// <summary>Contact persons for this listing</summary>
    public ICollection<CaseContact> CaseContacts { get; set; } = new List<CaseContact>();
    /// <summary>Media assets associated with this listing</summary>
    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();

}
