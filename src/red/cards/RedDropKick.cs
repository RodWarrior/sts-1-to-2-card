using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.cards;

public sealed class RedDropKick : CardModel
{
    // 显示伤害和能量/抽牌信息
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(5m, ValueProp.Move)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromPower<VulnerablePower>()
    };

    public RedDropKick()
        : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null) throw new ArgumentNullException(nameof(cardPlay.Target));

        Creature target = cardPlay.Target;

        // 播放攻击VFX，不用 Node
        NThinSliceVfx.Create(target);  // 只调用 VFX 创建方法即可，内部会处理添加到场景

        float animDelay = base.Owner.Character.AttackAnimDelay;
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(target)
            .WithAttackerAnim("Attack", animDelay)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);

        // 如果目标有易伤，奖励能量+抽牌
        if (target.HasPower<VulnerablePower>())
        {
            await PlayerCmd.GainEnergy(1, base.Owner);
            await CardPileCmd.Draw(choiceContext, 1, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m); // 升级伤害 5 -> 8
    }

    // 高亮显示，如果存在带易伤的敌人
    protected override bool ShouldGlowGoldInternal =>
        base.CombatState?.HittableEnemies.Any(e => e.HasPower<VulnerablePower>()) ?? false;
}