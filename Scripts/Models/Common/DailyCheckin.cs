using System;
using System.Collections;
using System.Collections.Generic;
public class DailyCheckin
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string Type { get; set; }
    public string ObjectId { get; set; }
    public int Quantity { get; set; }
    public UserDailyCheckin UserDailyCheckin { get; set; }
}