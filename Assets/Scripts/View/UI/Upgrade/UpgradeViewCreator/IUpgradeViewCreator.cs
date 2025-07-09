using System.Collections.Generic;

public interface IUpgradeViewCreator
{
    IReadOnlyCollection<UpgradeView> CreateViews();
}
