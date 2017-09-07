using System;
using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Infrastructure
{
    public class IdDomainComparer<TDomain> : IEqualityComparer<TDomain> where TDomain : BasicDomain
    {
        public bool Equals(TDomain x, TDomain y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            return x != null && y != null && x.Id.Equals(y.Id);
        }

        public int GetHashCode(TDomain obj)
        {
            int hashGenreName = obj.Id == null ? 0 : obj.Id.GetHashCode();

            return hashGenreName;
        }
    }
}
