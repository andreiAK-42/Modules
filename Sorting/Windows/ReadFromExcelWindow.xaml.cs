using ExcelDataReader;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows.Input;
using Border = System.Windows.Controls.Border;

namespace Sorting
{
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
            openFileDialog.Filter = "Документы Excel (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*";

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
                MassiveInteger = ReadIntegerNumbersFromSheet(tbFilePath.Text, int.Parse(tbSheetName.Text));
                SortType = 0;
            }
            else
            {
                MassiveDouble = ReadDoubleNumbersFromSheet(tbFilePath.Text, int.Parse(tbSheetName.Text));
                SortType = 1;
            }
            
            this.DialogResult = true;
            this.Close();
        }

        public static double[] ReadDoubleNumbersFromSheet(string filePath, int sheetIndex)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var worksheet = reader.AsDataSet().Tables[sheetIndex];

                    double[] numbers = new double[worksheet.Rows.Count * worksheet.Columns.Count];

                    int index = 0;
                    for (int row = 0; row < worksheet.Rows.Count; row++)
                    {
                        for (int col = 0; col < worksheet.Columns.Count; col++)
                        {
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

        public static int[] ReadIntegerNumbersFromSheet(string filePath, int sheetIndex)
        {
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var worksheet = reader.AsDataSet().Tables[sheetIndex];

                    int[] numbers = new int[worksheet.Rows.Count * worksheet.Columns.Count];

                    int index = 0;
                    for (int row = 0; row < worksheet.Rows.Count; row++)
                    {
                        for (int col = 0; col < worksheet.Columns.Count; col++)
                        {
                            if (int.TryParse(worksheet.Rows[row][col].ToString(), out int value))
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
