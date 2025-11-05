using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Remp.Model.Enums;

namespace Remp.Model.Entities;

/// <summary>
/// Media asset (photo/video/floor plan) uploaded for a listing case
/// </summary>
public class MediaAsset
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>Media type (Photo/Video/FloorPlan/VRTour)</summary>
    public MediaType MediaType { get; set; }
    /// <summary>Media file storage URL</summary>
    public string MediaUrl { get; set; } = string.Empty;

    /// <summary>Upload timestamp</summary>
    public DateTime UploadedAt { get; set; }
    /// <summary>Whether agent has selected this media for display</summary>
    public bool IsSelected { get; set; }
    /// <summary>Whether this is the hero/cover image</summary>
    public bool IsHero { get; set; }
    /// <summary>Soft delete flag</summary>
    public bool IsDeleted { get; set; }

    /// <summary>Uploader user ID</summary>
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    /// <summary>Associated listing case ID</summary>
    public int ListingCaseId { get; set; }
    [ForeignKey(nameof(ListingCaseId))]
    public ListingCase ListingCase { get; set; } = null!;
}
