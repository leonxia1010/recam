using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Remp.Model.Entities;

public class PhotographyCompany
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string PhotographyCompanyName { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    public ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public PhotographyCompany()
    {
        Id = Guid.NewGuid().ToString();
    }
}
