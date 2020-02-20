﻿using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Client.Application.ViewModels.Authentication
{
    public interface IRegisterViewModel
    {
        bool ShowRegisterFailed { get; }
        RegisterDetails UserRegisterDetails { get; }

        Task RegisterUserAsync();
    }
}