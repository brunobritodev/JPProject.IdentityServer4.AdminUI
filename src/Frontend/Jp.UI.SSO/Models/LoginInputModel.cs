// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Jp.Infra.CrossCutting.Tools.StringUtils;
using System.ComponentModel.DataAnnotations;

namespace Jp.UI.SSO.Models
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public bool IsUsernameEmail()
        {
            // Return true if strIn is in valid e-mail format.
            return Username.IsEmail();
        }
    }
}