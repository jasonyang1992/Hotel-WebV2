using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private BookingDBContext db = new BookingDBContext();

        // GET: Bookings
        public ActionResult Index(string searchString)
        {
            var Bookings = from g in db.Bookings
                               select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                Bookings = Bookings.Where(s => s.GuestName.Contains(searchString));
            }

            return View(Bookings);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GuestName,Address,City,State,ZipCode,Email,PhoneNumber,RoomNum,DateIn,DateOut,NumberOfAdults,NumberOfKids,GrandTotal,TotalPaid,Balance")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GuestName,Address,City,State,ZipCode,Email,PhoneNumber,RoomNum,DateIn,DateOut,NumberOfAdults,NumberOfKids,GrandTotal,TotalPaid,Balance")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult PDF(int id)
        {
            // This code is for export Database data to PDF file
            string fileName = Guid.NewGuid() + ".pdf";
            string filePath = System.IO.Path.Combine(Server.MapPath("~/PDFFiles"), fileName);

            Document doc = new Document(PageSize.A4.Rotate(), 2, 2, 2, 2);
            // Create paragraph for show in PDF file header
            Paragraph p = new Paragraph("Export Database data to PDF file in ASP.NET");
            //p.SetAlignment("center");

            //try
            //{
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            //Create table here for write database data
            PdfPTable pdfTab = new PdfPTable(8); // here 7 is no of column
            pdfTab.HorizontalAlignment = 1; // 0- Left, 1- Center, 2- right
            pdfTab.SpacingBefore = 20f;
            pdfTab.SpacingAfter = 20f;

            var data = new Booking();
            using (BookingDBContext dc = new BookingDBContext())
            {
                data = dc.Bookings.First(x => x.ID == id);
            }

            pdfTab.AddCell("ID");
            pdfTab.AddCell("Guest Name");
            pdfTab.AddCell("Address");
            pdfTab.AddCell("City");
            pdfTab.AddCell("State");
            pdfTab.AddCell("Date In");
            pdfTab.AddCell("Date Out");
            pdfTab.AddCell("Balance");

            pdfTab.AddCell(data.ID.ToString());
            pdfTab.AddCell(data.GuestName);
            pdfTab.AddCell(data.Address);
            pdfTab.AddCell(data.City);
            pdfTab.AddCell(data.State);
            pdfTab.AddCell(data.DateIn.ToString("MM/dd/yyyy"));
            pdfTab.AddCell(data.DateOut.ToString("MM/dd/yyyy"));
            pdfTab.AddCell(data.Balance.ToString("C"));

            doc.Open();
            doc.Add(p);
            doc.Add(pdfTab);
            doc.Close();

            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine("Hello, I am a new text file");

                }
            }

            return File(info.OpenRead(), "application/pdf");
            /*}
            catch (Exception)
            {

                throw;
            }
            finally
            {
                doc.Close();
            }*/
        }
    }
}
