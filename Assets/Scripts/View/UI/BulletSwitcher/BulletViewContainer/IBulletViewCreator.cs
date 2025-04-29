using System.Collections.Generic;

public interface IBulletViewCreator
{
    IReadOnlyCollection<IBulletView> GetViews(IEnumerable<IBulletDescription> data);
}