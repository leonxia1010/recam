using System;
using Microsoft.AspNetCore.Identity;
using Remp.Model.Enums;

namespace Remp.Model.Entities;

/// <summary>
/// System user (Admin or Agent role)
/// </summary>
public class User : IdentityUser
{
    /// <summary>Soft delete flag</summary>
    public bool IsDeleted { get; set; }
    /// <summary>User account creation time</summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>User profile type (None/Agent/PhotographyCompany)</summary>
    public UserProfile UserProfile { get; set; }

    /// <summary>Agent profile (if user is Agent)</summary>
    public Agent? Agent { get; set; }
    /// <summary>Photography company profile (if user is Admin)</summary>
    public PhotographyCompany? PhotographyCompany { get; set; }

    /// <summary>Listing cases created by this user</summary>
    public ICollection<ListingCase> ListingCases { get; set; } = new List<ListingCase>();
    /// <summary>Media assets uploaded by this user</summary>
    public ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
}
