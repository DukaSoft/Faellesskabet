using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary;
using DataLibrary.Booking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Faellesskabet_Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Arrangement> Arrangementer { get; set; }
		public DbSet<Nyhed> Nyheder { get; set; }
		public DbSet<Bestyrelse> Bestyrelsen { get; set; }
		public DbSet<Dokument> Dokumenter { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
	}
}
