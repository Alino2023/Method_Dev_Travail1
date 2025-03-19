using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Borrower;
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

    }
}
