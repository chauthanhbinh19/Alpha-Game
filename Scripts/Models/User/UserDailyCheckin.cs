using System;
using System.Collections;
using System.Collections.Generic;
public class UserDailyCheckin
{
    public string UserId { get; set; }
    public string DailyCheckinId { get; set; }
    public bool Status { get; set; }
    public DateTime Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public DailyCheckin DailyCheckin { get; set; }
}