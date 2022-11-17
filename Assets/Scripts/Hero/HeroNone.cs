public class HeroNone : IHeroPlay
{
    public void BeginPlay()
    {
    }

    public void EndPlay()
    {
    }

    public HeroState UpdatePlay()
    {
        return HeroState.none;
    }

}
