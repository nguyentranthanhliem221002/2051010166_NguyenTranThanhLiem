using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace _2051010166_NguyenTranThanhLiem.ViewModels
{
    public class UserRoleViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
