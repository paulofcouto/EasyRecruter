using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Easy.Core.Entities
{
    public class Candidato : BaseEntity
    {
        public Candidato(string urlPublica, string emailUsuario, string nome = "", string cargo = "")
        {
            UrlPublica = urlPublica;
            EmailUsuario = emailUsuario;
            Nome = nome;
            Cargo = cargo;
        }

        public Candidato(){}

        public string UrlPublica { get; private set; }
        public string EmailUsuario { get; private set; }
        public string Nome { get; private set; }
        public string Cargo { get; private set; }
    }
}
