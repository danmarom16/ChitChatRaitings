using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChitChatRaitings.Models;

namespace ChitChatRaitings.Data
{
    public class ChitChatRaitingsContext : DbContext
    {
        public ChitChatRaitingsContext (DbContextOptions<ChitChatRaitingsContext> options)
            : base(options)
        {
        }

        public DbSet<ChitChatRaitings.Models.Feedback>? Feedback { get; set; }
    }
}
