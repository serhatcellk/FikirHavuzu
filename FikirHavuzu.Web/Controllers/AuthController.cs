using System.Security.Claims;
using FikirHavuzu.Entities;
using FikirHavuzu.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FikirHavuzu.Web.Controllers;

public class AuthController : Controller
{
    private readonly IKullaniciService _kullaniciService;
    private readonly IYetkiService _yetkiService;

    public AuthController(IKullaniciService kullaniciService, IYetkiService yetkiService)
    {
        _kullaniciService = kullaniciService;
        _yetkiService = yetkiService;
    }

    public IActionResult Giris()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Giris(string mail, string sifre)
    {
        var kullanici = await _kullaniciService.GirisDogrulaAsync(mail, sifre);

        if (kullanici is null)
        {
            ViewBag.Hata = "Mail veya şifre hatalı.";
            return View();
        }

        if (!kullanici.AktifMi)
        {
            ViewBag.Hata = "Hesabınız aktif değildir.";
            return View();
        }

        var yetkiler = await _yetkiService.KullaniciYetkileriniGetirAsync(kullanici.Id);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString()),
            new Claim(ClaimTypes.Name, kullanici.Ad + " " + kullanici.Soyad),
            new Claim("Mail", kullanici.MailAdresi)
        };

        foreach (var ky in yetkiler)
            claims.Add(new Claim(ClaimTypes.Role, ky.Yetki.Ad));

        var identity = new ClaimsIdentity(claims, "FikirHavuzuAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("FikirHavuzuAuth", principal);

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Cikis()
    {
        await HttpContext.SignOutAsync("FikirHavuzuAuth");
        return RedirectToAction("Giris");
    }

    public IActionResult YetkiYok()
    {
        return View();
    }
}