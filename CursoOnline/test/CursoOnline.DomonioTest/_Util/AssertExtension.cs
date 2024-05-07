using CursoOnline.Dominio._Base;

namespace CursoOnline.DomonioTest._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ExcecaoDeDominio exception, string message)
        {
            if (exception.MensagensDeErro.Contains(message))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem '{message}'");
        }
    }
}
