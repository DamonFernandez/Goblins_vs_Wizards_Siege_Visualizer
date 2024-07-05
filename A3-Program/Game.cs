namespace COIS2020.DamonFernandez0813575.Assignment3;

using Microsoft.Xna.Framework; // Needed for Vector2
using TrentCOIS.Tools.Visualization;
using COIS2020.StarterCode.Assignment3;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;


public class CastleDefender : Visualization
{
    public LinkedList<Wizard> WizardSquad { get; private set; }
    public Queue<Wizard> RecoveryQueue { get; private set; }

    public LinkedList<Goblin> GoblinSquad { get; private set; }
    public LinkedList<Goblin> BackupGoblins { get; private set; }

    public LinkedList<Spell> Spells { get; private set; }
    public Node<Wizard>? ActiveWizard { get; private set; }

    private uint nextSpellTime;

    private Vector2 goblinDirection;


    private readonly Random random = new Random();

    private uint nextWizardEatingTime;

    private void addStarterWizards()
    {
        WizardSquad = new LinkedList<Wizard>();
        for (int i = 0; i < 8; i++)
        {
            Wizard currentWizard = new Wizard();
            WizardSquad.AddBack(currentWizard);
        }
    }

    private void addStarterGoblins()
    {
        GoblinSquad = new LinkedList<Goblin>();

        for (int i = 0; i < 8; i++)
        {
            Goblin currentGoblin = new Goblin();
            GoblinSquad.AddBack(currentGoblin);
        }
    }
    private void addBackUpGoblins()
    {

        BackupGoblins = new LinkedList<Goblin>();
        for (int i = 0; i < 6; i++)
        {
            Goblin currentGoblin = new Goblin();
            BackupGoblins.AddBack(currentGoblin);
        }
    }

    private uint calculateNextSpellTime()
    {
        float addOrSubtractDecider = random.NextSingle();

        if (addOrSubtractDecider >= 0.50f)
        {
            nextSpellTime = (uint)(15 + (5 * random.Next()));
        }
        else
        {
            nextSpellTime = (uint)(15 - (5 * random.Next()));
        }

        return 5;
        return nextSpellTime;
    }

    private Vector2 calculateGoblinDirection()
    {
        float randomXDir = random.NextSingle() * 2 - 1;
        float randomYDir = random.NextSingle() * 2 - 1;
        Vector2 goblinDirection = new Vector2(randomXDir, randomYDir);
        goblinDirection.Normalize();
        return goblinDirection;
    }
    public CastleDefender()
    {
        addStarterWizards();
        addStarterGoblins();
        addBackUpGoblins();


        ActiveWizard = WizardSquad!.Head;
        nextSpellTime = calculateNextSpellTime();
        goblinDirection = calculateGoblinDirection();
        Spells = new LinkedList<Spell>();
        nextWizardEatingTime = 0;
        RecoveryQueue = new Queue<Wizard>();
    }



    private void updateSpells()
    {
        if (!Spells.IsEmpty)
        {
            Node<Spell> spellNode = Spells.Head;

            while (spellNode != null)
            {
                Spell spell = spellNode.Item;
                spell.Move(0, -Spell.Speed);
                if (CastleGameRenderer.IsOffScreen(spell))
                {
                    Spells.Remove(spellNode);
                }
            }

        }


    }
    private void updateGoblins()
    {
        if (!GoblinSquad.IsEmpty)
        {
            Node<Goblin> goblinNode = GoblinSquad.Tail;

            Node<Spell> spellNode = Spells.Head;




            while (goblinNode != GoblinSquad.Head)
            {
                goblinNode.Item.MoveTowards(goblinNode.Prev.Item, Goblin.Speed);


                while (spellNode != null)
                {
                    if (goblinNode.Item.Colliding(spellNode.Item))
                    {
                        Node<Spell> tmpSpell = spellNode;
                        GoblinSquad.Remove(goblinNode.Item);
                        Spells.Remove(spellNode.Item);
                        goblinDirection = calculateGoblinDirection();
                    }
                    spellNode = spellNode.Next;
                }



                goblinNode = goblinNode.Prev;

            }
            Node<Goblin> goblinHead = GoblinSquad.Head;
            goblinHead.Item.Move(goblinDirection, Goblin.Speed);
            CastleGameRenderer.CheckWallCollision(goblinHead.Item, ref goblinDirection);

            spellNode = Spells.Head;
            while (spellNode != null)
            {
                if (goblinNode.Item.Colliding(spellNode.Item))
                {
                    Node<Spell> tmpSpell = spellNode;
                    GoblinSquad.Remove(goblinNode.Item);
                    Spells.Remove(spellNode.Item);
                    goblinDirection = calculateGoblinDirection();
                }
                spellNode = spellNode.Next;
            }
        }

    }





    private void updateWizards()
    {
        nextSpellTime--;
        nextWizardEatingTime--;

        Console.WriteLine($"nextSpellTime: {nextSpellTime}");

        if (nextSpellTime <= 0)
        {
            Spell newSpell = new Spell(ActiveWizard.Item.SpellType, ActiveWizard.Item.Position);
            Spells.AddFront(newSpell);
            ActiveWizard.Item.Energy -= ActiveWizard.Item.SpellLevel;

            ActiveWizard = ActiveWizard.Next;
            nextSpellTime = calculateNextSpellTime();

            if (ActiveWizard.Item.Energy <= 0)
            {
                WizardSquad.Remove(ActiveWizard);
                RecoveryQueue.Enqueue(ActiveWizard.Item);

            }
        }

        if (nextWizardEatingTime <= 0)
        {
            nextWizardEatingTime = 5;
            if (!RecoveryQueue.IsEmpty)
            {
                RecoveryQueue.Peek().Energy += 1;
            }
        }
        if (!RecoveryQueue.IsEmpty && RecoveryQueue.Peek().Energy == RecoveryQueue.Peek().MaxEnergy)
        {

            WizardSquad.InsertBefore(ActiveWizard, RecoveryQueue.Peek());
            RecoveryQueue.Dequeue();
        }
        // foreach (Wizard wizard in WizardSquad)
        // {

        //     if (wizard.Energy <= 0)
        //     {
        //         WizardSquad.Remove(wizard);
        //     }
        // }
    }



    private void checkIfGobsNeedReinforcements()
    {
        if (GoblinSquad.Count == 4)
        {
            GoblinSquad.AppendAll(BackupGoblins);
        }
    }

    private void checkIfGobsDefeated()
    {
        if (GoblinSquad.IsEmpty)
        {
            Pause();
        }
    }


    protected override void Update(uint currentFrame)
    {
        updateSpells();
        updateGoblins();
        updateWizards();
        checkIfGobsNeedReinforcements();
        checkIfGobsDefeated();

    }
}
