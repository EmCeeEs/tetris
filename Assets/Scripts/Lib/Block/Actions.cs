using Redux;

public readonly struct AddBlockAction : IAction
{
    public readonly Block block;

    public AddBlockAction(Block newBlock)
    {
        block = newBlock;
    }
}
