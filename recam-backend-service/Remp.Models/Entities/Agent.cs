using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remp.Model.Entities;

/// <summary>
/// Real estate agent profile
/// </summary>
public class Agent
{
    [Key]
    public string Id { get; set; } = string.Empty;

    /// <summary>Agent first name</summary>
    public string AgentFirstName { get; set; } = string.Empty;
    /// <summary>Agent last name</summary>
    public string AgentLastName { get; set; } = string.Empty;
    /// <summary>Agent avatar image URL</summary>
    public string AvatarUrl { get; set; } = string.Empty;
    /// <summary>Agent company name</summary>
    public string AgentCompanyName { get; set; } = string.Empty;

    /// <summary>Associated user ID</summary>
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    /// <summary>Listing cases assigned to this agent</summary>
    public ICollection<ListingCase> ListingCases { get; set; } = new List<ListingCase>();
    /// <summary>Photography companies this agent works with</summary>
    public ICollection<PhotographyCompany> PhotographyCompanies { get; set; } = new List<PhotographyCompany>();

    public Agent()
    {
        Id = Guid.NewGuid().ToString();
    }
}

