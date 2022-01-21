﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
	public class AppIdentityDbContext : IdentityDbContext
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
			: base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }

		public DbSet<Home> Home { get; set; }

		public DbSet<Testimonial> Testimonials { get; set; }

		public DbSet<Ingredient> Ingredients { get; set; }
	}
}