using AutoMapper;
using viagem_api.Data.Dtos;
using viagem_api.Models;

namespace viagem_api.Profiles;

public class DestinoProfile : Profile
{
    public DestinoProfile()
    {
        CreateMap<CreateDestinoDto, Destino>();
        CreateMap<Destino, ReadDestinoDto>();
        CreateMap<UpdateDestinoDto, Destino>();
    }
}
