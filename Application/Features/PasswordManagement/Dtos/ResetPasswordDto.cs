﻿using System.ComponentModel.DataAnnotations;

namespace Application.Features.PasswordManagement.Dtos
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
