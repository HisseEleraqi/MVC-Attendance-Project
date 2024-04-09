﻿using AttendenceSystem.IRepo;
using AttendenceSystem.Models;
using AttendenceSystem.Repo;
using Microsoft.AspNetCore.Mvc;

namespace AttendenceSystem.Controllers
{
    public class SecurityController : Controller
    {

        private readonly IStudentRepo _studentRepo;
        private readonly TrackIRepo _trackIRepo;
        private readonly IAttendance _attendance;
        public SecurityController(IStudentRepo studentRepo, TrackIRepo trackIRepo , IAttendance attendance)
        {

            _studentRepo = studentRepo;
             _attendance = attendance;
            _trackIRepo = trackIRepo;

        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GetAllTracks()
        {

            var tracks = _trackIRepo.GetAllTracks();
            return View(tracks);
        }

        [HttpGet("GetStudentByTrackID/{id}")]
        public IActionResult GetStudentByTrackID([FromRoute] int id)
        {
            var students = _trackIRepo.GetStudentsByTrackId(id);
            return View(students);
        }

        [HttpGet("ConfirmStudent/{studentId}")]
        public IActionResult ConfirmStudentAttendace([FromRoute]int studentId)
        {
            DateTime studentDate = DateTime.Now;
            DateTime dateOnly = studentDate.Date;
            string studentTime = studentDate.ToString("hh:mm:ss");
            string correctTime = String.Format("09:00:00");

            Attendence studentAttendance = new Attendence() { Date = DateOnly.Parse(dateOnly.ToString("yyyy-MM-dd")), InTime = TimeOnly.Parse(studentTime), UserId = 2 };


            TimeSpan studentTimeSpan = TimeSpan.Parse(studentTime);
            TimeSpan correctTimeSpan = TimeSpan.Parse(correctTime);

            int comparison = TimeSpan.Compare(studentTimeSpan, correctTimeSpan);

            if (comparison == 0)
            {
              
                studentAttendance.IsPresent = true;
            }
            else if (comparison > 0)
            {
                studentAttendance.IsLate = true;
            }
            else
            {
                studentAttendance.IsPresent = true;
            }
           _attendance.ConfirmStudentAttendance(studentAttendance);

            return View();  
            

        }
      
          

        }


    }


