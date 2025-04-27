// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Infrastructure.Constants.User;

public abstract class UserName
{
    public const string SuperAdmin = nameof(SuperAdmin);
    public const string Admin = nameof(Admin);
    public const string Demo = nameof(Demo);
    public const string Users = nameof(Users);
    public const string SuperAdminDefaultPassword = "Password@123!";
    public const string AdminDefaultPassword = "Password123!";
}