using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Borrowers;
using Infrastructure;
using ApiTP1MethodeDeDev.Dtos;
using Domain.LatePayment;
using Domain.Bank;
using Domain.Emploi;

namespace ApiTP1MethodeDeDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowersController : ControllerBase
    {

        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        public IEnumerable<BorrowerReponse> Get()
        {
            return _borrowerService.GetAll().Select(b =>
                new BorrowerReponse
                {
                    Sin = b.Sin,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Phone = b.Phone,
                    Email = b.Email,
                    Address = b.Address,
                    Equifax_Result = b.Equifax_Result,
                    BankruptyDate = b.BankruptyDate,
                    OtherBankLoans = b.OtherBankLoans,
                    NumberOfLatePayments = b.NumberOfLatePayments,
                    EmploymentHistory = b.EmploymentHistory

                });
        }

        [HttpGet("{sin}")]
        public ActionResult<BorrowerReponse> Get(string sin)
        {
            try
            {
                Borrower borrower = _borrowerService.GetBySin(sin);

                return Ok(
                    new BorrowerReponse
                    {
<<<<<<< HEAD
                        Sin = borrower.Sin,
=======
>>>>>>> parent of f589cb5 (delete of the existing code to searsh for the one in develop)
                        LastName = borrower.LastName,
                        FirstName = borrower.FirstName,
                        Phone = borrower.Phone,
                        Email = borrower.Email,
                        Address = borrower.Address,
                        Equifax_Result = borrower.Equifax_Result,
                        BankruptyDate = borrower.BankruptyDate,
                        OtherBankLoans = borrower.OtherBankLoans,
                        NumberOfLatePayments = borrower.NumberOfLatePayments,
                        EmploymentHistory = borrower.EmploymentHistory
                    }
                );
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] BorrowerResquest borrowerResquest)
        {
            if (borrowerResquest == null)
                return BadRequest("Invalid borrower data");

            var borrower = new Borrower
            {
                Sin = borrowerResquest.Sin,
                FirstName = borrowerResquest.FirstName,
                LastName = borrowerResquest.LastName,
                Phone = borrowerResquest.Phone,
                Email = borrowerResquest.Email,
                Address = borrowerResquest.Address,
                Equifax_Result = borrowerResquest.Equifax_Result,
                BankruptyDate = borrowerResquest.BankruptyDate,
                OtherBankLoans = borrowerResquest.OtherBankLoans.Select(otherBank => new OtherBankLoan
                {
                    BankName = otherBank.BankName,
                    Mensuality = otherBank.Mensuality,
                    RemainingBalance = otherBank.RemainingBalance,
                    Reason = otherBank.Reason
                }).ToList(),

                NumberOfLatePayments = borrowerResquest.NumberOfLatePayments.Select(date => new LatePaymentBorrower
                {
                    LatePaymentDate = date.LatePaymentDate
                }).ToList(),

                EmploymentHistory = borrowerResquest.EmploymentHistory.Select(employHistory => new Job
                {
                    InstitutionName = employHistory.InstitutionName,
                    StartingDate = employHistory.StartingDate,
                    EndingDate = employHistory.EndingDate,
                    MentualSalary = employHistory.MentualSalary
                }).ToList()
            };

            string borrowerSin = _borrowerService.Add(borrower);
            return CreatedAtAction(nameof(Get), new { sin = borrowerSin }, borrowerSin);
        }

        [HttpPut("{sin}")]
        public IActionResult Put(string sin, [FromBody] BorrowerResquest borrowerResquest)
        {
            var borrower = _borrowerService.GetBySin(sin);
            if (borrower == null)
                return NotFound();

<<<<<<< HEAD

=======
>>>>>>> parent of f589cb5 (delete of the existing code to searsh for the one in develop)
            borrower.FirstName = borrowerResquest.FirstName;
            borrower.LastName = borrowerResquest.LastName;
            borrower.Phone = borrowerResquest.Phone;
            borrower.Email = borrowerResquest.Email;
            borrower.Address = borrowerResquest.Address;
            borrower.Equifax_Result = borrowerResquest.Equifax_Result;
            borrower.BankruptyDate = borrowerResquest.BankruptyDate;

            borrower.OtherBankLoans = borrowerResquest.OtherBankLoans.Select(otherBank => new OtherBankLoan
            {
                BankName = otherBank.BankName,
                Mensuality = otherBank.Mensuality,
                RemainingBalance = otherBank.RemainingBalance,
                Reason = otherBank.Reason
            }).ToList();

            borrower.NumberOfLatePayments = borrowerResquest.NumberOfLatePayments.Select(date => new LatePaymentBorrower
            {
                LatePaymentDate = date.LatePaymentDate
            }).ToList();

            borrower.EmploymentHistory = borrowerResquest.EmploymentHistory.Select(job => new Job
            {
                InstitutionName = job.InstitutionName,
                StartingDate = job.StartingDate,
                EndingDate = job.EndingDate,
                MentualSalary = job.MentualSalary
            }).ToList();

            _borrowerService.Update(borrower);
            return NoContent();
        }


    }
}
