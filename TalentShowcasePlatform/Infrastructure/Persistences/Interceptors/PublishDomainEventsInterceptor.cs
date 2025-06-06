﻿using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistences.Interceptors;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
	private readonly IPublisher _mediator;

	public PublishDomainEventsInterceptor(IPublisher mediator)
	{
		_mediator = mediator;
	}
	public override InterceptionResult<int> SavingChanges(
		DbContextEventData eventData,
		InterceptionResult<int> result
	)
	{
		PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
		return base.SavingChanges(eventData, result);
	}

	public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default
	)
	{
		await PublishDomainEvents(eventData.Context);
		return await base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private async Task PublishDomainEvents(DbContext? dbContext)
	{
		if (dbContext is null)
		{
			return;
		}

		var entitiesWithDomainEvents = dbContext.ChangeTracker
			.Entries<BaseEntity>()
			.Where(entry => entry.Entity.DomainEvents.Any())
			.Select(entry => entry.Entity)
			.ToList();

		var domainEvents = entitiesWithDomainEvents
			.SelectMany(entry => entry.DomainEvents)
			.ToList();

		entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

		foreach (var domainEvent in domainEvents)
		{
			await _mediator.Publish(domainEvent);
		}
	}
}
