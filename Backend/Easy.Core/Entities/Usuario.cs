using Easy.Core.Enum;

namespace Easy.Core.Entities
{
    public class Usuario : BaseEntity
    {
        public Usuario(string email, string senhaHash)
        {
            Email = email;
            SenhaHash = senhaHash;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public TipoUsuario TipoUsuario { get; private set; }
    }
}
