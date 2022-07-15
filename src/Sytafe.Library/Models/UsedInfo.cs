#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using Sytafe.Library.Extensions;

namespace Sytafe.Library.Models;

public class UsedInfo : Abstract
{
    public string DayOfWeek { get; set; } = string.Empty;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public UserInfo User { get; set; }

    [NotMapped]
    public string DisplayFrom => From.ToDateTimeString();
    [NotMapped]
    public string DisplayTo => To.ToDateTimeString();
    [NotMapped]
    public int TotalMinutes
    {
        get
        {
            var to = To;
            if (to == DateTime.MinValue)
            {
                to = DateTime.Now;
            }
            var totalMinutes = (to - From).TotalMinutes;
            return totalMinutes.ToInt32();
        }
    }
    [NotMapped]
    public string TotalMinutesString => TotalMinutes.ToString("N0");

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