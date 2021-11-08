using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog_API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUsersRepo _usersrepo;
        private readonly ILogger _logger;
        public AccountController(ILogger<AccountController> logger, IUsersRepo usersrepo)
        {
            _logger = logger;
            _usersrepo = usersrepo;
        }

        
    }
}
