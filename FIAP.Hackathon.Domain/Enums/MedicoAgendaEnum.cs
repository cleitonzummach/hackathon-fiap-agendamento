namespace FIAP.Hackathon.Domain.Enums
{
    public class MedicoAgendaEnum
    {
        public enum Status 
        {
            Disponivel = 1,
            Agendado = 2,
            Excluido
        }

        public static string RetornarDescricao(Status situacao) 
        {
            string descricao = string.Empty;
            
            switch (situacao)
            {
                case Status.Disponivel:
                    descricao = "Disponível";
                    break;
                case Status.Agendado:
                    descricao = "Agendado";
                    break;
                case Status.Excluido:
                    descricao = "Excluído";
                    break;
                default:
                    break;
            }

            return descricao;
        }
    }
}
