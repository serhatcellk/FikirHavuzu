using FikirHavuzu.Entities;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repositories;

public class KullaniciRepository : IKullaniciRepository
{
    private readonly FikirHavuzuDbContext _context;

    public KullaniciRepository(FikirHavuzuDbContext context)
    {
        _context = context;
    }

    public async Task<List<Kullanici>> HepsiniGetirAsync()
        => await _context.Kullanicilar.ToListAsync();

    public async Task<Kullanici?> IdIleGetirAsync(int id)
        => await _context.Kullanicilar.FindAsync(id);

    public async Task EkleAsync(Kullanici kullanici)
        => await _context.Kullanicilar.AddAsync(kullanici);

    public void Guncelle(Kullanici kullanici)
        => _context.Kullanicilar.Update(kullanici);

    public async Task<bool> KaydetAsync()
        => await _context.SaveChangesAsync() > 0;
}