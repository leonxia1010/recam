using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Remp.Model.Enums;

namespace Remp.Model.Entities;

public class ListingCase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public AustralianState State { get; set; }
    public string PostCode { get; set; } = string.Empty;
    public double Price { get; set; }
    [Column(TypeName = "decimal(18, 6)")]
    public decimal Longitude { get; set; }
    [Column(TypeName = "decimal(18,6)")]
    public decimal Latitude { get; set; }

    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int Garages { get; set; }
    public double FloorArea { get; set; }

    public PropertyType PropertyType { get; set; }
    public SaleCategory SaleCategory { get; set; }
    public ListingCaseStatus ListCaseStatus { get; set; }

    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    public ICollection<Agent> Agents { get; set; } = new List<Agent>();
    public ICollection<CaseContact> CaseContacts { get; set; } = new List<CaseContact>();
    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();

}
