﻿using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class UpdateProjectStaffDto
    {
        public string? UserId { get; set; }
        public StaffRoleType Role { get; set; }
    }
}
