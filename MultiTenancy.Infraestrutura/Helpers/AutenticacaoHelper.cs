using System.Web;

namespace MultiTenancy.Infraestrutura.Helpers
{
    public static class AutenticacaoHelper
    {
        public static void AtualizarUsuarioAutenticado(string nomeUsuarioAutenticado)
        {
            HttpContext.Current.Session[Constantes.Ambiente.USUARIO_AUTENTICADO] = nomeUsuarioAutenticado;
        }

        public static string RecuperarUsuarioAutenticado()
        {
            object usuario = HttpContext.Current.Session[Constantes.Ambiente.USUARIO_AUTENTICADO];

            return usuario != null ? usuario.ToString() : "Não Autenticado";
        }
    }
}
