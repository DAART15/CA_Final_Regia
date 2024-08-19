using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Final_Regia.Domain.Models
{
    internal class Picture
    {
        public byte[]? FileData { get; set; }
        public Person Person { get; set; }
        public Person PersonalId { get; set; }
    }
}
