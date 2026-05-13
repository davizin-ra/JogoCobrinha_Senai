namespace JogoDaCobrinha
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] // necessário para os componentes gráficos do windows forms funcionarem corretamente
        static void Main()
        {
            // ativando visual moderno do windows
            Application.EnableVisualStyles();

            //  conf para compatibilidade de renderização de texto - false = mais moderno
            Application.SetCompatibleTextRenderingDefault(false);

            // inicia a aplicação - abre a janela Form1, enquanto estiver aberto o programa continuará executando
            Application.Run(new Form1());
        }
    }
}