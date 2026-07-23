using FikirHavuzu.Entities;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repositories;

public class FikirRepository : IFikirRepository
{
    private readonly FikirHavuzuDbContext _context;

    public FikirRepository(FikirHavuzuDbContext context)
    {
        _context = context;
    }

    public async Task<List<Fikir>> HepsiniGetirAsync()
        => await _context.Fikirler
            .Include(f => f.Kullanici)
            .OrderByDescending(f => f.OlusturmaTarihi)
            .ToListAsync();

    public async Task<Fikir?> IdIleGetirAsync(int id)
        => await _context.Fikirler
            .Include(f => f.Kullanici)
            .FirstOrDefaultAsync(f => f.Id == id);

    public async Task EkleAsync(Fikir fikir)
        => await _context.Fikirler.AddAsync(fikir);

    public void Guncelle(Fikir fikir)
        => _context.Fikirler.Update(fikir);

    public async Task<bool> KaydetAsync()
        => await _context.SaveChangesAsync() > 0;
}