namespace FIAP.Hackathon.Domain.Enums
{
    public class NotificacaoEnum
    {
        public enum Status 
        {
            AguardandoEnvio = 1,
            Enviado,
            FalhaAoEnviar,
            Cancelado
        }

        public enum TipoNotificacao
        {
            LembreteAgendamento = 1
        }
    }
}
