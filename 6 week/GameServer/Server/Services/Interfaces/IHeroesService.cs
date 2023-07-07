using Server.Models;

public interface IHeroesService
{
    public HeroesSettings CreateDefaultHero(int heroId);
}