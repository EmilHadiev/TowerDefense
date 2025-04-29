using System.Collections.Generic;

public interface IBulletSwitcherViewCreator
{
    IReadOnlyCollection<IBulletView> GetViews(IEnumerable<IBulletDescription> data);
}