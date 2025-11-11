using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Remp.Application.DTOs.Email;

public class EmailRequestDTO
{
    public string ReceiverEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
