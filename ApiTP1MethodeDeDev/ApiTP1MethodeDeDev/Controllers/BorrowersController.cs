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
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Phone = b.Phone,
                    Email = b.Email,
                    Address = b.Address
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
                        LastName = borrower.LastName,
                        FirstName = borrower.FirstName,
                        Phone = borrower.Phone,
                        Email = borrower.Email,
                        Address = borrower.Address
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

            borrower.FirstName = borrowerResquest.FirstName;
            borrower.LastName = borrowerResquest.LastName;
            borrower.Phone = borrowerResquest.Phone;
            borrower.Email = borrowerResquest.Email;
            borrower.Address = borrowerResquest.Address;

            _borrowerService.Update(borrower);
            return NoContent();
        }




    }
}
