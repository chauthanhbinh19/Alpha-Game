using System;
using System.Collections;
using System.Collections.Generic;
public class DailyCheckin
{
    public string id { get; set; }
    public DateTime date { get; set; }
    public int month { get; set; }
    public int year { get; set; }
    public string type { get; set; }
    public string object_id { get; set; }
    public int quantity { get; set; }
    public UserDailyCheckin UserDailyCheckin { get; set; }
}