using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieReviews_MVC.Models;
using MovieReviews_MVC.Models.Entities;
using MovieReviews_MVC.Models.ViewModels;

namespace MovieReviews_MVC.Controllers
{
  public class FilmCrewMembersController : Controller
  {
    private ApplicationDbContext ctx = new ApplicationDbContext();

    // GET: FilmCrewMembers

    [Route("FilmCrewMembers/Index/{crewRole:alpha}")]
    public ActionResult Index(string crewRole)
    {
      Enum.TryParse(crewRole, true, out MovieRole role);

      ViewBag.Title = crewRole;
      return View();
    }


    
    public PartialViewResult ActorCards()
    {

      var vm = ctx.FilmCrewMembers
        .Where(cm => cm.Role == MovieRole.Actor)
        .Select(cm => new FilmCrewMemberCardModel()
        {
          Id = cm.Id,
          Name = cm.Name,
          ImageUri = cm.ImageUri
        })
        .ToList();

      return PartialView("_FilmCrewMemberCard", vm);
    }

    public PartialViewResult DirectorCards()
    {

      var vm = ctx.FilmCrewMembers
        .Where(cm => cm.Role == MovieRole.Director)
        .Select(cm => new FilmCrewMemberCardModel()
        {
          Id = cm.Id,
          Name = cm.Name,
          ImageUri = cm.ImageUri
        })
        .ToList();

      return PartialView("_FilmCrewMemberCard", vm);
    }

    // GET: FilmCrewMembers/Details/5
    public async Task<ActionResult> Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      FilmCrewMember filmCrewMember = await ctx.FilmCrewMembers.FindAsync(id);
      if (filmCrewMember == null)
      {
        return HttpNotFound();
      }
      return View(filmCrewMember);
    }

    // GET: FilmCrewMembers/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: FilmCrewMembers/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,Name,Bio,DoB,ImageUri,Role")] FilmCrewMember filmCrewMember)
    {
      if (ModelState.IsValid)
      {
        ctx.FilmCrewMembers.Add(filmCrewMember);
        await ctx.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      return View(filmCrewMember);
    }

    // GET: FilmCrewMembers/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      FilmCrewMember filmCrewMember = await ctx.FilmCrewMembers.FindAsync(id);
      if (filmCrewMember == null)
      {
        return HttpNotFound();
      }
      return View(filmCrewMember);
    }

    // POST: FilmCrewMembers/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Bio,DoB,ImageUri,Role")] FilmCrewMember filmCrewMember)
    {
      if (ModelState.IsValid)
      {
        ctx.Entry(filmCrewMember).State = EntityState.Modified;
        await ctx.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(filmCrewMember);
    }

    // GET: FilmCrewMembers/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      FilmCrewMember filmCrewMember = await ctx.FilmCrewMembers.FindAsync(id);
      if (filmCrewMember == null)
      {
        return HttpNotFound();
      }
      return View(filmCrewMember);
    }

    // POST: FilmCrewMembers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      FilmCrewMember filmCrewMember = await ctx.FilmCrewMembers.FindAsync(id);
      ctx.FilmCrewMembers.Remove(filmCrewMember);
      await ctx.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        ctx.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
