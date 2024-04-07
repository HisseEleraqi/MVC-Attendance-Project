﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendenceSystem.Data;
using AttendenceSystem.Models;
using System.Security.Claims;

namespace AttendenceSystem.Controllers
{
    public class SchedulesController : Controller
    {
        DataContext db = new DataContext();
        private string _trackName;
        private int _trackId;
        public SchedulesController()
        {
            RetrieveTrackInfo();
        }
        private void RetrieveTrackInfo()
        {
            int id = 3; // Get the id from the session
            string trackName = db.Tracks.FirstOrDefault(a => a.SupervisorId == id).Name;
            int trackId = db.Tracks.FirstOrDefault(a => a.SupervisorId == id).Id;
            _trackName = trackName;
            _trackId = trackId;

        }
        // GET: Schedules
        public IActionResult Index()
        {
       
            ViewData["TrackName"] = _trackName;

            var Schedules = db.Schedules.Include(s => s.Tracks).ToList();
            return View(Schedules);
        }

        // GET: Schedules/Details/id
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule =  db.Schedules
                .Include(s => s.Tracks)
                .FirstOrDefault(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            int id = 3; // Get the id from the session
            // Get its track
            string trackName = db.Tracks.FirstOrDefault(a => a.SupervisorId == id).Name;
            ViewData["TrackName"] = trackName;

            return View();
        }

        // POST: Schedules/Create
        [HttpPost]
        public IActionResult Create(Schedule schedule)
        {
            int id = 3; // Get the id from the session
            string trackName = db.Tracks.FirstOrDefault(a => a.SupervisorId == id).Name;
            ViewData["TrackName"] = trackName;

            int trackId = db.Tracks.FirstOrDefault(a => a.SupervisorId == id).Id;
            schedule.TrackId = trackId;
            if (ModelState.IsValid)
            {   
                db.Add(schedule);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrackName"] = trackName;
            return View(schedule);
        }

        // GET: Schedules/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule =  db.Schedules.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        [HttpPost]
        public IActionResult Edit(int id, Schedule schedule)
        {
            int Insid = 3; // Get the id from the session
            string trackName = db.Tracks.FirstOrDefault(a => a.SupervisorId == Insid).Name;
            ViewData["TrackName"] = trackName;

            int trackId = db.Tracks.FirstOrDefault(a => a.SupervisorId == Insid).Id;

            if (id != schedule.Id)
            {
                return NotFound();
            }
            schedule.TrackId = trackId;
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(schedule);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule =  db.Schedules
                .Include(s => s.Tracks)
                .FirstOrDefault(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var schedule =  db.Schedules.Find(id);
            if (schedule != null)
            {
                db.Schedules.Remove(schedule);
            }

             db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return db.Schedules.Any(e => e.Id == id);
        }
    }
}
