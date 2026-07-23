using FikirHavuzu.Entities;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repositories;

public class YetkiRepository : IYetkiRepository
{
    private readonly FikirHavuzuDbContext _context;

    public YetkiRepository(FikirHavuzuDbContext context)
    {
        _context = context;
    }

    public async Task<List<Yetki>> HepsiniGetirAsync()
        => await _context.Yetkiler.ToListAsync();

    public async Task<Yetki?> IdIleGetirAsync(int id)
        => await _context.Yetkiler.FindAsync(id);

    public async Task EkleAsync(Yetki yetki)
        => await _context.Yetkiler.AddAsync(yetki);

    public async Task<List<KullaniciYetki>> KullaniciYetkileriniGetirAsync(int kullaniciId)
        => await _context.KullaniciYetkileri
            .Include(ky => ky.Yetki)
            .Where(ky => ky.KullaniciId == kullaniciId)
            .ToListAsync();

    public async Task YetkiAtaAsync(int kullaniciId, int yetkiId)
    {
        var atama = new KullaniciYetki { KullaniciId = kullaniciId, YetkiId = yetkiId };
        await _context.KullaniciYetkileri.AddAsync(atama);
    }

    public async Task YetkiKaldirAsync(int kullaniciId, int yetkiId)
    {
        var atama = await _context.KullaniciYetkileri
            .FirstOrDefaultAsync(ky => ky.KullaniciId == kullaniciId && ky.YetkiId == yetkiId);
        if (atama is not null)
            _context.KullaniciYetkileri.Remove(atama);
    }

    public async Task<bool> AtamaVarMiAsync(int kullaniciId, int yetkiId)
        => await _context.KullaniciYetkileri
            .AnyAsync(ky => ky.KullaniciId == kullaniciId && ky.YetkiId == yetkiId);

    public async Task<bool> KaydetAsync()
        => await _context.SaveChangesAsync() > 0;
}