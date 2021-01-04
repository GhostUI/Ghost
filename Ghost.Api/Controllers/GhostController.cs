using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GhostController : ControllerBase
    {
        /// <summary>
        /// Get Test
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task GetTest()
        {

        }
    }
}
