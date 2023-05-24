using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCalculadora
{
    public partial class MainPage : ContentPage
    {
        public double numeroUno = 0, numeroDos = 0, numeroRespuesta = 0;// 4(numeroUno) + 5(numeroDos) = 9(numeroRespuesta)
        int operador = 4; // 0=+, 1=-, 2=*, 3=/, 4=ninguna operacion
        bool hayPunto = false, unoDecimal = false, dosDecimales;
        private string numero;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Igualar_valores(String operando, int valor)
        {
            bool validaLabel = lblNumber.Text.GetType() != operador.GetType();//QUE TIPO DE DATOS ES
            bool validarSpn = primerNumero.GetType() != operador.GetType();

            if (numeroRespuesta != 0 || ((validaLabel || validarSpn) || (validaLabel && validarSpn)))
                unoDecimal = true;
            if (unoDecimal)
                numeroUno = double.Parse(lblNumber.Text);
            else
                numeroUno = double.Parse(lblNumber.Text);

            primerNumero.Text = numeroUno + " ";
            lblNumber.Text = "0";
            signoOperacion.Text = operando;
            operador = valor;
            hayPunto = false;
        }

        private bool Hallar_Lleno()
        {
            // Validacion si ya se eligio una operacion no se pueda poner otra mas
            bool estaLleno = false;
            if (primerNumero.Text == "" && signoOperacion.Text == "")
                estaLleno = true;
            return estaLleno;
        }

        private void Ingresar_numero(String numero)
        {
            //metodo para ingresar numero si es 0 o es punto no se deja ingresar 
            if (lblNumber.Text == "0" && numero != ".")
                lblNumber.Text = numero;
            else
                lblNumber.Text += numero;
        }

        private void Btn_AC(object sender, EventArgs e)
        {
            //metodo que resetea todo
            numeroUno = 0; numeroDos = 0; numeroRespuesta = 0; hayPunto = false;
            primerNumero.Text = ""; signoOperacion.Text = ""; segundoNumero.Text = ""; lblNumber.Text = "0";
        }


        private void Btn_operacion(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operador = button.Text;

            int operadorIdex = 4;

            switch (operador)
            {
                case "+":
                    operadorIdex = 0;
                    break;
                case "-":
                    operadorIdex = 1;
                    break;
                case "x":
                    operadorIdex = 2;
                    break;
                case "÷":
                    operadorIdex = 3;
                    break;

            }

            Igualar_valores(operador, operadorIdex);

            if (!Hallar_Lleno())
                segundoNumero.Text = "";
        }

        private void Click_C(Object sender, EventArgs e)
        {
            if (lblNumber.Text.EndsWith("."))
            {
                hayPunto = false;
            }
            if (lblNumber.Text != "0" && lblNumber.Text != "0.")
            {
                if (double.Parse(lblNumber.Text) > 10)
                {
                    lblNumber.Text = lblNumber.Text.Substring(0, lblNumber.Text.Length - 1);
                }
                else
                {
                    lblNumber.Text = "0";
                }
            }
            if (lblNumber.Text.EndsWith("."))
            {
                lblNumber.Text = lblNumber.Text.Substring(0, lblNumber.Text.Length - 1);
            }
        }

        private void Click_zero(object sender, EventArgs e)
        {
            if (lblNumber.Text != "0")
            {
                Ingresar_numero("0");
            }
        }

        private void btnMensaje_Clicked(object sender, EventArgs e)
        {

        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            numero = button.Text;
            Ingresar_numero(numero);
        }


        private void Btn_igual(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(primerNumero.Text) && !string.IsNullOrEmpty(signoOperacion.Text))
            {
                segundoNumero.Text = " " + lblNumber.Text;
                if (dosDecimales)
                    numeroDos = double.Parse(segundoNumero.Text);
                else
                    numeroDos = int.Parse(segundoNumero.Text);

                switch (operador)
                {
                    case 0:
                        numeroRespuesta = numeroUno + numeroDos;
                        lblNumber.Text = numeroRespuesta + "";
                        break;
                    case 1:
                        numeroRespuesta = numeroUno - numeroDos;
                        lblNumber.Text = numeroRespuesta + "";
                        break;
                    case 2:
                        numeroRespuesta = numeroUno * numeroDos;
                        lblNumber.Text = numeroRespuesta + "";
                        break;
                    case 3:
                        if (numeroDos == 0)
                        {
                            numeroDos = 1;
                        }
                        numeroRespuesta = numeroUno / numeroDos;
                        lblNumber.Text = numeroRespuesta + "";
                        break;

                }
                numeroUno = 0; numeroDos = 0; numeroRespuesta = 0;
                operador = 4; unoDecimal = false; dosDecimales = false;
            }
        }

        private void Click_punto(object sender, EventArgs e)
        {
            if (!hayPunto)
            {
                Ingresar_numero(".");
                hayPunto = true;
            }
            if (operador == 4)
                unoDecimal = true;
            else
                dosDecimales = true;

        }

        private async void btnMesaje(Object sender, EventArgs e)
        {
            await DisplayAlert("Hecho por:", "Stalyn Palma", "Cerrar");
        }
    }
}
