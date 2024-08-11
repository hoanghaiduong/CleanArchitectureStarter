namespace MyWebApi.Domain.Entities;

public partial class RoomType
{
    public int TypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? PricePerNight { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
