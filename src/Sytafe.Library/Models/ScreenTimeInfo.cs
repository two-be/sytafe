#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace Sytafe.Library.Models;

public class ScreenTimeInfo : Abstract
{
    public bool Anytime { get; set; }
    public TimeSpan AvailableFrom { get; set; }
    public TimeSpan AvailableTo { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public int MinuteLimit { get; set; }
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserInfo User { get; set; }

    [NotMapped]
    public string DisplayAvailableTimes => Anytime ? "Anytime" : $"{AvailableFrom.ToString("hh\\:mm")} to {AvailableTo.ToString("hh\\:mm")}";
    [NotMapped]
    public string DisplayMinuteLimit => MinuteLimit == 720 ? string.Empty : $"{MinuteLimit} min";

    public ScreenTimeInfo ToInfo()
    {
        if (User is not null)
        {
            User.ScreenTimes = new List<ScreenTimeInfo>();
            User.ToInfo();
        }
        return this;
    }
}