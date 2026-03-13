using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	public sealed class RedBloodForBlood : CardModel
	{
		private const string _calculatedCostReductionKey = "CalculatedCostReduction";

		protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
		{
			new DamageVar(18m, ValueProp.Move),
			new RepeatVar(1),
			new CalculatedVar(_calculatedCostReductionKey)
				.WithMultiplier((CardModel card, Creature? _) =>
					CombatManager.Instance.History.Entries
						.OfType<DamageReceivedEntry>()
						.Count(e => e.Receiver == card.Owner.Creature && e.Result.UnblockedDamage > 0))
		};

		protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
		{
			base.EnergyHoverTip
		};

		public RedBloodForBlood()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
		{
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

			// 先计算费用减少
			int reduction = (int)((CalculatedVar)base.DynamicVars[_calculatedCostReductionKey]).Calculate(cardPlay.Target);
			base.EnergyCost.AddThisCombat(-reduction);

			// 执行攻击
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
				.WithHitCount((int)base.DynamicVars.Repeat.BaseValue)
				.FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash")
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m); // 18 -> 24
		}
	}
}