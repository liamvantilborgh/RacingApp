using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.Core.DTO_S
{
    public class PilotsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
#nullable enable
        public string? NickName { get; set; }
#nullable disable
        public string LicenseNumber { get; set; }
#nullable enable
        public string? PhotoRelativePath { get; set; }
#nullable disable
        public char Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? Length { get; set; }
        public double? Weight { get; set; }
    }
}
