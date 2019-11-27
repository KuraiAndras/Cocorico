﻿using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class LoginResult
    {
        public ICollection<string> Claims { get; set; } = new List<string>();
    }
}
