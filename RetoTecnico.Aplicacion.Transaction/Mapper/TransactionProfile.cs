using AutoMapper;

using RetoTecnico.Aplicacion.Transaction.Dto;

namespace RetoTecnico.Aplicacion.Transaction.Mapper;

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
