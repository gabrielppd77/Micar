using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.PushTokens;
using Domain.Exceptions;

namespace Application.PushTokens.Remover;

public class RemoverPushTokenService
{
    private readonly IPushTokenRepository _pushTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public RemoverPushTokenService(
        IPushTokenRepository pushTokenRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _pushTokenRepository = pushTokenRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(string token, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var pushToken = await _pushTokenRepository.GetByTokenAsync(token, ct);

        if (pushToken is null || pushToken.UsuarioId != usuarioId)
            throw new NotFoundException("Token não encontrado.");

        _pushTokenRepository.Remove(pushToken);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
