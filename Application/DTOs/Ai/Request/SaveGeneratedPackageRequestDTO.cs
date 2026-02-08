using Fas7ny.Application.DTOs.Ai.Response;
using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Application.DTOs.Ai.Request
{
    public class SaveGeneratedPackageRequestDTO
    {


        [Required(ErrorMessage = "Package name is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Package name must be between 5 and 200 characters")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Generated package data is required")]
        public GeneratedPackageData PackageData { get; set; }
    }
}
