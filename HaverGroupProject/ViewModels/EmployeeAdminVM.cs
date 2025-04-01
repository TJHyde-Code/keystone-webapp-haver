using HaverGroupProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HaverGroupProject.ViewModels
{
    /// <summary>
    /// Add back in any Restricted Properties and list of UserRoles
    /// </summary>
    [ModelMetadataType(typeof(EmployeeMetaData))]
    public class EmployeeAdminVM : EmployeeVM
    {
        public string Email { get; set; } = "";
        public bool Active { get; set; } = true;

        [Display(Name = "Roles")]
        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
