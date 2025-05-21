using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistences.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.BEContext;

public class BEContext : DbContext
{
	private readonly IConfiguration _configuration;
	private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
	private readonly IDomainEventDispatcher _dispatcher;
	public BEContext(DbContextOptions<BEContext> options, 
					IConfiguration configuration, 
					PublishDomainEventsInterceptor publishDomainEventsInterceptor, 
					IDomainEventDispatcher dispatcher) : base(options)
	{
		_configuration = configuration;
		_publishDomainEventsInterceptor = publishDomainEventsInterceptor;
		_dispatcher = dispatcher;
	}
	protected BEContext()
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Ignore<List<BaseEvent>>()
					.ApplyConfigurationsFromAssembly(typeof(BEContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}

	override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BEContext"))
						.AddInterceptors(_publishDomainEventsInterceptor);
		}
		base.OnConfiguring(optionsBuilder);
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<UserTalent> UserTalents { get; set; }
	public DbSet<Video> Videos { get; set; }
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<GroupMember> GroupMembers { get; set; }
	public DbSet<CommentGroupPost> CommentGroupPosts { get; set; }
	public DbSet<Contest> Contests { get; set; }
	public DbSet<ContestEntry> ContestEntries { get; set; }
	public DbSet<Message> Messages { get; set; }
	public DbSet<Notification> Notifications { get; set; }
	public DbSet<View> Views { get; set; }
	public DbSet<Job> Jobs { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<VideoLike> VideoLikes { get; set; }
	public DbSet<Award> Awards { get; set; }
	public DbSet<Certification> Certifications { get; set; }

}
