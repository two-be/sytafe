#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using Sytafe.Library.Extensions;

namespace Sytafe.Library.Models;

public class UserInfo : Abstract
{
    public string Password { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public List<ScreenTimeInfo> ScreenTimes { get; set; } = new List<ScreenTimeInfo>();
    public List<UsedInfo> Useds { get; set; } = new List<UsedInfo>();

    [NotMapped]
    public bool IsAdministrator => Type == "administrator";
    [NotMapped]
    public int MinuteLeft
    {
        get
        {
            if (TodayScreenTime is not null)
            {
                var minuteLeft = TodayScreenTime.MinuteLimit;
                var usedMinute = TodayUseds.Sum(x => (x.To - x.From).TotalMinutes.ToInt32());
                minuteLeft = minuteLeft - usedMinute;
                if (!TodayScreenTime.Anytime && minuteLeft > 0)
                {
                    var availableLeft = (TodayScreenTime.AvailableTo - DateTime.Now.TimeOfDay).TotalMinutes.ToInt32();
                    if (minuteLeft > availableLeft)
                    {
                        minuteLeft = availableLeft + 1;
                    }
                }
                return minuteLeft;
            }
            return 2;
        }
    }
    [NotMapped]
    public ScreenTimeInfo TodayScreenTime => ScreenTimes.FirstOrDefault(x => x.DayOfWeek == DateTimeOffset.Now.DayOfWeek.ToString());
    [NotMapped]
    public List<UsedInfo> TodayUseds => Useds.Where(x => x.From.Date == DateTimeOffset.Now.Date).ToList();
    [NotMapped]
    public string Token { get; set; } = string.Empty;

    public UserInfo ToInfo()
    {
        Password = string.Empty;
        ScreenTimes = (ScreenTimes ?? new List<ScreenTimeInfo>()).Select(x =>
        {
            x.User = null;
            return x;
        }).ToList();
        Useds = (Useds ?? new List<UsedInfo>()).Select(x =>
        {
            x.User = null;
            return x;
        }).ToList();
        return this;
    }

    public string Validate()
    {
        if (string.IsNullOrEmpty(Username))
        {
            return "Please enter a valid Username.";
        }
        if (string.IsNullOrEmpty(Password))
        {
            return "Please enter a valid Password.";
        }
        return string.Empty;
    }
}