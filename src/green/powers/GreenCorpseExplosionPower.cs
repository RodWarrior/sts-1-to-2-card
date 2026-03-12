using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;


namespace sts1to2card.src.green.powers
{
    public sealed class GreenCorpseExplosionPower : PowerModel
    {
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.None;

        public override async Task AfterDeath(
            PlayerChoiceContext context,
            Creature creature,
            bool wasRemovalPrevented,
            float deathAnimLength)
        {
            if (wasRemovalPrevented || creature != Owner || creature.CombatState == null)
                return;

            int damage = creature.MaxHp + Amount;

            var enemies = creature.CombatState
                .GetOpponentsOf(creature)
                .Where(c => c.IsAlive);

            foreach (var enemy in enemies)
            {
                await CreatureCmd.Damage(
                    context,      // 传入 context，不能是 null
                    enemy,
                    damage,
                    ValueProp.Unblockable,
                    null,
                    null
                );
            }
        }
    }
}
