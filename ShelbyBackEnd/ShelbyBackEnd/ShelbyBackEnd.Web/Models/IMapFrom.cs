using AutoMapper;

namespace ShelbyBackEnd.Web.Models
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }

}