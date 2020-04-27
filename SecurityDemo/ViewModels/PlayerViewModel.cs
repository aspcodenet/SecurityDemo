using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SecurityDemo.ViewModels
{
    public class PlayerListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PlayerViewModel
    {
        public int DbNumber { get; set; }


        [Required] public string Name { get; set; }

        public int JerseyNumber { get; set; }

        [Range(1, 100)] public int Age { get; set; }


        [Required]
        public int TeamId { get; set; }
        public List<SelectListItem> AllTeams { get; set; } = new List<SelectListItem>();


        public int PositionEnumValue { get; set; }
        public List<SelectListItem> AllPositions { get; set; } = new List<SelectListItem>();


    }
}