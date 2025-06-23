using AutoMapper;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Create;
using CadastroDeClienteMuralis.Aplicacao.DTO.Request.Update;
using CadastroDeClienteMuralis.Aplicacao.DTO.Response;
using CadastroDeClienteMuralis.Dominio.Entidades;

namespace CadastroDeClienteMuralis.Aplicacao.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateClienteRequest, Cliente>();
            CreateMap<UpdateClienteRequest, Cliente>();
            CreateMap<CreateEnderecoRequest, Endereco>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore());

            CreateMap<UpdateEnderecoRequest, Endereco>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore());

            CreateMap<CreateContatoRequest, Contato>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore());

            CreateMap<UpdateContatoRequest, Contato>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore());

            CreateMap<Cliente, ClienteResponse>();
            CreateMap<Endereco, EnderecoResponse>();
            CreateMap<Contato, ContatoResponse>();
        }
    }
}
