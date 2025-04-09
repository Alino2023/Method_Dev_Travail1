using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.LatePayment;

namespace Tests
{
    [TestClass]
    public class LatePaymentBorrowerTests
    {
        [TestMethod]
        public void LatePaymentBorrower_ShouldBeValid_WhenDateIsProvided()
        {
            var latePayment = new LatePaymentBorrower
            {
                LatePaymentDate = DateTime.Now.AddDays(-30)
            };

            var context = new ValidationContext(latePayment, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(latePayment, context, results, true);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void LatePaymentBorrower_ShouldFailValidation_IfDateIsDefault()
        {
            var latePayment = new LatePaymentBorrower
            {
                LatePaymentDate = default 
            };

            var context = new ValidationContext(latePayment, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(latePayment, context, results, true);

            Assert.IsTrue(isValid);
        }
    }
}
