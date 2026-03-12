using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models;
using sts1to2card.src.green.powers;


namespace sts1to2card.src.green.cards
{
    public sealed class GreenCorpseExplosion : CardModel
    {
        public GreenCorpseExplosion()
            : base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new[]
                {
                    new PowerVar<PoisonPower>(6m)
                };
            }
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                return new IHoverTip[]
                {
                    HoverTipFactory.FromPower<PoisonPower>(),
                    HoverTipFactory.FromPower<GreenCorpseExplosionPower>()
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
        {
            var target = play.Target;
            if (target == null) return;

            await PowerCmd.Apply<PoisonPower>(
                target,
                DynamicVars["PoisonPower"].BaseValue,
                Owner.Creature,
                this,
                false
            );

            await PowerCmd.Apply<GreenCorpseExplosionPower>(
                target,
                1,
                Owner.Creature,
                this,
                false
            );
        }

        protected override void OnUpgrade()
        {
            DynamicVars["PoisonPower"].UpgradeValueBy(3m);
        }
    }
}