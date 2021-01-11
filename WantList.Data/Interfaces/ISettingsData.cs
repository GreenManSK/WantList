using WantList.Core;

namespace WantList.Data.Interfaces
{
    public interface ISettingsData
    {
        Settings Get();
        Settings Update(Settings settings);
        int Commit();
    }
}