using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Repositories;
using FIAP.Hackathon.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Hackathon.Infrastructure.Repositories
{
    public class MedicoAgendaRepository : IMedicoAgendaRepository
    {
        private readonly HackathonDBContext _context;
        
        public MedicoAgendaRepository(HackathonDBContext context) 
        {
            _context = context;
        }

        public bool Criar(MedicoAgenda medicoAgenda)
        {
            try
            {
                _context.MedicoAgenda.Add(medicoAgenda);
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

        public IEnumerable<MedicoAgenda> RetornarAgendaDisponivel(Guid? especialidadeId)
        {
            var query = _context.MedicoAgenda
                .Where(x => x.Situacao == Domain.Enums.MedicoAgendaEnum.Status.Disponivel);

            if (especialidadeId.HasValue)
                query = query.Where(x => x.EspecialidadeId == especialidadeId.Value);

            query = query.OrderBy(x => x.DataHoraInicio);
            
            return query.ToList();
        }

        public IEnumerable<MedicoAgenda> RetornarAgenda(Guid medicoId)
        {
            var query = _context.MedicoAgenda
                .Include(x => x.Agendamentos)
                .Where(x => x.MedicoId == medicoId && x.Situacao != Domain.Enums.MedicoAgendaEnum.Status.Excluido)
                .OrderBy(x => x.DataHoraInicio);
            return query.ToList();
        }

        public MedicoAgenda? RetornarPorId(Guid medicoAgendaId)
        {
            return _context.MedicoAgenda
                .FirstOrDefault(x => x.MedicoAgendaId == medicoAgendaId);
        }
    }
}
