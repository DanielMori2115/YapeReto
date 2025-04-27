using AutoMapper;
using RetoTecnico.Aplicacion.AntiFraud.Dto;
using RetoTecnico.Dominio.Models;

namespace RetoTecnico.Aplicacion.AntiFraud.Mapper;

public class AntiFraudProfile : Profile
{
    public AntiFraudProfile()
    {
        SetConfigurations();
        MappingRequests();
        MappingResponses();
        MappingErrors();
    }

    private void SetConfigurations()
    {
        AllowNullCollections = true;
        AllowNullDestinationValues = true;
    }

    private void MappingRequests()
    {
        CreateMap<TransactionDto, Transaction>().ReverseMap();
    }

    private void MappingResponses()
    {
        //CreateMap<UsuarioViewModel, Usuario>().ReverseMap();
    }

    private void MappingErrors()
    {
    }
}