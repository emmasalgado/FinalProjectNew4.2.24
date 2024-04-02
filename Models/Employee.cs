using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Core.Types;

namespace FinalProjectNew.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int YearsServed { get; set; }
    }
}
