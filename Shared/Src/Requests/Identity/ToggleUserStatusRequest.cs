namespace Shared.Requests.Identity;

public record ToggleUserStatusRequest(
     [Required] string UserId,
     [Required] bool ActivateUser);