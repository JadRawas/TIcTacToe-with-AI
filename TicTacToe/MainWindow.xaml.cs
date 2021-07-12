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
using System.Xml.Linq;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int[,] array;
        public static bool turn = true;
        public static bool compstart = false;
        public static int compON = -1;

        public MainWindow()
        {
            InitializeComponent();
            newGame(0);
        }

        public void newGame(int type)
        {
            array = new int[3, 3];
            turn = true;
            this.b00.Content = "";
            this.b01.Content = "";
            this.b02.Content = "";
            this.b10.Content = "";
            this.b11.Content = "";
            this.b12.Content = "";
            this.b20.Content = "";
            this.b21.Content = "";
            this.b22.Content = "";
            compON = -1;
            if (type == 1)
            {
                this.newComputer.IsEnabled = false;
                this.newPlayer.IsEnabled = true;
                compON = 0;
                if (compstart)
                {
                    autoPicker();
                }
                compstart = !compstart;
            }
            else
            {
                this.newComputer.IsEnabled = true;
                this.newPlayer.IsEnabled = false;
            }
        }

        public bool checkWinner(int x, int y, String xo)
        {

            int nextGameType = compON + 1;
            //check horizentally
            if (array[0, y] == array[1, y] && array[0, y] == array[2, y])
            {
                MessageBox.Show(xo + " Won!");
                newGame(nextGameType);
                return true;
            }

            //check vertically
            if (array[x, 0] == array[x, 1] && array[x, 0] == array[x, 2])
            {
                MessageBox.Show(xo + " Won!");
                newGame(nextGameType);
                return true;
            }
            if (array[1, 1] != 0)
            {
                if (array[0, 0] == array[1, 1] && array[1, 1] == array[2, 2])
                {
                    MessageBox.Show(xo + " Won!");
                    newGame(nextGameType);
                    return true;
                }
                if (array[2, 0] == array[1, 1] && array[1, 1] == array[0, 2])
                {
                    MessageBox.Show(xo + " Won!");
                    newGame(nextGameType);
                    return true;
                }
            }
            //check if draw
            Boolean draw = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] == 0)
                    {
                        draw = false;
                        break;
                    }
                }
            }
            if (draw)
            {
                MessageBox.Show("Draw!");
                newGame(nextGameType);
                return true;
            }
            return false;
        }

        public void gridPicker(int x, int y, int compturn)
        {
            if (array[x, y] != 0)
                return;

            int num = 1;
            String xo = "X";

            if (!turn)
            {
                xo = "O";
                num = 2;
            }

            array[x, y] = num;
            turn = !turn;

            if (x == 0 && y == 0)
            {
                this.b00.Content = xo;
            }
            else if (x == 0 && y == 1)
            {
                this.b01.Content = xo;
            }
            else if (x == 0 && y == 2)
            {
                this.b02.Content = xo;
            }
            else if (x == 1 && y == 0)
            {
                this.b10.Content = xo;
            }
            else if (x == 1 && y == 1)
            {
                this.b11.Content = xo;
            }
            else if (x == 1 && y == 2)
            {
                this.b12.Content = xo;
            }
            else if (x == 2 && y == 0)
            {
                this.b20.Content = xo;
            }
            else if (x == 2 && y == 1)
            {
                this.b21.Content = xo;
            }
            else if (x == 2 && y == 2)
            {
                this.b22.Content = xo;
            }

            if (checkWinner(x, y, xo)) return;

            //check if computer turn if vs computer
            if (compON != -1)
            {
                if (compturn == 0)
                {
                    autoPicker();
                }
            }
        }

        private void click_0_0(object sender, RoutedEventArgs e)
        {
            gridPicker(0, 0, 0);
        }
        private void click_0_1(object sender, RoutedEventArgs e)
        {
            gridPicker(0, 1, 0);
        }
        private void click_0_2(object sender, RoutedEventArgs e)
        {
            gridPicker(0, 2, 0);
        }
        private void click_1_0(object sender, RoutedEventArgs e)
        {
            gridPicker(1, 0, 0);
        }
        private void click_1_1(object sender, RoutedEventArgs e)
        {
            gridPicker(1, 1, 0);
        }
        private void click_1_2(object sender, RoutedEventArgs e)
        {
            gridPicker(1, 2, 0);
        }
        private void click_2_0(object sender, RoutedEventArgs e)
        {
            gridPicker(2, 0, 0);
        }
        private void click_2_1(object sender, RoutedEventArgs e)
        {
            gridPicker(2, 1, 0);
        }
        private void click_2_2(object sender, RoutedEventArgs e)
        {
            gridPicker(2, 2, 0);
        }
        private void Player_Click(object sender, RoutedEventArgs e)
        {
            newGame(0);
        }
        private void newComputer_Click(object sender, RoutedEventArgs e)
        {
            newGame(1);
        }


        //vs Computer functions
        public void autoPicker()
        {

            /*int[,] arr2 = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] == 0)
                    {
                        gridPicker(i, j, 1);
                        return;
                    }
                }
            }*/
            int num = 1;
            if (!turn)
            {
                num = 2;
            }
            SmartAI x = new SmartAI(array, num);
            gridPicker(x.x, x.y, 1);
        }
        public void autoPickerHard()
        {

        }
    }
}
