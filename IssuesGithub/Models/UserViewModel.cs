using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssuesGithub.Models
{
    public class GroupedUserViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }

    public class UserViewModel
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}