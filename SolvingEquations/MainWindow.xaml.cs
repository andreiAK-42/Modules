using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Data;
using WpfMath.Controls;
using Brushes = System.Windows.Media.Brushes;

namespace SolvingEquations
{
    public partial class MainWindow : Window
    {
        double[][] Matrix;
        double[] RightPart;

        public MainWindow()
        {
            InitializeComponent();
            SetIamge();
        }

        private void Generate_Random_Massive(object sender, MouseButtonEventArgs e)
        {

        }

        private void MouseDown_ReadFromFile(object sender, MouseButtonEventArgs e)
        {
            ReadFromFile readFromFileWindow = new ReadFromFile();

            if (readFromFileWindow.ShowDialog() == true)
            {
                Matrix = readFromFileWindow.Matrix;
                RightPart = readFromFileWindow.RightPart;

                LoadMatrix(Matrix, RightPart);
            }
        }

        private void WindowClosing(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void TextMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        public void SetIamge()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource));
            Bitmap icon4 = (Bitmap)resources.GetObject("file.Image");

     
            imReadFromFile.Source = Imaging.CreateBitmapSourceFromHBitmap(icon4.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon4.Width, icon4.Height));

            Bitmap icon7 = (Bitmap)resources.GetObject("shutdown.Image");
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(icon7.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(icon7.Width, icon7.Height)); ;
            BtnCloseWindow.Background = ib;
        }

        public void LoadMatrix(double[][] matrix, double[] rightPart)
        {
            dgResult.Columns.Clear();
            dgResult.ItemsSource = null;

            if (matrix.Count() > 0)
            {
                var max = matrix.Max(c => c.Count());

                for (var counterI = 0; counterI < max + 1; ++counterI)
                {
                    if (counterI == max)
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Вспомогательные", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }
                    else
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Column: {0:00}", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }

                }

                double[][] resultMatrix = new double[matrix.Length][];
                
                for (var counterI = 0; counterI < resultMatrix.Length; ++counterI)
                {
                    resultMatrix[counterI] = new double[matrix.Length + 1];
                    matrix[counterI].CopyTo(resultMatrix[counterI], 0);
                    resultMatrix[counterI][resultMatrix[counterI].Length - 1] = rightPart[counterI];
                }
             
                dgResult.ItemsSource = resultMatrix;
            }
        }


        public void LoadMatrix(double[][] matrix, double[] resultPart, bool yes)
        {
            dgResult.Columns.Clear();
            dgResult.ItemsSource = null;

            if (matrix.Count() > 0)
            {
                var max = matrix.Max(c => c.Count());

                for (var counterI = 0; counterI < max + 1; ++counterI)
                {
                    if (counterI == max)
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Результат", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }
                    if (counterI == max - 1)
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Вспомогательные", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }
                    else
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Column: {0:00}", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }

                }

                double[][] resultMatrix = new double[matrix.Length][];

                for (var counterI = 0; counterI < resultMatrix.Length; ++counterI)
                {
                    resultMatrix[counterI] = new double[matrix.Length + 1];
                    matrix[counterI].CopyTo(resultMatrix[counterI], 0);
                    resultMatrix[counterI][resultMatrix[counterI].Length - 1] = resultPart[counterI];
                }

                dgResult.ItemsSource = resultMatrix;
            }
        }

        public void LoadMatrix(double[][] matrix, double[] rightPart, double[] resultPart)
        {
            dgResult.Columns.Clear();
            dgResult.ItemsSource = null;

            if (matrix.Count() > 0)
            {
                var max = matrix.Max(c => c.Count());

                for (var counterI = 0; counterI < max + 2; ++counterI)
                {

                    if (counterI == max + 1)
                    {
                        dgResult.Columns.Add(
                       new DataGridTextColumn
                       {
                           Header = string.Format("Результат", counterI),
                           Binding = new Binding(string.Format("[{0}]", counterI))
                       });
                    }
                    else if (counterI == max)
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Вспомогательные", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }
                    else
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Column: {0:00}", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }

                }

                double[][] resultMatrix = new double[matrix.Length][];

                for (var counterI = 0; counterI < resultMatrix.Length; ++counterI)
                {
                    resultMatrix[counterI] = new double[matrix.Length + 2];
                    matrix[counterI].CopyTo(resultMatrix[counterI], 0);
                    resultMatrix[counterI][resultMatrix[counterI].Length - 2] = rightPart[counterI];
                    resultMatrix[counterI][resultMatrix[counterI].Length - 1] = resultPart[counterI];
                }

                dgResult.ItemsSource = resultMatrix;
            }
        }

        private void btn_Solve(object sender, MouseButtonEventArgs e)
        {
            GausMethod gausMethod = new GausMethod((uint)Matrix[0].Length, (uint)Matrix[0].Length);

            double[][] copySquare = new double[Matrix.Length][];
            double[] copyRightPart = new double[RightPart.Length];
            double[]? arguments = null;

            for (var counterI = 0; counterI < Matrix.Length; ++counterI)
            {
                copySquare[counterI] = new double[Matrix.Length];
                Matrix[counterI].CopyTo(copySquare[counterI], 0);
            }

            RightPart.CopyTo(copyRightPart, 0);

            gausMethod.Matrix = copySquare;
            gausMethod.RightPart = copyRightPart;

            if (rbGauss.IsChecked == true)
            {
                gausMethod.SolveGauss();

                arguments = gausMethod.Answer;

                LoadMatrix(gausMethod.Matrix, gausMethod.RightPart, gausMethod.Answer);
            }
            else if (rbGaussJordan.IsChecked == true) 
            {
                gausMethod.SolveGaussJordan();

                arguments = new double[Matrix.Length];

                for (var counterI = 0; counterI < Matrix.Length; ++counterI)
                {
                    arguments[counterI] = gausMethod.Answer[counterI];
                }

               // LoadDGMatrix(gausMethod.Matrix, null);
            }
            else
            {
                double[][] cramerMatrix = new double[Matrix.Length][];

                for (var counterI = 0; counterI < Matrix.Length; ++counterI)
                {
                    cramerMatrix[counterI] = new double[Matrix.Length + 1];
                    Matrix[counterI].CopyTo(cramerMatrix[counterI], 0);
                    cramerMatrix[counterI][cramerMatrix[counterI].Length - 1] = RightPart[counterI];
                }

                double[] solution = Cramer.Solve(cramerMatrix);
                arguments = solution;

                LoadMatrix(cramerMatrix, solution, true);
            }

            formulasPanel.Children.Clear();

            for (var counterI = 0; counterI < Matrix.Length; ++counterI)
            {
                string yravnenie = "";
                for (int counterJ = 0; counterJ < Matrix.Length; ++counterJ)
                {
                    if (Matrix[counterI][counterJ] == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (Matrix[counterI][counterJ] > 0 && yravnenie != "")
                        {
                            yravnenie += $"+ {Matrix[counterI][counterJ]} * ({arguments[counterJ]}) ";
                        }
                        else
                        {
                            yravnenie += $"{Matrix[counterI][counterJ]} * ({arguments[counterJ]}) ";
                        }
                    }
                }


                yravnenie += "= " + RightPart[counterI];

                FormulaControl formulaControl = new FormulaControl();
                formulaControl.Formula = yravnenie;
                formulaControl.Foreground = Brushes.White;
                formulaControl.FontSize = 16;
                formulasPanel.Children.Add(formulaControl);
            }
        }

        private void rbGauss_Копировать_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
