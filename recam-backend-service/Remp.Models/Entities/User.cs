using System;
using Microsoft.AspNetCore.Identity;
using Remp.Model.Enums;

namespace Remp.Model.Entities;

public class User : IdentityUser
{
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserProfile UserProfile { get; set; }

    public Agent? Agent { get; set; }
    public PhotographyCompany? PhotographyCompany { get; set; }

    public ICollection<ListingCase> ListingCases { get; set; } = new List<ListingCase>();
    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
}
