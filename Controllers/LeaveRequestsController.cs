using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProjectNew.Data;
using FinalProjectNew.Models;

namespace FinalProjectNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly LeaveContext _context;

        public LeaveRequestsController(LeaveContext context)
        {
            _context = context;
        }

        // GET: api/LeaveRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveRequest>>> GetLeaveRequests()
        {
            return await _context.LeaveRequests.ToListAsync();
        }

        // GET: api/LeaveRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequest>> GetLeaveRequest(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return leaveRequest;
        }
        
        // GET: api/LeaveRequests/Employee
        [HttpGet("Employee/{employeeid}")]
        public async Task<ActionResult<IEnumerable<LeaveRequest>>> GetLeaveRequestsByEmployeeID(int employeeid)
        {
            var leaveRequest = await _context.LeaveRequests.Where(e => e.EmployeeID == employeeid).ToListAsync();

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return leaveRequest;
        }

        // GET: api/LeaveRequests/Employee
        [HttpGet("EmployeeRequest/{employeeid}")]
        public async Task<ActionResult<int>> GetRemainingLeaveByEmployeeID(int employeeid)
        {

            var employee = await _context.Employees.FindAsync(employeeid);

            if (employee == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.Where(e => (e.EmployeeID == employeeid) && (e.LeaveDate.Year == DateTime.Now.Year) && (e.Cancelled == false)).ToListAsync();

            if (leaveRequest == null)
            {
                return NotFound();
            }

            int HoursTaken = 0;
            foreach(var e in leaveRequest)
            {
                if(e.DurationType == DurationType.HALF_DAY)
                {
                    HoursTaken += 4;
                }
                else if(e.DurationType == DurationType.FULL_DAY)
                {
                    HoursTaken += 8;
                }
            }

            int RemainingHours = 80 + ((employee.YearsServed / 5) * 40)-HoursTaken;
            return RemainingHours;
        }

        // PUT: api/LeaveRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveRequest(int id, LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return BadRequest();
            }
           
            if (leaveRequest.DurationType > DurationType.FULL_DAY)
            {
                return BadRequest();
            }


            _context.Entry(leaveRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LeaveRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveRequest>> PostLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DurationType > DurationType.FULL_DAY)
            {
                return BadRequest();
            }

            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveRequest", new { id = leaveRequest.Id }, leaveRequest);
        }

        // DELETE: api/LeaveRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.Id == id);
        }

    }
}
