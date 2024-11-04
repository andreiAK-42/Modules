using ExcelDataReader;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Border = System.Windows.Controls.Border;

namespace Sorting
{
    /// <summary>
    /// Логика взаимодействия для ReadFromExcelWindow.xaml
    /// </summary>
    public partial class ReadFromExcelWindow : System.Windows.Window
    {
        public int[] MassiveInteger;
        public double[] MassiveDouble;
        public byte SortType;
        public ReadFromExcelWindow()
        {
            InitializeComponent();
        }
        private void MouseDown_SelectFile(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Документы Excel (*.xls)|*.xls|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                this.tbFilePath.Text = openFileDialog.FileName;
            }
        }

        private void MouseDown_CreateMassive(object sender, MouseButtonEventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (rbInteger.IsChecked == true)
            {
                    ReadNumbersFromSheet(tbFilePath.Text, int.Parse(tbSheetName.Text));
                    SortType = 0;
            }
            else
                {
                    
                    MassiveDouble = ReadNumbersFromSheet(tbFilePath.Text, int.Parse(tbSheetName.Text));
                    SortType = 1;
                }

                this.DialogResult = true;
                this.Close();

        }

        public static double[] ReadNumbersFromSheet(string filePath, int sheetIndex)
        {
            // Открываем файл Excel в потоке
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Создаем объект ExcelDataReader
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {

                    // Получаем первый лист
                    var worksheet = reader.AsDataSet().Tables[sheetIndex];

                    // Создаем массив для хранения чисел
                    double[] numbers = new double[worksheet.Rows.Count * worksheet.Columns.Count];

                    // Считываем числа в массив
                    int index = 0;
                    for (int row = 0; row < worksheet.Rows.Count; row++)
                    {
                        for (int col = 0; col < worksheet.Columns.Count; col++)
                        {
                            // Проверяем, является ли значение ячейки целым числом
                            if (double.TryParse(worksheet.Rows[row][col].ToString(), out double value))
                            {
                                numbers[index] = value;
                                index++;
                            }
                        }
                    }

                    return numbers;
                }
            }
        }

            #region Визуал
        public void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.EnterBrush;
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = VisualManager.LeaveBrush;
        }
        #endregion
    }
}
