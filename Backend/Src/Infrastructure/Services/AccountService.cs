namespace Infrastructure.Services;

public class AccountService(
    IUserRepository userRepository,
    IRefreshSessionRepository refreshSessionRepository)
    : IAccountService 
{
    
}