using System;

public interface ICard
{
    string Id { get; }
    string Name { get; set; }
    string Image { get; }
    string Rare { get; set; }
    string Type { get; set; }
    public int Level { get; set; }
    string Position { get; }
    double Power {get; }
    Teams Team { get; set; }
}