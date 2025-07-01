using UniverSys.Application.Requests.Usuarios.Commands;

namespace UniverSys.Application.Common.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        //CreateMap<UsuarioDto, UsuarioExcelDto>();

        CreateMap<UsuarioCriarCommand, Usuario>()
            .ForMember(x => x.Email, y => y.MapFrom(z => string.IsNullOrEmpty(z.Email) ? "" : z.Email.ToLower()))
            .ForMember(x => x.Login, y => y.MapFrom(z => string.IsNullOrEmpty(z.Login) ? "" : z.Login.ToLower()));

        CreateMap<UsuarioAlterarCommand, Usuario>()
            .ForMember(x => x.Email, y => y.MapFrom(z => string.IsNullOrEmpty(z.Email) ? "" : z.Email.ToLower()))
            .ForMember(x => x.Login, y => y.MapFrom(z => string.IsNullOrEmpty(z.Login) ? "" : z.Login.ToLower()))
            .ForMember(x => x.Senha, y => y.Ignore());

        CreateMap<Usuario, UsuarioDto>();
    }
}
