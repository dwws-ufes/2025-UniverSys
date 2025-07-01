namespace UniverSys.Application.Requests.Usuarios.Queries
{
    public class UsuarioObterPorIdQuery : IRequest<UsuarioDto>
    {
        public int Id { get; set; }
    }

    public class UsuarioObterPorIdQueryHandler : IRequestHandler<UsuarioObterPorIdQuery, UsuarioDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioObterPorIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Handle(UsuarioObterPorIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Usuarios
                .ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return dto;
        }
    }
}
