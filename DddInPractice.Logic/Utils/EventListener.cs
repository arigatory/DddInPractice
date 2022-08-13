using DddInPractice.Logic.Common;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddInPractice.Logic.Utils
{
    public class EventListener : 
        IPostInsertEventListener, 
        IPostDeleteEventListener, 
        IPostUpdateEventListener,
        IPostCollectionUpdateEventListener
    {
        public void OnPostInsert(PostInsertEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostUpdate(PostUpdateEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostDelete(PostDeleteEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostUpdateCollection(PostCollectionUpdateEvent ev)
        {
            DispatchEvents(ev.AffectedOwnerOrNull as AggregateRoot);
        }

        private void DispatchEvents(AggregateRoot aggregateRoot)
        {
            foreach (IDomainEvent domainEvent in aggregateRoot.DomainEvents)
            {
                // TODO: change
                //DomainEvents.Dispatch(domainEvent);
            }

            aggregateRoot.ClearEvents();
        }

        public Task OnPostDeleteAsync(PostDeleteEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
