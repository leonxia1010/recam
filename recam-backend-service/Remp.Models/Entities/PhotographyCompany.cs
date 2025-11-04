using System;
using System.ComponentModel.DataAnnotations;


namespace Remp.Model.Entities;

public class PhotographyCompany
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string PhotographyCompanyName { get; set; } = string.Empty;

    public ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public PhotographyCompany()
    {
        Id = Guid.NewGuid().ToString();
    }
}
