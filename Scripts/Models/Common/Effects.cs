public class Effects
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EffectType { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public string ValueType { get; set; }
    public int Value { get; set; }
    public float ScalingFactor { get; set; }
    public EffectProperty EffectProperty { get; set; }
    public EffectAction EffectAction{ get; set; }
}