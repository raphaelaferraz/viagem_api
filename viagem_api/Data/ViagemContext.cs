using Microsoft.EntityFrameworkCore;
using viagem_api.Models;

namespace viagem_api.Data;

public class ViagemContext : DbContext
{
    public ViagemContext(DbContextOptions<ViagemContext> configuracoes) : base(configuracoes)
    {
        
    }

    public DbSet<Depoimento> Depoimento { get; set; }
}
