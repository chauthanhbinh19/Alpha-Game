public class FlatClassDTO
{
    public string id;
    public string sub_type;
    public string sub_image;
    public string main_type;
    public string main_image;
    public int? movement_range;  // Sử dụng nullable để dễ kiểm tra nếu DB trả về null
    public int? movement_point;
    public int? attack_range;
}