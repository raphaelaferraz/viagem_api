using AutoMapper;
using viagem_api.Data.Dtos;
using viagem_api.Models;

namespace viagem_api.Profiles;

public class DepoimentoProfile : Profile
{
    public DepoimentoProfile() 
    {
        CreateMap<CreateDepoimentoDto, Depoimento>();
        CreateMap<Depoimento, ReadDepoimentoDto>();
        CreateMap<UpdateDepoimentoDto, Depoimento>();
    }
}
