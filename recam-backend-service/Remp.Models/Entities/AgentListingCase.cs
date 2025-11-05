using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remp.Model.Entities;

public class AgentListingCase
{
    public string AgentId { get; set; } = string.Empty;
    [ForeignKey(nameof(AgentId))]
    public Agent Agent { get; set; } = null!;
    public int ListingCaseId { get; set; }
    [ForeignKey(nameof(ListingCaseId))]
    public ListingCase ListingCase { get; set; } = null!;
    public DateTime AssignedAt { get; set; }
    public string AssignedByPhotographyCompanyId { get; set; } = string.Empty;
    [ForeignKey(nameof(AssignedByPhotographyCompanyId))]
    public PhotographyCompany AssignedByPhotographyCompany { get; set; } = null!;
}
