namespace FIAP.Hackathon.Domain.Enums
{
    public class AgendamentoEnum
    {
        public enum Status 
        {
            Agendado = 1,
            Cancelado
        }

        public static string RetornarDescricao(Status situacao) 
        {
            string descricao = string.Empty;
            
            switch (situacao)
            {
                case Status.Agendado:
                    descricao = "Agendado";
                    break;
                case Status.Cancelado:
                    descricao = "Cancelado";
                    break;
                default:
                    break;
            }

            return descricao;
        }
    }
}
