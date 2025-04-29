using System.Collections.Generic;

public interface IUpgradeViewCreator
{
    IReadOnlyCollection<IUpgradeView> CreateViews();
}
