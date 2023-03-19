using System;
using System.Drawing;
using System.Windows.Forms;

namespace MasterMind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreaSpazioSelezione();
            CreaScacchiera();
        }

        private const string pallinoPieno = "•";

        // Righe da zero a 9.
        // Se si sbaglia al decimo tentativo GAME OVER
        private int rigaCorrente = 0;

        // Mentre il giocatore preme i tasti dobbiamo ricordare anche a 
        // che punto era arrivato
        private int colonnaCorrente = 0;

        // Colori premuti della riga corrente. Quando siamo a 4 dobbiamo
        // fare i controlli del caso e stampare il risultato.
        private string coloriPremuti = "";

        private const int numeroMassimoColori = 6;
        private Color[] coloriDisponibili = new Color[]
        {
            Color.White,
            Color.Yellow,
            Color.Green,
            Color.Aqua,
            Color.Magenta,
            Color.Red
        };


        private Label[] spazioSelezioneLabels = new Label[6];
        
        // Facciamo una matrice 10x4
        // Le abbiamo viste molto di sfuggita ma sono array bidimensionali
        private Label[,] scacchieraLabels = new Label[10,4];

        private void CreaSpazioSelezione()
        {
            // 2 righe per 3 colonne con impostati i colori
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int numeroColore = 3 * i + j;
                    
                    Label label = CreaLabel();
                    // Catturiamo l'evento. E' una sintassi un po' diversa dal codice generato
                    // automaticamente ma non cambia niente.
                    label.Click += labelSpazioSelezione_Click;
                    label.Top = 60 + i * 40;
                    label.Left = 30 + j * 40;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.ForeColor = coloriDisponibili[numeroColore];

                    spazioSelezioneLabels[numeroColore] = label;
                }
            }
        }


        private void CreaScacchiera()
        {
            // 10 righe per 4 colonne
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Label label = CreaLabel();
                    label.Top = 10 + i * 20;
                    label.Left = 200 + j * 30;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.Visible = false;

                    scacchieraLabels[i, j] = label;
                }
            }
        }


        private Label CreaLabel()
        {
            Label label = new Label();
            
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(33, 23);
            label.Name = "label1";
            label.Size = new System.Drawing.Size(35, 13);
            label.Text = pallinoPieno;
            this.Controls.Add(label);

            return label;
        }

        // Non creo un array di funzioni ma ne creo solo una
        // Per capire che label è stata cliccata è possibile guardare il sender
        // (chi ha spedito l'evento); ne abbiamo parlato molto sommariamente
        // in classe
        private void labelSpazioSelezione_Click(object sender, EventArgs e)
        {
            // Quando clicchiamo un colore dobbiamo
            //    Colorare il pallino corrispondente
            //    Aggiungere il colore premuto alla lista dei colori premuti
            //          per poi poter fare il test se abbiamo premuto quelli
            //          giusti. Per la lista dei colori premuti io ho usato
            //          una stringa ma è possibile usare un array
            //    Se siamo alla quarta pressione
            //          * dobbiamo fare i controlli del caso
            //          * stampare il risultato
            //          Se il giocatore non ha vinto
            //              * azzerare i colori premuti
            //              * passare alla riga successiva
            //              * tornare alla colonna zero

            // Le label vanno da 0 a 5 (numero massimo colori -1)
            // La label 0 corrisponde al colore 0 e così via
            int colorePremuto = CalcolaLabelCliccata(sender);

            scacchieraLabels[rigaCorrente, colonnaCorrente].ForeColor = coloriDisponibili[colorePremuto];
            scacchieraLabels[rigaCorrente, colonnaCorrente].Visible = true;

            coloriPremuti += colorePremuto.ToString();

            colonnaCorrente++;

            if (colonnaCorrente < 4)
                return;

            // Fare i controlli del caso
            // Se il giocatore ha vinto terminare

            // Qui il codice nel caso in cui il giocatore non abbia vinto
            coloriPremuti = "";
            rigaCorrente++;
            colonnaCorrente = 0;

            if (rigaCorrente == 10)
            {
                // GAME OVER!!!!
            }

        }

        private int CalcolaLabelCliccata(object label)
        {
            for (int i = 0; i < numeroMassimoColori; i++)
            {
                if (label == spazioSelezioneLabels[i])
                    return i;
            }

            // Non abbiamo trovato la label. Molto strano!!!
            return -1;
        }

    }
}
