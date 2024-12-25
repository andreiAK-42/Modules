using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MLS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double[][] Matrix;
        public MainWindow()
        {
            InitializeComponent();
            SetIamge();
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

        private void MouseDown_ReadFromFile(object sender, MouseButtonEventArgs e)
        {
            ReadFromFile readFromFileWindow = new ReadFromFile();

            if (readFromFileWindow.ShowDialog() == true)
            {
                Matrix = readFromFileWindow.Matrix;

                LoadMatrix(Matrix);
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

        private void btn_Solve(object sender, MouseButtonEventArgs e)
        {
            Parser parser = new Parser();
            if (rbGauss.IsChecked == true)
            {
                parser.Linear(Matrix, this);
            }
            else
            {
                parser.Parabola(Matrix, this);
            }
        }

        public void LoadMatrix(double[][] matrix)
        {
            dgResult.Columns.Clear();
            dgResult.ItemsSource = null;

            if (matrix.Count() > 0)
            {
                for (var counterI = 0; counterI != 2; ++counterI)
                {
                    if (counterI == 1)
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("Y", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }
                    else
                    {
                        dgResult.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = string.Format("X", counterI),
                            Binding = new Binding(string.Format("[{0}]", counterI))
                        });
                    }

                }

                double[][] resultMatrix = new double[matrix[0].Length][];

                for (var counterI = 0; counterI != matrix[0].Length; ++counterI)
                {
                    resultMatrix[counterI] = new double[2];
                    resultMatrix[counterI][0] = Matrix[0][counterI];
                    resultMatrix[counterI][1] = matrix[1][counterI];
                }

                dgResult.ItemsSource = resultMatrix;
            }
        }
    }
}
