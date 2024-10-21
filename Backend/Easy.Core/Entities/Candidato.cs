using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Easy.Core.Entities
{
    public class Candidato : BaseEntity
    {
        public Candidato(string idUsuario, string urlPublica, string nome, string cargo, string sobre, List<Experiencia> experiencias, List<Formacao> formacoesAcademicas)
        {
            IdUsuario = idUsuario;
            UrlPublica = urlPublica;
            Nome = nome;
            Cargo = cargo;
            Sobre = sobre;
            Experiencias = experiencias;
            Formacoes = formacoesAcademicas;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string IdUsuario { get; private set; }
        public string UrlPublica { get; private set; }
        public string Nome { get; private set; }
        public string Cargo { get; private set; }
        public string Sobre { get; private set; }
        public List<Experiencia> Experiencias { get; private set; }
        public List<Formacao> Formacoes { get; private set; }

        public class Experiencia
        {
            public Experiencia(string cargo, string empresa, DateTime dataInicial, DateTime dataFinal,
                               string local, string descricao)
            {
                Cargo = cargo;
                Empresa = empresa;
                DataInicial = dataInicial;
                DataFinal = dataFinal;
                Local = local;
                Descricao = descricao;
            }

            public string Cargo { get; private set; }
            public string Empresa { get; private set; }
            public DateTime DataInicial { get; private set; }
            public DateTime DataFinal { get; private set; }
            public string Local { get; private set; }
            public string Descricao { get; private set; }
        }

        public class Formacao
        {
            public Formacao(string instituicao, string curso, DateTime dataInicial, DateTime dataDeConclusao)
            {
                Instituicao = instituicao;
                Curso = curso;
                DataInicial = dataInicial;
                DataDeConclusao = dataDeConclusao;
            }

            public string Instituicao { get; private set; }
            public string Curso { get; private set; }
            public DateTime DataInicial { get; private set; }
            public DateTime DataDeConclusao { get; private set; }
        }
    }
}
