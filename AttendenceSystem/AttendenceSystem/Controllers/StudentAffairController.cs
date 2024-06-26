﻿using AttendenceSystem.IRepo;
using AttendenceSystem.Models;
using AttendenceSystem.Repo;
using AttendenceSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendenceSystem.Controllers
{
    [Authorize(Roles = "Student_affairs")]
    public class StudentAffairController : Controller
    {
        private readonly IStudentService studentService;
        private readonly IStudentRepo studentRepo;
        private readonly TrackIRepo trackIRepo;

        public StudentAffairController(IStudentService _studentService, IStudentRepo _studentRepo, TrackIRepo _trackIRepo)
        {
            studentService = _studentService;
            studentRepo = _studentRepo;
            trackIRepo = _trackIRepo;
        }
        public IActionResult Attendance()
        {
            List<Track> tracks = trackIRepo.GetActiveTracks();
            return View(tracks);
        }
        [HttpGet]
        public IActionResult GetStudentDetails(int id)
        {
            // Retrieve student details based on the selected name
            var student = studentRepo.GetStudentById(id);

            if (student != null)
            {
                return PartialView("_StudentDetailsPartial", student);
            }

            return NotFound();
        }
        [HttpGet]
        public IActionResult StudentAttendance(int studentId)
        {
            StudentAttendanceViewModel viewModel = studentService.GetStudentAttendance(3);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult StudentAttendanceAtTrack(int trackId, DateOnly date)
        {
            //DateOnly date1 = new DateOnly(2024, 1, 4);
            List<StudentAttendanceViewModel> viewModels = studentService.GetTrackAttendancedate(trackId, date);

            return PartialView("StudentAttendanceAtTrack", viewModels);
            //return Json(viewModels);
        }
        [HttpPost]
        public IActionResult EditDegree(int id , int perid)
        {
            Student student = studentRepo.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            // Update student degree
            studentRepo.UpdateStudentDegree(id,perid);

            // Return updated degree value (assuming you want to return it)
            return Json(new { degree = student.Degree });
        }







    }

} 


