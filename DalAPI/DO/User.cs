﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool InService;
    }
}
