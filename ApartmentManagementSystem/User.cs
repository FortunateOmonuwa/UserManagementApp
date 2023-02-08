using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem
{
    public class User 
    {
       

      
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

      
    }
}
