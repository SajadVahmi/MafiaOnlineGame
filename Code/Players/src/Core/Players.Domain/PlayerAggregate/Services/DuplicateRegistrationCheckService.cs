using Players.Domain.PlayerAggregate.Data;

namespace Players.Domain.PlayerAggregate.Services
{
    public class DuplicateRegistrationCheckService:IDuplicateRegistrationCheckService
    {
        private readonly IPlayerRepository _playerRepository;

        public DuplicateRegistrationCheckService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<bool> CheckAsync(string userId, CancellationToken cancellationToken = default)
        {
            return _playerRepository.ExistAsync(player => player.UserId == userId); 
        }
    }
}
