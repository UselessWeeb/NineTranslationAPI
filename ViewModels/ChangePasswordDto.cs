﻿namespace APINineTranslation.Controllers
{
    public class ChangePasswordDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}