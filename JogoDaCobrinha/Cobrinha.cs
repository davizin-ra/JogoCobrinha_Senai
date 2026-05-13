using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaCobrinha
{
    public class Cobrinha
    {
        // lista que armazena todas as partes do corpo da cobrinha
        // cada point representa uma posição no jogo ( coordenadas x e y )
        public List<Point> partesDoCorpo { get; private set; }


        // direção horizontal
        // 1 - direita // -1 - esquerda // 0 - sem movimento horizontal
        public int direcaoH {get; private set;}

        //direção vertical
        // 1 - baixo // -1 - cima // 0 - sem movimento vertical
        public int direcaoV {get; private set;}

        // construtor da classe
        public Cobrinha()
        {
            // cria a lista vazia
            partesDoCorpo = new List<Point>();

            // inicializar a cobrinha
            reiniciar();

        }

        // método responável por reiniciar o jogo
        public void reiniciar()
        {
            // limpa as partes do corpo
            partesDoCorpo.Clear();

            // adiciona a cabeça da cobrinha
            // define a posição - x=5 y=5
            partesDoCorpo.Add(new Point(5, 5));

            // h inicial pra direita
            direcaoH = 1;
            direcaoV = 0;
        }

        // retornar posição da cabeça - pos0 da lista
        public Point obterCabeca()
        {
            return partesDoCorpo[0];
        }

        // calcula próxima posição da cabeça
        public Point calcularCabeca()
        {
            Point cabecaAtual = obterCabeca();

            // retorna nova posição 
            return new Point(
                cabecaAtual.X + direcaoH,
                cabecaAtual.Y + direcaoV
                );
        }

        // mover a cobrinha
        public void mover(Point novaPosicaoDaCabeca, bool comeuComida)
        {
            // add a nova cabeça no inicio da lista
            partesDoCorpo.Insert(0, novaPosicaoDaCabeca);

            if (!comeuComida)
            {
                // remover ultima parte do corpo - criar efeito movimento
                partesDoCorpo.RemoveAt(partesDoCorpo.Count -1);
            }
            
            // se comeu, não remove o ultimo bloco - ela cresce
        }

        // alterar a direção da cobrinha
        public void direcao(int novaDirecaoH, int novaDirecaoV)
        {
            // verifica se o jogador não está tentando voltar pra trás
            bool tentVoltar = direcaoH + novaDirecaoH == 0 && direcaoV + novaDirecaoV == 0;

            // só muda direção se não estiver tentando voltar
            if (! tentVoltar)
            {
                direcaoH = novaDirecaoH;
                direcaoV = novaDirecaoV;
            }
        }

        // verifica se a cobrinha bateu no próprio corpo
        public bool bateuNoCorpo(Point novaPosicaoDaCabeca)
        {
            // retorna true se a posição já existir no List do corpo
            return partesDoCorpo.Contains(novaPosicaoDaCabeca);
        }
    }
}
