using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers
{
	public class BlogController : Controller
	{
		private readonly AppIdentityDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;

		public BlogController(AppIdentityDbContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		// GET
		public async Task<IActionResult> Index(int? pageNumber)
		{
			var posts = from p in _db.Posts orderby p.Title where p.IsApproved && p.Type.Equals(PostType.Blog.ToString()) select p ;
			return View(await PaginatedList<Post>.CreateAsync(posts.AsNoTracking(),pageNumber ?? 1, 3));
		}
		
		// GET
		public async Task<ActionResult> Post(int? id)
		{
			var post = new Post();
			if (id == null) return View(post);
			post = await _db.Posts.FindAsync(id);
			return View(post);
		}
		
		// GET
		public IActionResult Create()
		{
			return View();
		}
		
		// POST: Posts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Title,TagLine,Content,Author")] Post post)
		{
			if (ModelState.IsValid && post.Author != null)
			{
				post.CreatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture);
				post.UpdatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture);
				post.IsApproved = false;
				post.Type = PostType.Blog.ToString();
				post.User = await _userManager.FindByNameAsync(post.Author);
				await _db.Posts.AddAsync(post);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(post);
		}
	}
}