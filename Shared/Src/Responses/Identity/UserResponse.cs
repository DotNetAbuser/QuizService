﻿namespace Shared.Responses.Identity;

public class UserResponse
{
    public string Id { get; set; }
    
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }

    public string Username { get; set; }
    
    public string Email { get; set; }
    public string Phone { get; set; }

    public bool IsActive { get; set; }
    
    public DateTime Created { get; set; }
}