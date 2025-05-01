using FIAP.Hackathon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Hackathon.Infrastructure.Data.Context
{
    public class HackathonDBContext : DbContext
    {
        public HackathonDBContext(DbContextOptions<HackathonDBContext> options) : base(options)
        {
        }

        public DbSet<MedicoAgenda> MedicoAgenda { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicoAgenda>()
                .HasKey(m => m.MedicoAgendaId);

            modelBuilder.Entity<Agendamento>()
                .HasKey(m => m.AgendamentoId);

            modelBuilder.Entity<Agendamento>()
                .HasOne(m => m.MedicoAgenda)
                .WithMany(e => e.Agendamentos)
                .HasForeignKey(m => m.MedicoAgendaId);

            modelBuilder.Entity<Notificacao>()
                .HasKey(m => m.NotificacaoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
