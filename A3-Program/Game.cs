namespace COIS2020./* FirstnameLastnameStudentnumber */.Assignment3;

using Microsoft.Xna.Framework; // Needed for Vector2
using TrentCOIS.Tools.Visualization;
using COIS2020.StarterCode.Assignment3;

public class CastleDefender : Visualization
{
    public LinkedList<Wizard> WizardSquad { get; private set; }
    public Queue<Wizard> RecoveryQueue { get; private set; }

    public LinkedList<Goblin> GoblinSquad { get; private set; }
    public LinkedList<Goblin> BackupGoblins { get; private set; }

    public LinkedList<Spell> Spells { get; private set; }
    public Node<Wizard>? ActiveWizard { get; private set; }

    private uint nextSpellTime;


    public CastleDefender()
    {

    }


    protected override void Update(uint currentFrame)
    {

    }
}
