using System;
using System.Collections;
using System.Collections.Generic;
public class UserDailyCheckin
{
    public string user_id { get; set; }
    public string daily_checkin_id { get; set; }
    public bool status { get; set; }
    public DateTime day { get; set; }
    public int month { get; set; }
    public int year { get; set; }
    public DailyCheckin DailyCheckin { get; set; }
}