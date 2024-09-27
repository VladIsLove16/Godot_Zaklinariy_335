using Zaklinariy_Godot353.Scripts;

public class DifficultyBasedSpawnDelayProvider : ISpawnDelayProvider
{
    private readonly Level difficultyLevel;

    public DifficultyBasedSpawnDelayProvider(Level difficultyLevel)
    {
        this.difficultyLevel = difficultyLevel;
    }

    public int GetSpawnDelayMultiplier()
    {
        switch (difficultyLevel)
        {
            case Level.easy:
                return 4;
            case Level.normal:
                return 3;
            case Level.hard:
                return 2;
            default:
                return 3;
        }
    }
}
