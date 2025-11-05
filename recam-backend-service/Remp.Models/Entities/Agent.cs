using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remp.Model.Entities;

public class Agent
{
    [Key]
    public string Id { get; set; } = string.Empty;

    public string AgentFirstName { get; set; } = string.Empty;
    public string AgentLastName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string AgentCompanyName { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    public ICollection<ListingCase> ListingCases { get; set; } = new List<ListingCase>();
    public ICollection<PhotographyCompany> PhotographyCompanies { get; set; } = new List<PhotographyCompany>();

    public Agent()
    {
        Id = Guid.NewGuid().ToString();
    }
}

