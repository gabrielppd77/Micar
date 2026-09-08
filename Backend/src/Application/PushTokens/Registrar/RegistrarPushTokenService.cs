using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.PushTokens;
using Domain.PushTokens;

namespace Application.PushTokens.Registrar;

public class RegistrarPushTokenService
{
    private readonly IPushTokenRepository _pushTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public RegistrarPushTokenService(
        IPushTokenRepository pushTokenRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _pushTokenRepository = pushTokenRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(RegistrarPushTokenRequest request, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var tokenExistente = await _pushTokenRepository.GetByTokenAsync(request.Token, ct);

        if (tokenExistente is not null)
            _pushTokenRepository.Remove(tokenExistente);

        var pushToken = new PushToken(usuarioId, request.Token, request.Plataforma);
        await _pushTokenRepository.AddAsync(pushToken, ct);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
