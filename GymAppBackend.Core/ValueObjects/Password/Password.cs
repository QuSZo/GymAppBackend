﻿using GymAppBackend.Core.ValueObjects.Password.Exceptions;

namespace GymAppBackend.Core.ValueObjects.Password;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 200 or < 8)
        {
            throw new InvalidPasswordException();
        }

        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value?.Value;

    public override string ToString() => Value;
}