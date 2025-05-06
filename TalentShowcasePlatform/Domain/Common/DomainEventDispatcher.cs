using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common;

public class DomainEventDispatcher : IDomainEventDispatcher
{
	private readonly IMediator mediator;

	public async Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents)
	{
		foreach (var entity in entitiesWithEvents)
		{
			var events = entity.DomainEvents.ToArray();

			entity.ClearDomainEvents();

			foreach (var domainEvent in events)
			{
				await mediator.Publish(domainEvent).ConfigureAwait(false);
			}
		}
	}
}
