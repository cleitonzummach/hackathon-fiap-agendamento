using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.Hackathon.Application.Requests
{
    public class CriarAgendaRequest
    {
        public Guid MedicoId { get; set; }
        public Guid EspecialidadeId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
