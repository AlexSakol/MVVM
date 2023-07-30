using System;
using System.Collections.Generic;
using System.Linq;
using WPF_LAB_9.Businnes.Managers;
using WPF_LAB_9.Domain.Entities;

namespace WPF_LAB_9.Businnes.Infrastructure
{
    public static class DbTestData
    {
        public static void SetupData(PayerManager payerManager)
        {
            // Добавление плательщиков
            payerManager.AddRange(new List<Payer> {
                new Payer{
                 DateOfBirth = new DateTime(1980, 07, 03),
                 PayerName = $"Иван Иванов",
                },
                new Payer{
                 DateOfBirth = new DateTime(1983, 04, 10),
                 PayerName = $"Мария Петрова",
                }
                });
            var payers = payerManager.Payers.ToArray();

            // Добавление платежей
            payerManager.AddPaymentToPayer(
            new Payment
            {
                PaymentName = "Авто",
                Price = 100.00m,
                PaymentDate = new DateTime(2023, 05, 11),
                ImageFileName = "1.jpg"
            },
            payers[0].PayerId
            );
            payerManager.AddPaymentToPayer(
            new Payment
            {
                PaymentName = "Еда",
                Price = 35.60m,
                PaymentDate = new DateTime(2023, 05, 08),
                ImageFileName = "2.jpg"
            },
            payers[0].PayerId
            );
           payerManager.AddPaymentToPayer(
           new Payment
           {
               PaymentName = "Одежда",
               Price = 55.85m,
               PaymentDate = new DateTime(2023, 05, 24),
               ImageFileName = "3.jpg"
           },
           payers[1].PayerId
           );
           payerManager.AddPaymentToPayer(
           new Payment
           {
               PaymentName = "Еда",
               Price = 48.95m,
               PaymentDate = new DateTime(2023, 05, 21),
               ImageFileName = "2.jpg"
           },
           payers[1].PayerId
           );
           payerManager.AddPaymentToPayer(
           new Payment
           {
               PaymentName = "Коммуналка",
               Price = 90.00m,
               PaymentDate = new DateTime(2023, 05, 23),
               ImageFileName = "4.jpg"
           },
           payers[1].PayerId
           );
        }
    }
}
