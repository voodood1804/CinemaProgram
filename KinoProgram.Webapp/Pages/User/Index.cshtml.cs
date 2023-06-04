using KinoProgram.Application.Infrasturcture.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KinoProgram.Webapp.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserRepository _users;

        public IndexModel(UserRepository users)
        {
            _users = users;
        }

        public IEnumerable<Application.models.User> Users =>
            _users.Set
                .OrderBy(u => u.Usertype).ThenBy(u => u.Username);
        public void OnGet()
        {

        }
    }
}
