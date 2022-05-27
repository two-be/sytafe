#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace Sytafe.Library.Models;

public class UsedInfo : Abstract
{
    public DateTime From { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public DateTime To { get; set; }
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public UserInfo User { get; set; }

    public UsedInfo ToInfo()
    {
        if (User is not null)
        {
            User.Useds = new List<UsedInfo>();
            User.ToInfo();
        }
        return this;
    }
}