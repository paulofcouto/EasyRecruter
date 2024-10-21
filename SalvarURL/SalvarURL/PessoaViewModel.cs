namespace SalvarURL
{
    public class PessoaViewModel
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Sobre { get; set; }
        public List<ExperienciaViewModel> Experiencias { get; set; }
        public List<FormacaoAcademicaViewModel> FormacaoAcademica { get; set; }
    }

    public class ExperienciaViewModel
    {
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public string Periodo { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
    }

    public class FormacaoAcademicaViewModel
    {
        public string Instituicao { get; set; }
        public string Curso { get; set; }
        public string Periodo { get; set; }
    }

}
