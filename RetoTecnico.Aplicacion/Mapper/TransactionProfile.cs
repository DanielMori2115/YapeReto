using AutoMapper;
using RetoTecnico.Aplicacion.Dto;

namespace RetoTecnico.Aplicacion.Mapper;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        SetConfigurations();
        MappingModels();
        MappingErrors();
    }

    private void SetConfigurations()
    {
        AllowNullCollections = true;
        AllowNullDestinationValues = true;
    }

    private void MappingModels()
    {
        CreateMap<TransactionDto, Dominio.Models.Transaction>().ReverseMap();
    }

    private void MappingErrors()
    {
    }
}
