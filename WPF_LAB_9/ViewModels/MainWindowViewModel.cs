using System;
using System.Collections.ObjectModel;
using System.Linq;
using WPF_LAB_9.Businnes.Managers;
using WPF_LAB_9.Domain.Entities;
using System.Windows.Input;
using WPF_LAB_9.Commands;
using WPF_LAB_9.Businnes.Infrastructure;
using System.IO;
using System.Windows;

namespace WPF_LAB_9.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        ManagersFactory factory;
        PayerManager payerManager;
        PaymentManager paymentManager;
        #region Public properties
        public ObservableCollection<Payer> Payers { get; set; }
        
        public ObservableCollection<Payment> Payments { get; set; }
        public string Title { get => title; set => title = value; }
        #region Выбранный плательщик
        private Payer _selectedPayer;
        public Payer SelectedPayer
        {
            get => _selectedPayer;
            set
            {
                Set(ref _selectedPayer, value);
            }
        }
        #endregion
        #endregion
        private string title = "Payers Window";
        public MainWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            payerManager = factory.GetPayerManager();
            if (payerManager.Payers.Count() == 0)
                DbTestData.SetupData(payerManager);
            paymentManager = factory.GetPaymentManager();
            Payers = new ObservableCollection<Payer>(payerManager.Payers);
            Payments = new ObservableCollection<Payment>();
            if (Payers.Count > 0)
                OnGetPaymentExecuted(Payers[0].PayerId);
        }
        #region Commands
        #region Выбор плательщика в списке

        private ICommand _getPaymentsCommand;
        public ICommand GetPaymentsCommand =>
        _getPaymentsCommand
        ??= new RelayCommand(OnGetPaymentExecuted);
        private async void OnGetPaymentExecuted(object id)
        {
            Payments.Clear();
            var payments = await payerManager.GetPaymentsOfPayerAsync((int)id);
            foreach (var payment in payments)
                Payments.Add(payment);
        }
        #endregion
        #region Добавление платежа
        private ICommand _newPaymentCommand;
        public ICommand NewPaymentCommand =>
        _newPaymentCommand ??= new RelayCommand(OnNewPaymentExecuted);
        private void OnNewPaymentExecuted(object id)
        {
            var dialog = new EditPaymentWindow
            {
                PaymentDate = DateTime.Now
            };
            if (dialog.ShowDialog() != true) return;
            try
            {
                var payment = new Payment
                {
                PaymentName = dialog.PaymentName,
                PaymentDate = dialog.PaymentDate,
                Price = Convert.ToDecimal(dialog.Price)
                };
                var fileName = Path.GetFileName(dialog.ImagePass);
                payment.ImageFileName = fileName;
                payerManager.AddPaymentToPayer(payment, _selectedPayer.PayerId);
                var target = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                File.Copy(dialog.ImagePass, target);
                Payments.Add(payment);
            }
            catch { MessageBox.Show("Ошибка добавления данных") ; }
        }
        #endregion
        #region Выбранный платеж
        private Payment _selectedPayment;
        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                Set(ref _selectedPayment, value);
            }
        }
        #endregion
        #region Редактирование платежа
        private ICommand _editPaymentCommand;
        public ICommand EditPaymentCommand =>
        _editPaymentCommand ??=
        new RelayCommand(OnEditPaymentExecuted, EditPaymentCanExecute);
        // Проверка возможности редактирования
        private bool EditPaymentCanExecute(object p) =>
        _selectedPayment != null;
        private void OnEditPaymentExecuted(object id)
        {
            var dialog = new EditPaymentWindow
            {
                PaymentName = _selectedPayment.PaymentName,
                PaymentDate = _selectedPayment.PaymentDate,
                ImagePass = _selectedPayment.ImageFileName,
                Price = _selectedPayment.Price.ToString()
        };
            if (dialog.ShowDialog() != true) return;
            // Путь к папке Images
            var imageFolderPass = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            // Если выбрано новое изображение
            if (!_selectedPayment.ImageFileName.Equals(dialog.ImagePass))
            {
                // Удалить старое изображение
                File.Delete(Path.Combine(imageFolderPass, _selectedPayment.ImageFileName));
                // Получить имя нового файла изображения
                var newImage = Path.GetFileName(dialog.ImagePass);
                // Скопировать файл в папку Images
               
            File.Copy(dialog.ImagePass, Path.Combine(imageFolderPass, newImage));
                _selectedPayment.ImageFileName = newImage;
            }
            _selectedPayment.PaymentName = dialog.PaymentName;
            _selectedPayment.PaymentDate = dialog.PaymentDate;
            try
            {
                if (Convert.ToDecimal(dialog.Price) > 0)
                    _selectedPayment.Price = Convert.ToDecimal(dialog.Price);
            }
            catch { MessageBox.Show("Неверное значение стоимости"); }
            // Обновить список платежей
            OnGetPaymentExecuted(_selectedPayer.PayerId);
        }
        #endregion        
        #region Удаление платежа
        private ICommand _deletePaymentCommand;
        public ICommand DeletePaymentCommand =>
        _deletePaymentCommand ??=
        new RelayCommand(OnDeletePaymentExecuted, DeletePaymentCanExecute);
        // Проверка возможности удаления
        private bool DeletePaymentCanExecute(object p) =>
        _selectedPayment != null;
        private void OnDeletePaymentExecuted(object id)
        {           
            payerManager.RemovePaymentFromPayer(_selectedPayment, _selectedPayer.PayerId);          
            OnGetPaymentExecuted(_selectedPayer.PayerId);
        }
        #endregion
        #endregion
    }
}
