using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using WPF_LAB_9.Commands;

namespace WPF_LAB_9
{
    /// <summary>
    /// Логика взаимодействия для EditPaymentWindow.xaml
    /// </summary>
    public partial class EditPaymentWindow : Window
    {
        public EditPaymentWindow()
        {
            InitializeComponent();
        }
        #region Properties
        public string PaymentName
        {
            get { return (string)GetValue(PaymentNameProperty); }
            set { SetValue(PaymentNameProperty, value); }
        }

        public static readonly DependencyProperty PaymentNameProperty =
        DependencyProperty.Register("PaymentName", typeof(string),
        typeof(EditPaymentWindow), new PropertyMetadata(default(string)));
        public DateTime PaymentDate
        {
            get { return (DateTime)GetValue(PaymentDateProperty); }
            set { SetValue(PaymentDateProperty, value); }
        }

        public static readonly DependencyProperty PaymentDateProperty =
        DependencyProperty.Register("PaymentDate", typeof(DateTime),
        typeof(EditPaymentWindow), new PropertyMetadata(default(DateTime)));
        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty =
        DependencyProperty.Register("Price", typeof(string),
        typeof(EditPaymentWindow), new PropertyMetadata(default(string)));
        public string ImagePass
        {
            get { return (string)GetValue(ImagePassProperty); }
            set { SetValue(ImagePassProperty, value); }
        }

        public static readonly DependencyProperty ImagePassProperty =
        DependencyProperty.Register("ImagePass", typeof(string),
        typeof(EditPaymentWindow), new PropertyMetadata(default(string)));
        #endregion

        private ICommand _selectedImageCommand;
        public ICommand SelectedImageCommand => _selectedImageCommand
            ?? new RelayCommand(OnSelectImageExecuted);

        public void OnSelectImageExecuted(object param)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ImagePass = dialog.FileName;
            }
        }
        private ICommand _okCommand;
        public ICommand OkCommand => _okCommand
          ?? new RelayCommand(OnOkExecuted);

        public void OnOkExecuted(object param)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
