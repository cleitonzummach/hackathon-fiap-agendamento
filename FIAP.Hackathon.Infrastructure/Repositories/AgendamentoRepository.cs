using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Repositories;
using FIAP.Hackathon.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Hackathon.Infrastructure.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly HackathonDBContext _context;
        
        public AgendamentoRepository(HackathonDBContext context) 
        {
            _context = context;
        }

        public Agendamento? RetornarPorId(Guid agendamentoId)
        {
            return _context.Agendamento
                .Include(x => x.MedicoAgenda)
                .FirstOrDefault(x => x.AgendamentoId == agendamentoId);
        }

        public IEnumerable<Agendamento> RetornarPorPacienteId(Guid pacienteId)
        {
            var query = _context.Agendamento
                .Include(x => x.MedicoAgenda)
                .Where(x => x.PacienteId == pacienteId && x.Situacao != Domain.Enums.AgendamentoEnum.Status.Cancelado);
            return query.ToList();
        }

        public bool Criar(Agendamento agendamento)
        {
            try
            {
                _context.Agendamento.Add(agendamento);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Editar()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
