﻿namespace Shared.Requests.Identity;

public record RegisterRequest(
    [Required] string LastName,
    [Required] string FirstName,
    string MiddleName,
    [Required] string Username,
    [Required] string Password,
    [Required] string Email,
    [Required] string Phone);