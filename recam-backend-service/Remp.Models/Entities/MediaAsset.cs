using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Remp.Model.Enums;

namespace Remp.Model.Entities;

public class MediaAsset
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public MediaType MediaType { get; set; }
    public string MediaUrl { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; }
    public bool IsSelected { get; set; }
    public bool IsHero { get; set; }
    public bool IsDeleted { get; set; }

    public int ListingCaseId { get; set; }
    [ForeignKey(nameof(ListingCaseId))]
    public ListingCase ListingCase { get; set; } = null!;
    // TODO Comment out when user model is ready
    // public string UserId { get; set; } = string.Empty;
    // [ForeignKey(nameof(UserId))]
    // public User User { get; set; } =  null!;
}
