using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStaff.Models
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
