using Timer = System.Windows.Forms.Timer;

namespace JogoDaCobrinha
{
    // classe principal da janela do jogo
    public partial class Form1 : Form
    {
        // timer responsável por atualizar o jogo - funciona como um loop
        Timer temporizadorJogo = new Timer();

        // cria objeto da cobrinha
        Cobrinha cobrinha = new Cobrinha();

        // var que armazena posição atual da comida
        Point posicaoDaComida;

        // gera numeros aleatórios para criar a comida em posições diferentes
        Random geradorAleatorio = new Random();

        // tamanhos do cenário
        int tamanhoDoBloco = 20;
        int quantidadeDeColunas = 30;
        int quantidadeDeLinhas = 22;

        // pontuação - quantidade de comidas
        int pontuacao = 0;

        // construtor da janela do jogo - executa quando inicia

        public Form1()
        {
            // inicializa os componentes do windows forms
            InitializeComponent();

            // configura a tela do jogo
            configurarJanela();

            // criar primeira comida
            criarComida();

            // tempo entre cada atualização do jogo
            temporizadorJogo.Interval = 150;

            // evento executado a cada "tick do timer, 120 ms"
            temporizadorJogo.Tick += atualizarJogo;

            // inicia o timer
            temporizadorJogo.Start();

            // evento responsável por desenhar na tela
            this.Paint += desenharJogo;

            // evento responsável por detectar teclas
            this.KeyDown += detectarTecla;
        }

        private void configurarJanela()
        {
            this.Text = "Jogo da Cobrinha";
            this.Width = 650;
            this.Height = 550;

            // centraliza a janela
            this.StartPosition = FormStartPosition.CenterScreen;

            // cor de fundo da janela
            this.BackColor = Color.Black;

            // evita piscadas durante a renderização
            this.DoubleBuffered = true;

            // permite as capturas das teclas do teclado
            this.KeyPreview = true;
        }

        // sempre chamado pelo Timer / sender = quem disparou o evento = Timer / e = informações extras do evento
        private void atualizarJogo(object sender, EventArgs e)
        {
            // prox posicao da cabeca
            Point novaPosicaoDaCabeca = cobrinha.calcularCabeca();

            // verificar colisao com parede
            bool bateuParede = novaPosicaoDaCabeca.X < 0 || novaPosicaoDaCabeca.X >= quantidadeDeColunas || 
                               novaPosicaoDaCabeca.Y < 0 || novaPosicaoDaCabeca.Y >= quantidadeDeLinhas;

            // verifica colisao com o corpo
            bool bateuCorpo = cobrinha.bateuNoCorpo(novaPosicaoDaCabeca);

            if (bateuParede || bateuCorpo)
            {
                // finaliza o jogo
                finalizarJogo();

                // interrompe o método
                return;
            }

            // verifica se a cobrinha comeu a comida
            bool comeuComida = novaPosicaoDaCabeca == posicaoDaComida;

            // move a cobra
            cobrinha.mover(novaPosicaoDaCabeca, comeuComida);

            // pontuar
            if (comeuComida )
            {
                pontuacao++;

                // criar nova comida
                criarComida();
            }

            // solicita redesenho da tela
            this.Invalidate();
        }

        // desenhar tudo na tela
        private void desenharJogo(object sender, PaintEventArgs e)
        {
            Graphics tela = e.Graphics;

            // desenha os elementos do jogo
            desenharPontacao(tela);
            desenharCobrinha(tela);
            desenharComida(tela);
        }

        private void desenharPontacao(Graphics tela)
        {
            tela.DrawString(

                $"Pontuação: {pontuacao}",
                new Font("Arial", 16, FontStyle.Bold),
                Brushes.White,
                10,
                10

            );
        }

        private void desenharCobrinha(Graphics tela)
        {
            // percorre todas as partes do corpo
            foreach(Point parte in cobrinha.partesDoCorpo)
            {
                // desenha retangulo
                tela.FillRectangle(

                    Brushes.LimeGreen,
                    parte.X * tamanhoDoBloco,
                    parte.Y * tamanhoDoBloco + 50,

                    // largura e altura do bloco
                    tamanhoDoBloco,
                    tamanhoDoBloco
        
                );
            }
        }

        private void desenharComida(Graphics tela)
        {
            // desenhar circulo vermelho
            tela.FillEllipse(

                Brushes.Red,
                posicaoDaComida.X * tamanhoDoBloco,
                posicaoDaComida.Y * tamanhoDoBloco + 50,

                // largura e altura do bloco
                tamanhoDoBloco,
                tamanhoDoBloco

            );
        }

        // detectar teclas pressionadas
        private void detectarTecla(object sender, KeyEventArgs e)
        {
            // seta pra cima
            if (e.KeyCode == Keys.Up)
            {
                cobrinha.direcao(0, -1);
            }

            // seta pra baixo
            else if (e.KeyCode == Keys.Down)
            {
                cobrinha.direcao(0, 1);
            }

            //seta pra esquerda
            else if (e.KeyCode == Keys.Left)
            {
                cobrinha.direcao(-1, 0);
            }

            //seta pra direita
            else if (e.KeyCode == Keys.Right)
            {
                cobrinha.direcao(1, 0);
            }
        }

        // criar comida em posição aleatória
        private void criarComida()
        {
            // gerar posições aleatórias
            int posicaoX = geradorAleatorio.Next(0, quantidadeDeColunas);
            int posicaoY = geradorAleatorio.Next(0, quantidadeDeLinhas);

            // define a posição da comida
            posicaoDaComida = new Point(posicaoX, posicaoY);
        }

        private void finalizarJogo()
        {
            temporizadorJogo.Stop();

            // mostra msg final
            MessageBox.Show(
            $"Fim de jogo!\nPontuação final: {pontuacao}"    
            );

            // reinicia o jogo
            reiniciarJogo();
        }

        private void reiniciarJogo()
        {
            cobrinha.reiniciar();
            pontuacao = 0;
            criarComida();
            temporizadorJogo.Start();
        }
    }
}
