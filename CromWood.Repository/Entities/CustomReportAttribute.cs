using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CromWood.Data.Entities
{
    public class CustomReportAttribute: DBTable
    {
        public Guid Id { get; set; }
        public Guid CustomReportId { get; set; }
        public CustomReport CustomReport { get; set; }
        public int Order { get; set; }
        public string HeaderName { get; set; }
        public string DataName { get; set; }
        public string Alignment { get; set; }
        public int Width { get; set; }
        public string Append { get; set; }
        public string Prepend { get; set; }
    }
}
