using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculadoraWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNumero_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int num = int.Parse(button.Name.Replace("btn", ""));

            if (num == 0)
            {
                if (tbOperacion.Text == "Error") tbOperacion.Text = "0";
                if (tbOperacion.Text != "0") tbOperacion.Text += num.ToString();
            }
            else
            {
                if (tbOperacion.Text == "0" || tbOperacion.Text == "Error") tbOperacion.Text = num.ToString();
                else tbOperacion.Text += num.ToString();
            }
        }

        private void btnOperadores_Click(object sender, RoutedEventArgs e)
        {
            String name = ((Button)sender).Name;

            if (name == "btnMas")
            {
                EscribirOperador("+");
            }
            else if (name == "btnMenos")
            {
                EscribirOperador("–");
            }
            else if (name == "btnMult")
            {
                EscribirOperador("×");
            }
            else if (name == "btnDivis")
            {
                EscribirOperador("÷");
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (tbOperacion.Text != "0")
            {
                if (tbOperacion.Text.Length == 1) tbOperacion.Text = "0";
                else tbOperacion.Text = tbOperacion.Text.Substring(0, tbOperacion.Text.Length - 1);
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbOperacion.Text = "0";
        }

        private void btnResultado_Click(object sender, RoutedEventArgs e)
        {
            Int32 result = 0;
            Int32 tryP = 0;
            String opr = "";
            String[] operacion = new String[1];
            String[] operacionFinal = new String[0];

            for (int i = 0; i < tbOperacion.Text.Length; i++)
            {
                String subs = tbOperacion.Text.Substring(i, 1);
                if (int.TryParse(subs, out tryP))
                {
                    operacion[operacion.Length - 1] += tryP;
                }
                else
                {
                    Array.Resize(ref operacion, operacion.Length + 1);
                    operacion[operacion.Length - 1] = subs;
                    Array.Resize(ref operacion, operacion.Length + 1);
                }
            }

            try
            {
                for (int i = 0; i < operacion.Length; i++)
                {
                    if (operacion[i] == "×")
                    {
                        operacionFinal[operacionFinal.Length - 1] = (int.Parse(operacionFinal[operacionFinal.Length - 1]) * int.Parse(operacion[i + 1])).ToString();
                        i++;
                    }
                    else if (operacion[i] == "÷")
                    {
                        operacionFinal[operacionFinal.Length - 1] = (int.Parse(operacionFinal[operacionFinal.Length - 1]) / int.Parse(operacion[i + 1])).ToString();
                        i++;
                    }
                    else
                    {
                        Array.Resize(ref operacionFinal, operacionFinal.Length + 1);
                        operacionFinal[operacionFinal.Length - 1] = operacion[i];
                    }
                }
            }
            catch
            {
                tbOperacion.Text = "Error";
                return;
            }

            for (int i = 0; i < operacionFinal.Length; i++)
            {
                if (i == 0) result = int.Parse(operacionFinal[i]);
                else if (int.TryParse(operacionFinal[i], out tryP))
                {
                    switch (opr)
                    {
                        case "+":
                            result += int.Parse(operacionFinal[i]);
                            break;
                        case "–":
                            result -= int.Parse(operacionFinal[i]);
                            break;
                        default: break;
                    }
                }
                else
                {
                    opr = operacionFinal[i];
                }
            }

            tbOperacion.Text = result.ToString();
        }

        private void EscribirOperador(string opr)
        {
            String ultCaracter = tbOperacion.Text.Substring(tbOperacion.Text.Length - 1, 1);
            Int32 res = 0;
            if (int.TryParse(ultCaracter, out res))
            {
                if (tbOperacion.Text != "0") tbOperacion.Text += opr;
            }
            else
            {
                tbOperacion.Text = tbOperacion.Text.Substring(0, tbOperacion.Text.Length - 1) + opr;
            }
        }
    }
}