using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class formMain : Form
    {
        public enum SymbolType
        {
            Number,
            Operator,
            DecimalPoint,
            PlusMminusSign,
            Backspace,
            ClearAll,
            ClearEntry,
            Undefined
        }

        public struct btnStruct
        {
            public char Content;
            public SymbolType Type;
            public bool IsBold;
            public btnStruct(char c, SymbolType t = SymbolType.Undefined, bool b = false)
            {
                this.Content = c;
                this.Type = t;
                this.IsBold = b;
            }
        }

        private btnStruct[,] buttons =
        {
            { new btnStruct('%'), new btnStruct('\u0152',SymbolType.ClearEntry), new btnStruct('C',SymbolType.ClearAll), new btnStruct('\u232B',SymbolType.Backspace) },
            { new btnStruct('\u215F'), new btnStruct('\u00B2'), new btnStruct('\u221A'), new btnStruct('\u00F7') },
            { new btnStruct('7',SymbolType.Number, true), new btnStruct('8',SymbolType.Number, true), new btnStruct('9',SymbolType.Number, true), new btnStruct('\u00D7',SymbolType.Operator) },
            { new btnStruct('4',SymbolType.Number, true), new btnStruct('5',SymbolType.Number, true), new btnStruct('6',SymbolType.Number, true), new btnStruct('-',SymbolType.Operator) },
            { new btnStruct('1',SymbolType.Number, true), new btnStruct('2',SymbolType.Number, true), new btnStruct('3',SymbolType.Number, true), new btnStruct('+',SymbolType.Operator) },
            { new btnStruct('\u00B1',SymbolType.PlusMminusSign), new btnStruct('0',SymbolType.Number, true), new btnStruct(',',SymbolType.DecimalPoint), new btnStruct('=',SymbolType.Operator) },
        };
        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            MakeButtons(buttons.GetLength(0),buttons.GetLength(1));
        }

        private void MakeButtons(int rows, int cols)
        {
            int btnWidth = 80;
            int btnHeight = 60;
            int posX = 0;
            int posY = 116;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button myButton = new Button();
                    FontStyle fs = buttons[i, j].IsBold ? FontStyle.Bold : FontStyle.Regular;
                    myButton.Text = buttons[i,j].Content.ToString();
                    myButton.Font = new Font("Segoe UI", 16,fs);
                    myButton.BackColor = buttons[i, j].IsBold ? Color.White : Color.Transparent;
                    myButton.Width = btnWidth;
                    myButton.Height = btnHeight;
                    myButton.Top = posY;
                    myButton.Left = posX;
                    myButton.Tag = buttons[i, j];
                    myButton.Click += Button_Click;
                    this.Controls.Add(myButton);
                    posX += myButton.Width;

                }
                posX = 0;
                posY += btnHeight;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            btnStruct clickedButtonStruct = (btnStruct)clickedButton.Tag;

            switch (clickedButtonStruct.Type)   
            {
                case SymbolType.Number:
                    if (lblResult.Text == "0") lblResult.Text = "";
                    lblResult.Text += clickedButton.Text;
                    break;
                case SymbolType.Operator:
                    break;
                case SymbolType.DecimalPoint:
                    if (lblResult.Text.IndexOf(",") == -1)
                        lblResult.Text += clickedButton.Text;
                    break;
                case SymbolType.PlusMminusSign:
                    if (lblResult.Text != "0")
                        if(lblResult.Text.IndexOf("-") == -1)
                            lblResult.Text = "-" + lblResult.Text;
                        else
                            lblResult.Text = lblResult.Text.Substring(1);
                    break;
                case SymbolType.Backspace:
                    lblResult.Text = lblResult.Text.Substring(0,lblResult.Text.Length - 1);
                    if (lblResult.Text.Length == 0 || lblResult.Text == "-0" || lblResult.Text == "-")
                        lblResult.Text = "0";
                    break;
                case SymbolType cler
                case SymbolType.Undefined:
                    break;
                default:
                    break;
            }
        }

        private void lblResult_TextChanged(object sender, EventArgs e)
        {
            if(lblResult.Text.Length > 16) lblResult.Text = lblResult.Text.Substring(0, 16);
            if (lblResult.Text.Length>11)
            {
                float delta = lblResult.Text.Length - 11;
                lblResult.Font = new Font("Segoe UI", 36 -delta*(float)2.8, FontStyle.Regular);
            }
            else
            {
                lblResult.Font = new Font("Segoe UI", 36 , FontStyle.Bold);
            }
        }
    }
}
