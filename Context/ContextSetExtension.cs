using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Context
{
    public static class ContextSetExtension
    {
        public static IQueryable<object> Set(this DbContext _context, Type t)
        {
            var res = _context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
            return (IQueryable<object>)res;
        }


        public static IQueryable<T> Set2<T>(this DbContext _context, T t)
        {
            var typo = t.GetType();
            return (IQueryable<T>)_context.GetType().GetMethod("Set").MakeGenericMethod(typo).Invoke(_context, null);
        }
    }
}
