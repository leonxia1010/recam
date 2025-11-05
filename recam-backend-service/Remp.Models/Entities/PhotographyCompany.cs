using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Remp.Model.Entities;

/// <summary>
/// Photography company profile (Admin role)
/// </summary>
public class PhotographyCompany
{
    [Key]
    public string Id { get; set; } = string.Empty;
    /// <summary>Photography company name</summary>
    public string PhotographyCompanyName { get; set; } = string.Empty;

    /// <summary>Associated user ID</summary>
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    /// <summary>Agents working with this photography company</summary>
    public ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public PhotographyCompany()
    {
        Id = Guid.NewGuid().ToString();
    }
}
