using System;
using Microsoft.Extensions.Options;

namespace Remp.Model.Settings;

public class EmailSettings
{
    public string Name { get; set; } = string.Empty;
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string SenderEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
