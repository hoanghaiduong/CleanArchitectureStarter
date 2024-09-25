
// using MyWebApi.Application.Common.DTO;
// using MyWebApi.Domain.Entities;

// namespace MyWebApi.Application.RoomTypes.Queries
// {
//     public class RoomTypeDTO
//     {
  
//         public string? RoomTypeID { get; init; }
//         public string? Name { get; init; }

//         public string? Description { get; init; }

//         public decimal PricePerNight { get; init; }

//         public int Capacity { get; init; }

//         public virtual IReadOnlyCollection<RoomDTO> Rooms { get; init; } = [];
//         private class Mapping : Profile
//         {
//             public Mapping()
//             {
//                 CreateMap<RoomType, RoomTypeDTO>();
//             }
//         }

//     }

// }