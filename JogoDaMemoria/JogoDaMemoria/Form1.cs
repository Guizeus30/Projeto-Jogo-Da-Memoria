using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaMemoria
{
    public partial class Form1 : Form
    {
        // firstClicked torna o label clicado visivel
        Label firstClicked = null;

        // secondClicked torna o segundo label clicado visivel
        Label secondClicked = null;

        // Random é usado para espalhar os icones aleatoriamento pelos label
        Random random = new Random();

        // Cada uma dessas letras é um ícone da fonte Webdings,
        // e cada ícone aparece duas vezes nesta lista
        // para ter um par de cada
        List<string> icons = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z",
        "f", "f", "i", "i", "o", "o", "#", "#",
        "m", "m", "ç", "ç", "?", "?", "u", "u",
        "x", "x", "s", "s"

        };

        /// <summary>
        /// Atribua cada ícone da lista de ícones a um label aleatório
        /// </summary>
        private void AssignIconsToSquares()
        {

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }


        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        /// <summary>
        /// O evento Click de cada marcador é tratado por esse manipulador de eventos
        /// </summary>
        private void label_Click(object sender, EventArgs e)
        {
            // O cronômetro é ativado somente depois de dois
            // ícones foram mostrados para o player,
            // e ignora todos os cliques se o cronômetro estiver em execução
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Se o rótulo clicado estiver preto,
                // o player clicou em um ícone
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Se firstClicked for nulo,
                // este é o primeiro ícone no par em que o jogador clicou,
                // então configurei firstClicked no label que o player
                // clicou, altere sua cor para preto e retorne
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
               
                // Então esse deve ser o segundo ícone no qual o player clicou
                // Altera sua cor para preto
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Verifica se os ícones são iguais
                CheckForWinner();

                // Se o jogador clicou em dois ícones não correspondentes,
                // mantenha-os preto ate o timer acabar e redefina o firstClicked e secondClicked
                // para que o player possa clicar em outro ícone
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }


                // Se o jogador chegou até aqui, 
                // o jogador clicou em dois ícones diferentes,
                // então inicie o timer (que aguardará três quartos de
                // um segundo e depois ocultara os ícones)
                timer1.Start();

            }
        }
        /// <summary>
        /// Este timer é iniciado quando o jogador clica
        /// dois ícones que não correspondem,
        /// então conta três quartos de um segundo
        /// depois se desliga e oculta os dois ícones
        /// </summary>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Para a contagem
            timer1.Stop();

            // Esconde os ícones
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Redefine firstClicked e secondClicked
            // então na próxima vez que um label for clicado,
            // o programa sabe que é o primeiro clique
            firstClicked = null;
            secondClicked = null;

        }
        /// <summary>
        /// Está função é a que determina se o jogador achou os pares correspondentes
        /// </summary>
        private void CheckForWinner()
        {
            // Verifica todos os label no TableLayoutPanel,
            // verificando cada um para ver se o ícone corresponde
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Se o loop não retornou,
            // não encontrou quaisquer ícones sem correspondência
            // Isso significa que o usuário venceu. Mostrara uma mensagem e fechara o formulário
            MessageBox.Show("Você descobriu todos os parés! uh ah uh :)", "Parabéns");
            Close();
        }
    }
}

