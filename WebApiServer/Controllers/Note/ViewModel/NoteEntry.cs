using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Note.ViewModel
{
    public class NoteEntry
    {
        public string Name { get; set; }

        public Common.DTO.Location Location { get; set; }
    }
}
