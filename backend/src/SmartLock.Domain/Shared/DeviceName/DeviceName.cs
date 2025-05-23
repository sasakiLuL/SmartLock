﻿using FluentValidation;
using SmartLock.Domain.Core;

namespace SmartLock.Domain.Shared.DeviceName;

public record DeviceName : ValueObject<string>
{
    public const int MaximumLenght = 100;

    public const string FormatString = @"^[a-zA-Z -!|]*";

    internal DeviceName(string value) : base(value) { }

    public static DeviceName CreateAndThrow(string value)
    {
        var deviceName = new DeviceName(value);

        new DeviceNameValidator().ValidateAndThrow(deviceName);

        return deviceName;
    }
}
