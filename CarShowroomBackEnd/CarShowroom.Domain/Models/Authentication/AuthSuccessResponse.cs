using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Domain.Models.Authentication
{
    public class AuthSuccessResponse
    {
        public object Token { get; set; }
        public string Id { get; set; }
    }
}
