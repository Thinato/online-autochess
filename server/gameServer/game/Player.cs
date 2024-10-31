namespace gameServer.game.entites;

public partial class Player
{
    public Hero[] Inventory { get; private set; }
    public Hero[] Battlefield { get; private set; }
    public Hero[] CurrentRoll { get; private set; }

    public uint Gold { get; private set; }
    public uint MaxGold { get; private set; }
    public uint RollMax { get; private set; }

    public uint Population { get; private set; }
    public uint MaxPopulation { get; private set; }

    public uint Level { get; private set; }
    public uint Experience { get; private set; }
    public uint ExperienceToLevel { get; private set; }

    public Player()
    {
        Inventory = new Hero[8];
        Battlefield = new Hero[8];
        CurrentRoll = new Hero[5];

        Gold = 0;
        MaxGold = 200;
        RollMax = 3;

        Population = 0;
        MaxPopulation = 10;

        Level = 1;
        Experience = 0;
    }

    public void AddGold(uint amount)
    {
        Gold += amount;
        if (Gold > MaxGold)
            Gold = MaxGold;
    }

    public void AddPopulation(uint amount)
    {
        Population += amount;
        if (Population > MaxPopulation)
            Population = MaxPopulation;
    }

    public void AddExperience(uint amount)
    {
        Experience += amount;
        if (Experience >= ExperienceToLevel)
        {
            Experience = (uint)(0);
            Level++;
        }
    }

    public void BuyHero(uint index)
    {
        Hero hero = CurrentRoll[index];

        if (Gold < hero.cost)
            return;

        if (Population >= MaxPopulation)
            return;

        if (Inventory[index] != null)
            return;

        AddGold(5);
        AddPopulation(1);
        Inventory[index] = new Hero();
    }
}