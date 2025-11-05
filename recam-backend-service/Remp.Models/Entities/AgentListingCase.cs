using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remp.Model.Entities;

/// <summary>
/// Junction table for Agent-ListingCase many-to-many relationship with assignment tracking
/// </summary>
public class AgentListingCase
{
    /// <summary>Assigned agent ID</summary>
    public string AgentId { get; set; } = string.Empty;
    [ForeignKey(nameof(AgentId))]
    public Agent Agent { get; set; } = null!;
    /// <summary>Listing case ID</summary>
    public int ListingCaseId { get; set; }
    [ForeignKey(nameof(ListingCaseId))]
    public ListingCase ListingCase { get; set; } = null!;
    /// <summary>Assignment timestamp</summary>
    public DateTime AssignedAt { get; set; }
    /// <summary>Photography company that made the assignment</summary>
    public string AssignedByPhotographyCompanyId { get; set; } = string.Empty;
    [ForeignKey(nameof(AssignedByPhotographyCompanyId))]
    public PhotographyCompany AssignedByPhotographyCompany { get; set; } = null!;
}
