using ApiTaskManagement.DTOs;
using ApiTaskManagement.Entities;
using AutoMapper;
namespace ApiTaskManagement.GeneralConfiguration.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TaskCreateDTO, TaskEntity>();
            CreateMap<TaskUpdateDTO, TaskEntity>();

            //CreateMap<TaskEntity, TaskResponseDTO>()
            //    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Name))
            //    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.Name));
        }
    }

}
