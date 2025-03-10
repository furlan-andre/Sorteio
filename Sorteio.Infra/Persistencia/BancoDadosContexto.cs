using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Infra.Mapeadores;

namespace Sorteio.Infra;

public class BancoDadosContexto : DbContext
{
    private IConfiguration _configuration;
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Familia> Familias { get; set; }

    public BancoDadosContexto(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("Sorteio");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PessoaConfiguration());
        modelBuilder.ApplyConfiguration(new FamiliaConfiguration());
    }
}