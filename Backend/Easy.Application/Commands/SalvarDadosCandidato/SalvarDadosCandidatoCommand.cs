using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.SalvarDadosCandidato
{
    public class SalvarDadosCandidatoCommand : IRequest<Result>
    {
        public string UrlPublica { get; set; }
        public string Nome { get; set; }
        public string DescricaoProfissional { get; set; }
        public string Sobre { get; set; }
        public List<ExperienciaCommand> Experiencias { get; set; }
        public List<FormacaoCommand> Formacoes { get; set; }

        public class ExperienciaCommand
        {

            public string Empresa { get; set; }

            public string Local { get; set; }

            public List<CargoCommand> Cargos { get; set; }
        }

        public class CargoCommand
        {
            public string Titulo { get; set; }
            public string Periodo { get; set; }
            public string Descricao { get; set; }
        }

        public class FormacaoCommand
        {
            public string Instituicao { get; set; }
            public string Curso { get; set; }
            public string Periodo { get; set; }
        }
    }
}
