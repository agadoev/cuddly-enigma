﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {

        [HttpGet]
        public IEnumerable<object> Get() {
            return new object[] {};
        }
    }
}
